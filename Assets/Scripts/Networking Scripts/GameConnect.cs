using UnityEngine;

/// <summary>
/// Handles game initialization upon server creation or connection to server
/// </summary>
public class GameConnect : MonoBehaviour
{

    void OnServerInitialized()
    {
        HandleServerStartup();

    }

    void OnConnectedToServer()
    {
        var fader = this.gameObject.AddComponent<CameraFade>();
        fader.FadeToSolid(1.0f);
        networkView.RPC("NewClientConnection", RPCMode.Server, Util.Username);
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        // When a player connects, disable regular communication with him until they are fully loaded
        Network.SetSendingEnabled(player, 0, false);
        networkView.RPCToGroup("SynchronizeGameTime", 2, player, GameTime.CurrentTime);
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        networkView.RPC("PlayerDisconnected", RPCMode.All, PlayerManager.Main.GetPlayerGameObject(player).GetNetworkViewID());
        PlayerManager.Main.RemovePlayer(player);
    }

    [RPC]
    void PlayerDisconnected(NetworkViewID viewID)
    {
        GameObject player = viewID.GetGameObject();
        player.SendMessage("PlayerDisconnected", SendMessageOptions.DontRequireReceiver);
        Destroy(player);

    }

    void Awake()
    {
        networkView.group = 1;
    }

    private void HandleServerStartup()
    {
        // Reset the game time (time passes in the main menu)
        GameTime.Reset();

        // Add a master game logic object and create the server's player.
        new GameObject("MasterGameLogic").AddComponent<MasterGameLogic>();
        if (SystemInfo.graphicsDeviceID != 0)
            PlayerManager.Main.GenerateNewPlayer(Network.player, Util.Username);
    }

    [RPC]
    void NewClientConnection(string username, NetworkMessageInfo msg)
    {
        StartCoroutine(HandleNewClientConnection(username, msg));
    }

    /// <summary>
    /// Server function that handles the connection of a new client. Should create their character and
    /// synchronize the new player with all of the info about the world (other players, objects, game state, etc)
    /// </summary>
    /// <param name="username"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    private System.Collections.IEnumerator HandleNewClientConnection(string username, NetworkMessageInfo msg)
    {
        // Send all player character information to the player 
        networkView.RPC("LoadingStarted", msg.sender);
        ClientStartup(msg);
        AssignNewPlayerCharacter(username, msg);
        SynchronizeGameObjects(msg.sender); // Synchronize all GameObjects we just sent.

        // Resume state updates to the newly connected client 
        Network.SetSendingEnabled(msg.sender, 0, true);
        networkView.RPC("LoadingFinished", msg.sender); // Notify the client that they are finished loading

        yield break;
    }

    private void SynchronizeGameObjects(NetworkPlayer networkPlayer)
    {
        // Sends a message to all GameObjects and tells them to synchronize with the newly connected player
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
        foreach (PlayerRecord pr in PlayerManager.Main.players)
        {
            networkView.RPC("SpawnCharacter", msg.sender, pr.username, pr.networkPlayer, pr.team, pr.interpolationViewID, pr.animationViewID);
        }
    }

    /// <summary>
    /// Run on the client to spawn another player's character.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="player"></param>
    /// <param name="team"></param>
    /// <param name="interpolationViewID"></param>
    /// <param name="animationViewID"></param>
    [RPC]
    void SpawnCharacter(string username, NetworkPlayer player, int team, NetworkViewID interpolationViewID, NetworkViewID animationViewID)
    {
        PlayerBuilder.GenerateClient(username, interpolationViewID, animationViewID);

    }

    /// <summary>
    /// Run on the server. Generates a new player character for a newly connected player.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="msg"></param>
    [RPC]
    void AssignNewPlayerCharacter(string username, NetworkMessageInfo msg)
    {
        var pr = MasterGameLogic.Main.PlayerManager.GenerateNewPlayer(msg.sender, username);
        networkView.RPC("SpawnCharacterAndSetOwnership", msg.sender, pr.username, pr.networkPlayer, pr.team, pr.interpolationViewID, pr.animationViewID);

        foreach (PlayerRecord rec in PlayerManager.Main.players)
        {
            if (rec != pr && rec.networkPlayer != Network.player)
            {
                Debug.Log("rec.networkPlayer = " + rec.networkPlayer);
                networkView.RPC("SpawnCharacter", rec.networkPlayer, pr.username, pr.networkPlayer, pr.team, pr.interpolationViewID, pr.animationViewID);
            }
        }

    }

    /// <summary>
    /// Run on the client. Spawns the client's player and gives them ownership
    /// </summary>
    /// <param name="username"></param>
    /// <param name="player"></param>
    /// <param name="team"></param>
    /// <param name="interpolationViewID"></param>
    /// <param name="animationViewID"></param>
    [RPC]
    void SpawnCharacterAndSetOwnership(string username, NetworkPlayer player, int team, NetworkViewID interpolationViewID, NetworkViewID animationViewID)
    {
        var go = PlayerBuilder.GenerateClient(username, interpolationViewID, animationViewID);

        PlayerBuilder.SetOwnership(go);

    }

    [RPC]
    void LoadingStarted()
    {

    }

    [RPC]
    void LoadingFinished()
    {
        var fader = this.gameObject.GetComponent<CameraFade>();
        fader.FadeToTransparent(3.0f);
        Destroy(fader, 3.1f);
    }

    [RPC]
    void SynchronizeGameTime(float gameTime)
    {
        GameTime.SetTime(gameTime);
    }
}