﻿using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    public float NeutralSpawnRadius = 100f;
    public int numNPCsPerWave = 25;
    public string npcType = "AssaultSkeleton";
    public string guardType = "GuardNPC";

    public List<GameObject> guardSpawnLocations = new List<GameObject>();
    public List<GameObject> playersInArea = new List<GameObject>();

    public int controllingTeam = 1;

    /// <summary>
    /// The amount of control a team has over a control point. 
    /// 100 is completely controlled by team 1, 
    /// -100 is completely controlled by team 2,
    /// 0 is neutral controlled
    /// </summary>
    public float influence;

    /// <summary>
    /// Amount of influence that the point will decay to when empty
    /// </summary>
    public float influenceDecayLimit;

    public event ControlPointCapturedEventHandler Captured;

    void OnMasterGameLogicAdded()
    {
        NPCManager.Main.AddControlPoint(this);
    }

    void Start()
    {
        this.gameObject.layer = 16;
        if (!collider)
            AddDefaultCollider();

        if (controllingTeam == 1) influence = 100;
        else if (controllingTeam == 2) influence = -100;
        else influence = 0;
    }

    void Update()
    {
        RemoveNullPlayers();
        UpdateInfluence();
    }

    private void RemoveNullPlayers()
    {
        for (int g = 0; g < playersInArea.Count; g++)
        {
            if (playersInArea[g] == null)
            {
                playersInArea.RemoveAt(g);
                g--;
            }
        }
    }

    private void UpdateInfluence()
    {
        int team1 = 0;
        int team2 = 0;
        int team0 = 0;

        foreach (GameObject player in playersInArea)
        {
            int team = player.GetTeamNumber();
            if (team == 0) team0++;
            else if (team == 1) team1++;
            else if (team == 2) team2++;
        }
        influence += team1 * Time.deltaTime * 3f;
        influence -= team2 * Time.deltaTime * 3f;

        var towardsZero = (influence > 0) ? -1 : 1;
        var neutralEffect = team0 * Time.deltaTime * 3f * towardsZero;
        if (Mathf.Abs(neutralEffect) > Mathf.Abs(influence))
            influence = 0;
        else
            influence += neutralEffect;
        influence = Mathf.Clamp(influence, -100, 100);

        UpdateControl();
        UpdateDecayLimit();

        if (playersInArea.Count == 0)
            DecayInfluence();
    }

    private void UpdateDecayLimit()
    {
        if (influence <= -75) influenceDecayLimit = -75;
        else if (influence <= -50) influenceDecayLimit = -50;
        else if (influence <= -25) influenceDecayLimit = -25;
        else if (influence <= 0) influenceDecayLimit = 0;
        else if (influence >= 75) influenceDecayLimit = 75;
        else if (influence >= 50) influenceDecayLimit = 50;
        else if (influence >= 25) influenceDecayLimit = 25;

    }

    private void UpdateControl()
    {
        if (influence > 0 && controllingTeam != 1) controllingTeam = 1;
        else if (influence < 0 && controllingTeam != 2) controllingTeam = 2;
        else if (controllingTeam != 0) controllingTeam = 0;
    }

    private void DecayInfluence()
    {
        if (influence > 0)
        {
            influence = Mathf.Max(influenceDecayLimit, influence - 3f * Time.deltaTime);
        }

        if (influence < 0)
        {
            influence = Mathf.Min(influenceDecayLimit, influence + 3f * Time.deltaTime);
        }
    }

    private void AddDefaultCollider()
    {
        var sphereCollider = this.gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = 15f;
        sphereCollider.isTrigger = true;

    }

    internal void SpawnNeutralWave()
    {
        for (int g = 0; g < numNPCsPerWave; g++)
        {
            var direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            direction.Normalize();
            var locationToSpawn = this.transform.position + direction * NeutralSpawnRadius;

            var newNPC = NPCManager.Main.ServerSpawnNPC(npcType, locationToSpawn);
            newNPC.GetComponent<NeutralWaveBehaviour>().Target = this.gameObject;
        }
    }

    internal void SpawnGuards()
    {
        foreach (GameObject go in guardSpawnLocations)
        {
            var pos = go.transform.position;
            NPCManager.Main.ServerSpawnNPC(guardType, pos, this.transform.rotation);
        }
    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo msg)
    {
        stream.Serialize(ref controllingTeam);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!playersInArea.Contains(other.gameObject))
        {
            playersInArea.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (playersInArea.Contains(other.gameObject))
        {
            playersInArea.Remove(other.gameObject);
        }
    }
}

public delegate void ControlPointCapturedEventHandler(GameObject sender, ControlPointCapturedEventArgs e);

public class ControlPointCapturedEventArgs
{
    public ControlPoint capturedPoint;
    public int oldTeam;
    public int newTeam;

    public ControlPointCapturedEventArgs(ControlPoint capturedPoint, int oldTeam, int newTeam)
    {
        this.capturedPoint = capturedPoint;
        this.oldTeam = oldTeam;
        this.newTeam = newTeam;
    }
}