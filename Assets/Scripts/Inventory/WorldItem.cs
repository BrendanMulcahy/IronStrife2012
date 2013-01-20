using UnityEngine;

[RequireComponent(typeof(NetworkView))]
public class WorldItem : InteractableObject
{
    public string itemName;

    public override void InteractWith(GameObject player)
    {
        if (Network.isServer)
            NetworkInteractWith(player.networkView.viewID);
        networkView.RPC("NetworkInteractWith", RPCMode.Server, player.networkView.viewID);

    }

    [RPC]
    void NetworkInteractWith(NetworkViewID networkViewID)
    {
        GameObject interactor = NetworkView.Find(networkViewID).gameObject;

        interactor.networkView.RPC("AddItemToInventory", RPCMode.All, itemName);
        Network.Destroy(this.gameObject);
    }
}