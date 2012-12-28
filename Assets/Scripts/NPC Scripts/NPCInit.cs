using UnityEngine;
using System.Collections;

public class NPCInit : MonoBehaviour {

    void Start()
    {
        if (Network.isClient)
        {
            GetComponent<NPCController>().enabled = false;
            GetComponent<NPCCombatController>().enabled = false;
            networkView.RPC("ChangeName", RPCMode.All, "NPC");

        }
            //Server only stuff
        else
        {
            gameObject.AddComponent<DamageReceiver>();
        }
    }
}
