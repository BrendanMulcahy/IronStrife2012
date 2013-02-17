using UnityEngine;

/// <summary>
/// Represents an item that can be picked up in the game world.
/// </summary>
[RequireComponent(typeof(NetworkView))]
public class WorldItem : InteractableObject
{
    /// <summary>
    /// The name of the item
    /// </summary>
    public string itemName;
    public Item item;

    public override void InteractWith(GameObject player)
    {
        if (Network.isServer)
            ServerInteractWith(player.networkView.viewID);
        else
            networkView.RPC("ServerInteractWith", RPCMode.Server, player.networkView.viewID);
    }
    
    [RPC]
    void ServerInteractWith(NetworkViewID playerViewID)
    {
        GameObject interactor = playerViewID.GetGameObject();

        if (item == null)
        {
            Debug.Log("This item doesn't have a viewID yet. Assigning one.");
            item = ItemFactory.CreateItemForPlayer(this.itemName);

        }

        interactor.networkView.RPC("CommitAddToInventory", RPCMode.All, item.viewID, itemName);
        Network.Destroy(this.gameObject);
    }
}