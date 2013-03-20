using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    public List<PlayerRecord> players = new List<PlayerRecord>();
    public List<GameObject> goodPlayers = new List<GameObject>();
    public List<GameObject> evilPlayers = new List<GameObject>();

    private List<RespawnPoint> respawnPoints = new List<RespawnPoint>();

    public static PlayerManager Main
    {
        get { return MasterGameLogic.Main.PlayerManager; }
    }

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

    public PlayerRecord GenerateNewPlayerRecord(NetworkPlayer player, string username)
    {
        var interpolationViewID = Network.AllocateViewID();
        var animationViewID = Network.AllocateViewID();
        var team = 1;
        var record = AddPlayer(null, username, player, interpolationViewID, animationViewID, team);
        GameObject newPlayer;

        if (player == Network.player && Network.isServer)
        {
            newPlayer = PlayerBuilder.GenerateServer(username, interpolationViewID, animationViewID, record);
            PlayerBuilder.SetOwnership(newPlayer);
        }
        else
        {
            newPlayer = PlayerBuilder.GenerateServer(username, interpolationViewID, animationViewID, record);
        }
        record.gameObject = newPlayer;
        return record;
    }

    /// <summary>
    /// Adds the player to the master player list. If a team number isn't given, a random team number is assigned.
    /// </summary>
    /// <param name="gameObject">The new player's game object</param>
    /// <param name="player">The network player of the new player</param>
    /// <param name="team">[Optional] The team number of the new player. If not supplied, will be auto-assigned.</param>
    public PlayerRecord AddPlayer(GameObject gameObject, string username, NetworkPlayer player, NetworkViewID interpolationViewID, NetworkViewID animationViewID, int team = -1)
    {
        if (team == -1)
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
        return 1;
    }

    public PlayerRecord FindRecord(GameObject go)
    {
        foreach (PlayerRecord pr in players)
        {
            if (pr.gameObject == go)
            {
                return pr;
            }
        }
        Debug.LogWarning("PlayerRecord for " + go.name + " not found.");
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

    private PlayerRecord FindRecord(NetworkPlayer player)
    {
        foreach (PlayerRecord pr in players)
        {
            if (pr.networkPlayer == player)
            {
                return pr;
            }
        }
        Debug.LogWarning("User not found.");
        return null;
    }

    internal List<PlayerRecord> GetPlayersOnTeam(int team)
    {
        return players.Where(rec => rec.team == team).ToList();
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
        else if (oldTeam == 2 && newTeam == 1)
        {
            evilPlayers.Remove(gameObject);
            goodPlayers.Add(gameObject);
        }
        this.FindRecord(gameObject).team = newTeam;
    }

    internal void RemovePlayer(NetworkPlayer player)
    {
        var record = FindRecord(player);
        players.Remove(record);
    }

    internal void RegisterRespawnPoint(RespawnPoint respawnPoint)
    {
        if (!respawnPoints.Contains(respawnPoint))
        {
            respawnPoints.Add(respawnPoint);
        }
    }

    internal void DeregisterRespawnPoint(RespawnPoint respawnPoint)
    {
        if (respawnPoints.Contains(respawnPoint))
        {
            respawnPoints.Remove(respawnPoint);
        }
    }

    internal RespawnPoint GetClosestRespawnPoint(Vector3 location, int teamNumber)
    {
        if (respawnPoints.Count == 0)
        {
            Debug.LogWarning("There are no respawn points registered.");
            return null;
        }
        var sortedList = respawnPoints.Where((g) => (g.controllingTeam == teamNumber)).OrderBy((g) => Vector3.Distance(this.transform.position, g.transform.position)).ToArray();
        return sortedList[0];
    }

    internal RespawnPoint GetStartingSpawnLocation(int teamNumber)
    {
        if (respawnPoints.Count == 0)
        {
            Debug.LogWarning("There are no respawn points registered.");
            return null;
        }
        var sortedList = respawnPoints.Where((g) => (g.isStartingSpawn)).ToArray();
        return sortedList[0];
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