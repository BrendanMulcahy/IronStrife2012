using UnityEngine;

public class Stamina : RegeneratingStat
{
    protected override System.Collections.IEnumerator Monitor()
    {
        networkView.RPC("UpdateStamina", RPCMode.Others, CurrentValue);
        int previousValue = CurrentValue;
        while (true)
        {
            if (previousValue != CurrentValue)
            {
                networkView.RPC("UpdateStamina", RPCMode.Others, CurrentValue);
                previousValue = CurrentValue;
            }
            yield return null;
        }
    }

    [RPC]
    void UpdateStamina(int newValue)
    {
        _currentValue = newValue;
    }

    public void Agility_Changed(GameObject sender, StatChangedEventArgs e)
    {
        var difference = e.newValue - e.oldValue;
        float currentPercentage = (float)CurrentValue / MaxValue;
        this.MaxValue = Mathf.Max(1, CurrentValue + (difference * AgilityStat.staminaPerAgility));

        //Retain percentage on stat gain (dota-esque)
        this.CurrentValue = (int)(this.MaxValue * currentPercentage);
    }
}
