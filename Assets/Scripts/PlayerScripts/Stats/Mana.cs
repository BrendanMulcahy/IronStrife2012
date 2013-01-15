using UnityEngine;

public class Mana : RegeneratingStat
{
    protected override System.Collections.IEnumerator Monitor()
    {
        networkView.RPC("UpdateMana", RPCMode.Others, CurrentValue);
        int previousValue = CurrentValue;
        while (true)
        {
            if (previousValue != CurrentValue)
            {
                networkView.RPC("UpdateMana", RPCMode.Others, CurrentValue);
                previousValue = CurrentValue;
            }
            yield return null;
        }
    }

    public void Intelligence_Changed(GameObject sender, StatChangedEventArgs e)
    {
        var difference = e.newValue - e.oldValue;
        this.MaxValue = Mathf.Max(1, CurrentValue + (difference * IntelligenceStat.manaPerIntel));
    }

    [RPC]
    void UpdateMana(int newValue)
    {
        _currentValue = newValue;
    }
}
