using UnityEngine;
using System.Collections.Generic;

public class Skeleton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Network.isClient) return;
		var npcStats = this.gameObject.GetComponent<NPCStats>();
		
		var wander = this.gameObject.AddComponent<Wander>();
		var flee = this.gameObject.AddComponent<Flee>();
		var chase = this.gameObject.AddComponent<Chase>();
		
        LinkedList<TransitionRequirement> fleeRequirements = new LinkedList<TransitionRequirement>();
        fleeRequirements.AddLast(new LowHealth(npcStats));
        wander.transitions.Add(new StateTransition(GetComponent<Flee>(), fleeRequirements));
		chase.transitions.Add(new StateTransition(GetComponent<Flee>(), fleeRequirements));

        LinkedList<TransitionRequirement> wandertochaseRequirements = new LinkedList<TransitionRequirement>();
        wandertochaseRequirements.AddLast(new EnemyVisible(this.gameObject));
        wander.transitions.Add(new StateTransition(GetComponent<Chase>(), wandertochaseRequirements));
		
		LinkedList<TransitionRequirement> fleetowanderRequirements = new LinkedList<TransitionRequirement>();
		fleetowanderRequirements.AddLast(new NormalHealth(npcStats));
		flee.transitions.Add(new StateTransition(GetComponent<Wander>(), fleetowanderRequirements));
		
		LinkedList<TransitionRequirement> chasetowanderRequirements = new LinkedList<TransitionRequirement>();
		chasetowanderRequirements.AddLast(new EnemyVisibilityLost(this.gameObject));
		chase.transitions.Add(new StateTransition(GetComponent<Wander>(), chasetowanderRequirements));
		
		//set initial state to wander
		this.GetComponent<NPC_AI>().SetInitialState(wander);
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
