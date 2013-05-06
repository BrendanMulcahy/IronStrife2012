using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class UserIdSync : MonoBehaviour {

    public static int userId = -1;

    private Dictionary<NetworkPlayer, int> players = new Dictionary<NetworkPlayer, int>();

    public void SetUserId(string value)
    {
        int val;
        if (int.TryParse(value, out val))
        {
            userId = val;
            Debug.Log("Received userid of " + userId);
        }
        else
        {
            Debug.LogError("Invalid format for userid: <" + value + ">");
        }
    }

    public void SetUsername(string value)
    {
        PlayerPrefs.SetString("username", value);
        Debug.Log("Received username: " + value);
    }

    void OnConnectedToServer()
    {
        networkView.RPC("BroadcastUserId", RPCMode.Server, userId);
        if (PlayerPrefs.GetInt("teamNumber", -1) != -1 && userId != -1)
        {

        }
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        StrifeMasterServer.NotifyPlayerLeft(players[player]);
        Network.RemoveRPCs(player);
        Network.RemoveRPCsInGroup(players[player]);
        Network.DestroyPlayerObjects(player);
    }

    [RPC]
    void BroadcastUserId(int id, NetworkMessageInfo info)
    {
        Debug.Log("Received user ID: " + info.sender.externalIP + " has id <" + id + ">");
        players[info.sender] = id;
        StrifeMasterServer.NotifyPlayerJoined(id);
        networkView.RPC("ReceivedId", info.sender);
    }

    [RPC]
    void ReceivedId()
    {
        Debug.Log("The server received your player ID.");
    }

}
