using UnityEngine;
using System;
using System.Collections;

public class PlayerStats : CharacterStats
{
    public string username = "default_username";
    public int experience = 0;
    public int Level { get; set; }
    public int experienceNeeded;

    static int[] experiencePerLevel = { 1000, 2000, 3000, 5000, 8000, 13000 };

    private int iron = 0;

    private NetworkPlayer networkPlayer;

    public event PlayerRespawnedEventHandler Respawned;
    public bool canRespawn;

    public override void Start()
    {
        base.Start();
        experienceNeeded = experiencePerLevel[0];
        Level = 1;
        Died += PlayerDied;
        UpdateKillReward();
    }

    internal void SetNetworkPlayer(NetworkPlayer networkPlayer)
    {
        this.networkPlayer = networkPlayer;
    }

    void OnRespawn(Vector3 requestedRespawnLocation)
    {
        if (Respawned != null)
        {
            Respawned(new PlayerRespawnedEventArgs() { respawnPosition = requestedRespawnLocation });
        }
    }

    void PlayerDied(GameObject sender, UnitDiedEventArgs e)
    {
        CharacterStats.RewardPlayersInArea(e.deathPosition, e.killer, e.reward);
        SetRespawnTimer();
        StopMonitoringRegeneration();
        networkView.RPC("BroadcastDeath", RPCMode.All, e.killer.networkView.viewID);

        StartCoroutine(TimedDeathAnimation());

    }

    private IEnumerator TimedDeathAnimation()
    {
        if (Network.isServer)
            networkView.RPC("BeginDying", RPCMode.All);
        yield return new WaitForSeconds(8.0f);
        transform.position = Vector3.zero;
    }

    private void SetRespawnTimer()
    {
        canRespawn = false;
        var respawnTime = 2 * Level + 4f;
        StartCoroutine(RespawnTimer(respawnTime));
    }

    private IEnumerator RespawnTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canRespawn = true;
    }

    private void EnableRespawnCamera()
    {
        Camera.main.GetComponent<RegularCamera>().enabled = false;
        Camera.main.GetComponent<RespawnCamera>().enabled = true;
        Camera.main.GetComponent<BlurEffect>().enabled = false;

    }

    private void DisableRespawnCamera()
    {
        Camera.main.GetComponent<RegularCamera>().enabled = true;
        Camera.main.GetComponent<RespawnCamera>().enabled = false;
    }

    /// <summary>
    /// Adds rewards for this player achieving a kill.
    /// </summary>
    public void RewardKill(KillReward reward)
    {
        this.experience += reward.experience;
        this.iron += reward.iron;
        if (experience > experienceNeeded)
        {
            LevelUp();
        }
        if (gameObject != Util.MyLocalPlayerObject) 
        networkView.RPC("BroadcastReward", this.networkPlayer, reward.experience, reward.iron);
    }

    [RPC] void BroadcastDeath(NetworkViewID killerID)
    {
        if (gameObject == Util.MyLocalPlayerObject)
        {
            PopupMessage.Display("You were killed by " + NetworkView.Find(killerID).gameObject.name);
            gameObject.DisableControls();
            SetRespawnTimer();
            Camera.main.gameObject.GetComponent<BlurEffect>().enabled = true;
            Invoke("EnableRespawnCamera", 6.0f);
            return;
        }
        else
        {
            transform.FindChild("Name Label").GetComponent<ObjectLabel>().DisableLabels();
            if (Util.MyLocalPlayerObject.GetCharacterStats().TeamNumber != this.teamNumber)
            {
                PopupMessage.LocalDisplay(gameObject.name + " has been killed by " + NetworkView.Find(killerID).gameObject.name + "!", 2.5f, 0, 1, 0);
            }
            else
            {
                PopupMessage.LocalDisplay(gameObject.name + " has been killed by " + NetworkView.Find(killerID).gameObject.name + "!", 2.5f, 1, 0, 0);
            }
        }
    }

    /// <summary>
    /// Received by the client when he kills an enemy and receives a reward.
    /// </summary>
    [RPC]
    void BroadcastReward(int xp, int ironReward)
    {
        //	Debug.Log("You have obtained "+xp + " experience and " + iron + " iron.");
        experience += xp;
        iron += ironReward;
    }

    void LevelUp()
    {
        Level++;
        Strength += 5;
        MaxHealth += (int)(MaxHealth * .15f);
        MaxMana += (int)(MaxMana * .15f);
        MaxStamina += (int)(MaxStamina * .15f);

        //	Debug.Log(this.gameObject.name + " has leveled up.");
        experience -= experienceNeeded;
        experienceNeeded = experiencePerLevel[Level - 1];
        UpdateKillReward();
        if (gameObject == Util.MyLocalPlayerObject)
            PopupMessage.Display("You have reached level " + Level);
        else
            networkView.RPC("ClientLevelUp", this.networkPlayer);
    }

    private void UpdateKillReward()
    {
        reward = new KillReward(Level * 400, Level * 200);
    }

    [RPC]
    void ClientLevelUp()
    {
        Level++;
        Strength += 5;
        MaxHealth += (int)(MaxHealth * .15f);
        MaxMana += (int)(MaxMana * .15f);
        MaxStamina += (int)(MaxStamina * .15f);
        //	Debug.Log(this.gameObject.name + " has leveled up.");
        experience -= experienceNeeded;
        experienceNeeded = experiencePerLevel[Level - 1];
        PopupMessage.Display("You have reached level " + Level);
    }

    /// <summary>
    /// Sent from Clients to Server when a respawn is requested at a certain location
    /// This method is run on the server.
    /// </summary>
    /// <param name="requestedRespawnLocation"></param>
    [RPC]
    public void TryRespawn(Vector3 requestedRespawnLocation)
    {
        Debug.Log("Trying to respawn.");
        if (canRespawn)
        {
            Vector3 actualRespawnLocation = Util.FindClosestTeamRespawn(requestedRespawnLocation, teamNumber);
            networkView.RPC("StopDying", RPCMode.All);
            StartMonitoringRegeneration();
            Health = MaxHealth;
            transform.position = actualRespawnLocation;
            OnRespawn(actualRespawnLocation);
            networkView.RPC("BroadcastRespawn", RPCMode.All, actualRespawnLocation);
        }
    }

    [RPC]
    void BroadcastRespawn(Vector3 respawnLocation)
    {
        Debug.Log(gameObject.name + "has respawned.");

        if (Util.MyLocalPlayerObject == gameObject)
        {
            gameObject.EnableControls();
            DisableRespawnCamera();
            
        }
        else
        {
            if (transform.FindChild("Name Label"))
            {
                transform.FindChild("Name Label").GetComponent<ObjectLabel>().EnableLabels();
            }
        }
    }
    [RPC]
    public override void ChangeName(String newName)
    {
        base.ChangeName(newName);
        username = newName;
    }
}