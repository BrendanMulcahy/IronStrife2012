using System.Collections.Generic;
using UnityEngine;

public class NPCManager
{
    public List<NPCRecord> NPCs = new List<NPCRecord>();

    /// <summary>
    /// Server-side method for allocating a new NPC
    /// </summary>
    /// <param name="type"></param>
    /// <param name="location"></param>
    /// <param name="zone"></param>
    public GameObject ServerSpawnNPC(string type, Vector3 location, NPCSpawnZone zone)
    {
        GameObject newNPC = GameObject.Instantiate(Resources.Load(type)) as GameObject;
        newNPC.transform.position = Util.SampleFloorIncludingObjects(location);
        NetworkViewID animationID = Network.AllocateViewID();
        NetworkViewID transformID = Network.AllocateViewID();
        newNPC.GetComponents<NetworkView>()[0].viewID = animationID;
        newNPC.GetComponents<NetworkView>()[1].viewID = transformID;

        newNPC.GetCharacterStats().Died += NPC_Died;
        NPCs.Add(new NPCRecord()
        {
            gameObject = newNPC,
            zone = zone,
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
}

public class NPCRecord
{
    public GameObject gameObject;
    public NPCSpawnZone zone;
    public NetworkViewID animationViewID;
    public NetworkViewID transformViewID;
}