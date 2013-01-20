using UnityEngine;

public class GameConnect : MonoBehaviour
{
    void OnNetworkLoadedLevel()
    {
        Debug.Log("doing stuff.");
        if (Network.isServer)
        {
            HandleServerStartup();
        }
        else
        {
            HandleClientStartup();
        }
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        Network.SetSendingEnabled(player, 0, false);
    }

    void Awake()
    {
        networkView.group = 1;
    }

    private void HandleClientStartup()
    {
        networkView.RPC("NewClientConnection", RPCMode.Server, Util.Username);
        //Send a request to the server to get info about other players, the world, etc.


    }

    private void HandleServerStartup()
    {
        new GameObject("MasterGameLogic").AddComponent<MasterGameLogic>();
        MasterGameLogic.Main.PlayerManager.GenerateNewPlayer(Network.player, Util.Username);
    }

    [RPC]
    void NewClientConnection(string username, NetworkMessageInfo msg)
    {
        StartCoroutine(HandleNewClientConnection(username, msg));

    }

    private System.Collections.IEnumerator HandleNewClientConnection(string username, NetworkMessageInfo msg)
    {
        networkView.RPC("LoadingStarted", msg.sender);
        ClientStartup(msg);
        AssignNewPlayerCharacter(username, msg);
        yield return new WaitForSeconds(2.0f);
        SynchronizeGameObjects(msg.sender);

        Network.SetSendingEnabled(msg.sender, 0, true);
        networkView.RPC("LoadingFinished", msg.sender);
        MessageTerminal.Main.networkView.RPC("GameStarted", msg.sender);
    }

    private void SynchronizeGameObjects(NetworkPlayer networkPlayer)
    {
        GameObject[] allGameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];

        foreach (GameObject go in allGameObjects)
        {
            go.SendMessage("SynchronizePlayer", networkPlayer, SendMessageOptions.DontRequireReceiver);
        }
    }

    /// <summary>
    /// Run on the server. Should send lots of info to the client about the world and stuff
    /// </summary>
    /// <param name="msg"></param>
    void ClientStartup(NetworkMessageInfo msg)
    {
        Debug.Log(msg.sender);
        foreach (PlayerRecord pr in MasterGameLogic.Main.PlayerManager.players)
        {
            networkView.RPC("SpawnCharacter", msg.sender, pr.username, pr.networkPlayer, pr.team, pr.interpolationViewID, pr.animationViewID);
        }



    }

    [RPC]
    void SpawnCharacter(string username, NetworkPlayer player, int team, NetworkViewID interpolationViewID, NetworkViewID animationViewID)
    {
        PlayerBuilder.GenerateClient(username, interpolationViewID, animationViewID);

    }

    [RPC]
    void AssignNewPlayerCharacter(string username, NetworkMessageInfo msg)
    {
        var pr = MasterGameLogic.Main.PlayerManager.GenerateNewPlayer(msg.sender, username);
        networkView.RPC("SpawnCharacterAndSetOwnership", msg.sender, pr.username, pr.networkPlayer, pr.team, pr.interpolationViewID, pr.animationViewID);

    }

    [RPC]
    void SpawnCharacterAndSetOwnership(string username, NetworkPlayer player, int team, NetworkViewID interpolationViewID, NetworkViewID animationViewID)
    {
        var go = PlayerBuilder.GenerateClient(username, interpolationViewID, animationViewID);

        PlayerBuilder.SetOwnership(go);

    }

    /// <summary>
    /// Deactivates message receiving for the general channel (0)
    /// </summary>
    [RPC]
    void LoadingStarted()
    {
        Network.SetReceivingEnabled(Network.connections[0], 0, false);
        Debug.Log("Disabling Network receiving on the server.");
    }

    /// <summary>
    /// Reactivates message receiving for the general channel (0)
    /// </summary>
    [RPC]
    void LoadingFinished()
    {
        Network.SetReceivingEnabled(Network.connections[0], 0, true);
        Debug.Log("Re-enabling Network receiving on the server.");

    }

}