using System.Collections.Generic;
using UnityEngine;

public class NPCManager
{
    public List<NPCRecord> NPCs = new List<NPCRecord>();

    public static NPCManager Main
    {
        get
        {
            return MasterGameLogic.Main.NPCManager;
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
}

public class NPCRecord
{
    public GameObject gameObject;
    public NetworkViewID animationViewID;
    public NetworkViewID transformViewID;
}