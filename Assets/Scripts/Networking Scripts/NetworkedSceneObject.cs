using System;
using UnityEngine;

public class NetworkedSceneObject : MonoBehaviour
{
    public Type type;

    void SynchronizePlayer(NetworkPlayer player)
    {
        Debug.Log(this.name + " is synchronizing " + player.ToString());
        var networkViews = this.GetComponents<NetworkView>();
        switch (networkViews.Length)
        {
            case 1:
                MessageTerminal.Main.networkView.RPCToGroup("CreateNetworkedSceneObject1", 2, player, type.Name, this.networkView.viewID);
                break;
            case 2:
                MessageTerminal.Main.networkView.RPCToGroup("CreateNetworkedSceneObject2", 2, player, type.Name, networkViews[0].viewID, networkViews[1].viewID);
                break;
            case 3:
                MessageTerminal.Main.networkView.RPCToGroup("CreateNetworkedSceneObject3", 2, player, type.Name, networkViews[0].viewID, networkViews[1].viewID, networkViews[2].viewID);
                break;
            case 4:
            case 5:
                Debug.LogError("NetworkedSceneObject has way too many network views. That isn't supported.");
                break;
            default:
                Debug.LogError("A component marked with the NetworkedSceneObject attribute does not have any networkviews attached to it.");
                break;
        }
    }
}