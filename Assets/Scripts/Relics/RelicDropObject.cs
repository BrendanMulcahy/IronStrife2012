using UnityEngine;

/// <summary>
/// Object players interact with in order to drop relics.
/// </summary>
public class RelicDropObject : InteractableObject
{

    public override void InteractWith(GameObject player)
    {
        var relic = player.GetComponentInChildren<Relic>();
        if (relic)
        {
            Debug.Log("Trying to drop relic.");
            relic.networkView.RPCToServer("TryDropRelic");
        }
        else Debug.Log("Player doesn't have a relic component.");
    }
}