using UnityEngine;

/// <summary>
/// Represents an item that can be picked up in the game world.
/// </summary>
[RequireComponent(typeof(NetworkView))]
public class WorldItem : InteractableObject
{
    public string itemName;

    public override void InteractWith(GameObject player)
    {
        if (Network.isServer)
            CommitInteractWith(player.networkView.viewID);
        networkView.RPC("CommitInteractWith", RPCMode.Server, player.networkView.viewID);
    }
    
    [RPC]
    void CommitInteractWith(NetworkViewID networkViewID)
    {
        GameObject interactor = networkViewID.GetGameObject();

        interactor.networkView.RPC("AddItemToInventory", RPCMode.All, itemName);
        Network.Destroy(this.gameObject);
    }
}