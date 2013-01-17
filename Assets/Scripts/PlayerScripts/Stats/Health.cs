using UnityEngine;

public class Health : RegeneratingStat
{
    protected override System.Collections.IEnumerator Monitor()
    {
        networkView.RPC("UpdateHealth", RPCMode.Others, CurrentValue);
        int previousValue = CurrentValue;
        while (true)
        {
            if (previousValue != CurrentValue)
            {
                networkView.RPC("UpdateHealth", RPCMode.Others, CurrentValue);
                previousValue = CurrentValue;
            }
            yield return null;
        }
    }

    public void Strength_Changed(GameObject sender, StatChangedEventArgs e)
    {
        var difference = e.newValue - e.oldValue;
        float currentPercentage = (float)CurrentValue / MaxValue;
        this.MaxValue = Mathf.Max(1, CurrentValue + (difference * StrengthStat.healthPerStrength));

        //Retain percentage on stat gain (dota-esque)
        this.CurrentValue = (int)(this.MaxValue * currentPercentage);
    }

    [RPC]
    void UpdateHealth(int newValue)
    {
        _currentValue = newValue;
    }
}
