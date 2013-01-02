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

    [RPC]
    void UpdateMana(int newValue)
    {
        _currentValue = newValue;
    }
}
