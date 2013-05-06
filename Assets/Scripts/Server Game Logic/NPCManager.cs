using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NPCManager : MonoBehaviour
{
    public List<NPCRecord> NPCs = new List<NPCRecord>();
    public List<NPCSpawnCamp> spawnCamps = new List<NPCSpawnCamp>();
    public const float RESPAWNINTERVAL = 4.0f;
    public const float NEUTRALWAVEINTERVAL = 6.0f;
    public bool debugMessages = false;

    private float lastSpawntime;

    private List<ControlPoint> controlPoints = new List<ControlPoint>();
    public event NeutralWaveEventHandler NeutralWaveSpawned;
    private float lastNeutralWaveSpawnTime = 6.0f;

    private float lastAttackWaveSpawnTime = 6.0f;
    public const float attackWaveInterval = 6.0f;

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
        TrySpawnGuards();
        GameTime.Main.NewDay += Main_NewDay;
    }

    void Main_NewDay()
    {
        lastNeutralWaveSpawnTime -= 24;
        lastSpawntime -= 24;
        lastAttackWaveSpawnTime -= 24;
    }

    void Update()
    {
        if (IsRespawnTime())
        {
            TrySpawnNeutrals();
        }

        if (IsNeutralWaveTime())
        {
            TrySpawnNeutralWaves();
        }

        if (IsAttackWaveTime())
        {
            TrySpawnAttackWaves();
        }
    }

    private bool IsAttackWaveTime()
    {
        if (GameTime.Main.IsNight) return false;

        if (GameTime.CurrentTime - lastAttackWaveSpawnTime >= attackWaveInterval)
        {
            return true;
        }
        return false;
    }

    private void TrySpawnAttackWaves()
    {
        lastAttackWaveSpawnTime = GameTime.CurrentTime;

        foreach (ControlPoint cp in controlPoints)
        {
            cp.SpawnAttackWaves();
        }
    }

    void OnNeutralWaveSpawn()
    {
        if (NeutralWaveSpawned != null)
        {
            NeutralWaveSpawned(new NeutralWaveEventArgs());
        }
    }

    internal void TrySpawnNeutralWaves()
    {
        lastNeutralWaveSpawnTime = GameTime.CurrentTime;

        foreach (ControlPoint cp in controlPoints)
        {
            cp.SpawnNeutralWave();
        }
    }

    private bool IsNeutralWaveTime()
    {
        if (GameTime.Main.IsNight) return false;

        if (GameTime.CurrentTime - lastNeutralWaveSpawnTime >= NEUTRALWAVEINTERVAL)
        {
            return true;
        }
        return false;
    }

    private void TrySpawnNeutrals()
    {
        lastSpawntime = GameTime.CurrentTime; ;

        foreach (NPCSpawnCamp camp in spawnCamps)
        {
            camp.SpawnNeutrals();
        }
    }

    private void TrySpawnGuards()
    {
        foreach (ControlPoint cp in controlPoints)
        {
            cp.SpawnGuards();
        }
    }

    internal void AddControlPoint(ControlPoint controlPoint)
    {
        controlPoints.Add(controlPoint);
    }

    /// <summary>
    /// Server-side method for allocating a new NPC
    /// </summary>
    /// <param name="type"></param>
    /// <param name="location"></param>
    /// <param name="zone"></param>
    public GameObject ServerSpawnNPC(string type, Vector3 location, Quaternion rotation = new Quaternion())
    {
        var pos = Util.SampleFloorIncludingObjects(location);
        bool collision = true;
        while (collision)
        {
            if (Physics.CheckSphere(pos, 1.0f, 1 << 9))
            {
                pos = Util.SampleFloorIncludingObjects(pos + new Vector3(Random.Range(-.5f, .5f), 0, Random.Range(-.5f, .5f)).normalized * .2f);
            }
            else { collision = false;}
        }

        GameObject newNPC = GameObject.Instantiate(Resources.Load("NPCs/" + type), pos, rotation) as GameObject;
        NetworkViewID animationID = Network.AllocateViewID();
        NetworkViewID transformID = Network.AllocateViewID();
        newNPC.GetComponents<NetworkView>()[0].viewID = transformID;
        newNPC.GetComponents<NetworkView>()[1].viewID = animationID;

        newNPC.GetCharacterStats().Died += NPC_Died;
        NPCs.Add(new NPCRecord()
        {
            type = type,
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
            return true;
        }

        return false;
    }

    void SynchronizePlayer(NetworkPlayer player)
    {
        foreach (NPCRecord record in NPCs)
        {
            MessageTerminal.Main.networkView.RPCToGroup("SpawnNPC", 2, player, record.type, record.gameObject.transform.position, record.animationViewID, record.transformViewID);
        }
    }
}

public class NPCRecord
{
    public string type;
    public GameObject gameObject;
    public NetworkViewID animationViewID;
    public NetworkViewID transformViewID;
}

public delegate void NeutralWaveEventHandler(NeutralWaveEventArgs e);

public class NeutralWaveEventArgs
{
    public GameObject target;
}