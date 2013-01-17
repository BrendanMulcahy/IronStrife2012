using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Handles instantiating network player objects, initializing them, setting ownership, and keeping track of them.
/// </summary>
public class SpawnPrefab : MonoBehaviour {

    /// <summary>
    /// The default prefab to be spawned.
    /// </summary>
    private string prefabName = "PlayerPrefabMelee01";

    void OnNetworkLoadedLevel ()
    {
        string username = PlayerPrefs.GetString("username", "DEFAULT_USERNAME");
        if (Network.isServer)
        {
            AuthoritativeServerSpawnPlayer(username, prefabName);
        }
        else
        {
            networkView.RPC("AuthoritativeServerSpawnPlayer", RPCMode.Server, username, prefabName);
        }
    }

    void Start()
    {
    }

    void OnGUI()
    {
        GUI.skin = Resources.Load("ISEGUISkin") as GUISkin;
    }

    /// <summary>
    /// This method is run on the server. Clients request it when they join the game.
    /// </summary>
    /// <param name="username">The username which is used to name the gameobject</param>
    /// <param name="msg"></param>
    [RPC]
    void AuthoritativeServerSpawnPlayer(string username, string prefabName, NetworkMessageInfo msg)
    {
        Debug.Log("RPC call into group " + int.Parse(msg.sender.ToString()));
        GameObject playerPrefab = Resources.Load("Player/" + prefabName) as GameObject;
        GameObject newPlayer = Network.Instantiate(playerPrefab, this.transform.position, Quaternion.identity, int.Parse(msg.sender.ToString())) as GameObject;
        newPlayer.networkView.RPC("ChangeName", RPCMode.AllBuffered, username);
        int team = MasterGameLogic.Main.PlayerManager.AddPlayer(newPlayer.gameObject, msg.sender);
        newPlayer.gameObject.GetCharacterStats().TeamNumber = team;
        ((PlayerStats)newPlayer.GetCharacterStats()).SetNetworkPlayer(msg.sender);

        newPlayer.networkView.RPC("SetOwnership", msg.sender);
    }

    /// <summary>
    /// Server version of AuthoritativeSpawnPlayer, only for instantiating the Server's player
    /// </summary>
    void AuthoritativeServerSpawnPlayer(string username, string prefabName)
    {
        GameObject playerPrefab = Resources.Load("Player/" + prefabName) as GameObject;
        GameObject newPlayer = Network.Instantiate(playerPrefab, this.transform.position, this.transform.rotation, 0) as GameObject;
        //newPlayer.gameObject.GetComponent<ThirdPersonNetworkInit>().EquipDefaultItems();
        newPlayer.gameObject.GetComponent<PlayerBuilder>().BuildCharacter();
        newPlayer.gameObject.GetComponent<PlayerBuilder>().SetOwnership();
        ((PlayerStats)newPlayer.GetCharacterStats()).SetNetworkPlayer(Network.player);
        int team = MasterGameLogic.Main.PlayerManager.AddPlayer(newPlayer.gameObject, Network.player);
        newPlayer.gameObject.GetCharacterStats().TeamNumber = team;
        newPlayer.networkView.RPC("ChangeName", RPCMode.AllBuffered, username);

    }

    /// <summary>
    /// Cleans up player after he disconnects. Removes his GO and also removes his buffered RPCs.
    /// </summary>
    /// <param name="player"></param>
    void OnPlayerDisconnected (NetworkPlayer player)
    {
        // Removes the buffered instantiation call for the player who disconnected.
        Network.RemoveRPCsInGroup(int.Parse(player.ToString()));
        Network.RemoveRPCs(player);
        // Not sure what this next line does.
	    Network.DestroyPlayerObjects(player);

        // Finds the player's object on the server and removes it.
        MasterGameLogic.Main.PlayerManager.GetPlayerGameObject(player).networkView.RPC("PlayerDisconnected", RPCMode.All);
    }
}
