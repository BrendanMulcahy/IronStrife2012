using UnityEngine;
using System.Collections.Generic;

public class PlayerManager
{
    public List<PlayerRecord> players = new List<PlayerRecord>();
    public List<GameObject> goodPlayers = new List<GameObject>();
    public List<GameObject> evilPlayers = new List<GameObject>();

    private int numGoodPlayers
    {
        get
        {
            int numPlayers = 0;
            foreach (PlayerRecord pr in players)
            {
                if (pr.team == 1) numPlayers++;
            }
            return numPlayers;
        }
    }

    private int numEvilPlayers
    {
        get
        {
            int numPlayers = 0;
            foreach (PlayerRecord pr in players)
            {
                if (pr.team == 2) numPlayers++;
            }
            return numPlayers;
        }
    }

    public PlayerRecord GenerateNewPlayer(NetworkPlayer player, string username)
    {
        var interpolationViewID = Network.AllocateViewID();
        var animationViewID = Network.AllocateViewID();
        var team = 1;

        GameObject newPlayer;

        if (player == Network.player && Network.isServer)
        {
            newPlayer = PlayerBuilder.GenerateServer(username, interpolationViewID, animationViewID);
            PlayerBuilder.SetOwnership(newPlayer);
        }
        else
        {
            newPlayer = PlayerBuilder.GenerateServer(username, interpolationViewID, animationViewID);
        }
        return AddPlayer(newPlayer, username, player, interpolationViewID, animationViewID, team);
    }
     
    /// <summary>
    /// Adds the player to the master player list. If a team number isn't given, a random team number is assigned.
    /// </summary>
    /// <param name="gameObject">The new player's game object</param>
    /// <param name="player">The network player of the new player</param>
    /// <param name="team">[Optional] The team number of the new player. If not supplied, will be auto-assigned.</param>
    public PlayerRecord AddPlayer(GameObject gameObject, string username, NetworkPlayer player, NetworkViewID interpolationViewID, NetworkViewID animationViewID, int team = -1)
    {
        if (team==-1)
        {
            team = GetAutoAssignTeamNumber();
        }
        var newPlayer = new PlayerRecord()
            {
                gameObject = gameObject,
                networkPlayer = player,
                team = team,
                interpolationViewID = interpolationViewID,
                animationViewID = animationViewID,
                username = username,

            };
        players.Add(newPlayer);
        Debug.Log("A new player has been added to team " + team);
        if (newPlayer.team == 1)
        {
            goodPlayers.Add(gameObject);
        }
        else
        {
            evilPlayers.Add(gameObject);
        }
            
        return newPlayer;
    }

    /// <summary>
    /// Returns the team number of the team with less players on it.
    /// </summary>
    /// <returns>The team with less players on it</returns>
    private int GetAutoAssignTeamNumber()
    {
        return (numGoodPlayers <=  numEvilPlayers) ? 1 : 2;
    }

    private PlayerRecord FindRecord(GameObject go)
    {
        foreach (PlayerRecord pr in players)
        {
            if (pr.gameObject == go)
            {
                return pr;
            }
        }
        Debug.LogWarning("User not found.");
        return null;
    }

    private PlayerRecord FindRecord(string username)
    {
        foreach (PlayerRecord pr in players)
        {
            if (pr.username == username)
            {
                return pr;
            }
        }
        Debug.LogWarning("User not found.");
        return null;
    }


    internal GameObject GetPlayerGameObject(NetworkPlayer player)
    {
        foreach (PlayerRecord pr in players)
        {
            if (pr.networkPlayer == player)
                return pr.gameObject;
        }
        Debug.LogError("Disconnected player was not added to the player list.");
        return null;
    }

    internal void ChangePlayerTeam(GameObject gameObject, int oldTeam, int newTeam)
    {
        if (oldTeam == 1 && newTeam == 2)
        {
            goodPlayers.Remove(gameObject);
            evilPlayers.Add(gameObject);
        }
        else if (oldTeam == 2 && newTeam ==1)
        {
            evilPlayers.Remove(gameObject);
            goodPlayers.Add(gameObject);
        }
        this.FindRecord(gameObject).team = newTeam;
    }
}

public class PlayerRecord
{
    public GameObject gameObject;
    public int team;
    public NetworkPlayer networkPlayer;
    public string username;

    public NetworkViewID interpolationViewID;
    public NetworkViewID animationViewID;
}