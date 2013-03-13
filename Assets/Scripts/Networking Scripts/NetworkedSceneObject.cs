using System;
using UnityEngine;

public class NetworkedSceneObject : MonoBehaviour
{
    void SynchronizePlayer(NetworkPlayer player)
    {
        var networkViews = this.GetComponents<NetworkView>();
        switch (networkViews.Length)
        {
            case 1:
                MessageTerminal.Main.networkView.RPC("CreateNetworkedSceneObject1", player, this.GetType().Name, this.networkView.viewID);
                break;
            case 2:
                MessageTerminal.Main.networkView.RPC("CreateNetworkedSceneObject2", player, this.GetType().Name, this.networkView.viewID);
                break;
            case 3:
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