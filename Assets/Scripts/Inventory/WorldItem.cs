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
        if (player.GetInventory().IsFull) PopupMessage.Display("Your inventory is full!", 0f);
        networkView.RPCToServer("TryPickupItem", player.GetNetworkViewID());
    }
    
    [RPC]
    void TryPickupItem(NetworkViewID playerViewID)
    {
        GameObject interactor = playerViewID.GetGameObject();
        if (interactor.GetInventory().IsFull) return;
        if (item == null)
        {
            Debug.Log("This item doesn't have a viewID yet. Assigning one.");
            item = ItemFactory.CreateItemForPlayer(this.itemName);

        }

        interactor.networkView.RPC("CommitAddToInventory", RPCMode.All, item.viewID, itemName);
        Network.Destroy(this.gameObject);
    }

    public static GameObject GetWorldItemPrefab(string itemName)
    {
        GameObject worldItemPrefab = Resources.Load("Items/WorldItems/" + itemName) as GameObject;
        if (!worldItemPrefab)
        {
            worldItemPrefab = Resources.Load("Items/WorldItems/PlaceholderItem") as GameObject;
        }

        return worldItemPrefab;
    }
}