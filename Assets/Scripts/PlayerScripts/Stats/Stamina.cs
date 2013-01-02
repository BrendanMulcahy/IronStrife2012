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
}
