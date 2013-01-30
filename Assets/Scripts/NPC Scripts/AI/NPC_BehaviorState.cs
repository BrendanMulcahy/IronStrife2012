using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NPC_BehaviorState : MonoBehaviour
{
    protected NPC_AI npcAI;
    protected NPC_Controller npcController;
    protected NPCStats npcStats;
    public List<StateTransition> transitions = new List<StateTransition>();

    public abstract void Run();
    public abstract void Enable();
    public abstract void Disable();
//    public abstract NPC_BehaviorState NextState();
	
	protected virtual void Start()
	{
		npcController = GetComponent<NPC_Controller>();
		npcAI = GetComponent<NPC_AI>();
		npcStats = GetComponent<NPCStats>();
	}

}
