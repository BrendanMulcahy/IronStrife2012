using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPCManager : MonoBehaviour
{
    public List<NPCRecord> NPCs = new List<NPCRecord>();
    public List<NPCSpawnCamp> spawnCamps = new List<NPCSpawnCamp>();
    public const float RESPAWNINTERVAL = 4.0f;
    public bool debugMessages = false;

    private float lastSpawntime;

    public static NPCManager Main
    {
        get
        {
            return MasterGameLogic.Main.NPCManager;
        }
    }

    void Start()
    {
        spawnCamps = FindObjectsOfType(typeof(NPCSpawnCamp)).Cast<NPCSpawnCamp>().ToList();
        lastSpawntime = GameTime.CurrentTime;
        TrySpawnNeutrals();
    }

    void Update()
    {
        if (IsRespawnTime())
        {
            TrySpawnNeutrals();
        }
    }

    private void TrySpawnNeutrals()
    {
        foreach (NPCSpawnCamp camp in spawnCamps)
        {
            camp.SpawnNeutrals();
        }
    }

    /// <summary>
    /// Server-side method for allocating a new NPC
    /// </summary>
    /// <param name="type"></param>
    /// <param name="location"></param>
    /// <param name="zone"></param>
    public GameObject ServerSpawnNPC(string type, Vector3 location)
    {
        GameObject newNPC = GameObject.Instantiate(Resources.Load("NPCs/" + type)) as GameObject;
        newNPC.transform.position = Util.SampleFloorIncludingObjects(location);
        NetworkViewID animationID = Network.AllocateViewID();
        NetworkViewID transformID = Network.AllocateViewID();
        newNPC.GetComponents<NetworkView>()[0].viewID = transformID;
        newNPC.GetComponents<NetworkView>()[1].viewID = animationID;

        newNPC.GetCharacterStats().Died += NPC_Died;
        NPCs.Add(new NPCRecord()
        {
            gameObject = newNPC,
            animationViewID = animationID,
            transformViewID = transformID
        });
        MessageTerminal.Main.networkView.RPC("SpawnNPC", RPCMode.Others, type, location, animationID, transformID);
        return newNPC;
    }

    void NPC_Died(GameObject sender, UnitDiedEventArgs e)
    {
        NPCs.Remove(FindRecord(sender));
    }

    private NPCRecord FindRecord(GameObject go)
    {
        foreach (NPCRecord rec in NPCs)
        {
            if (rec.gameObject == go)
                return rec;
            
        }
        return null;
    }

    /// <summary>
    /// Checks the current game time to see if it is time to respawn the neutral units in this camp
    /// </summary>
    private bool IsRespawnTime()
    {
        float currentTime = GameTime.CurrentTime;
        if (currentTime - lastSpawntime >= RESPAWNINTERVAL)
        {
            if (debugMessages) { Debug.Log("It is time to spawn neutrals: " + GameTime.CurrentTime); }
            lastSpawntime = currentTime;
            return true;
        }

        return false;
    }
}

public class NPCRecord
{
    public GameObject gameObject;
    public NetworkViewID animationViewID;
    public NetworkViewID transformViewID;
}