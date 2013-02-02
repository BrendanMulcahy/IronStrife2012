using System.Collections.Generic;
using UnityEngine;

public class SkeletonGuard : MonoBehaviour
{
    void Start()
    {
        if (Network.isClient) return;

        var guard = this.gameObject.AddComponent<Guard>();
        guard.guardLocation = this.transform.position;
        var chase = this.gameObject.AddComponent<Chase>();

        LinkedList<TransitionRequirement> chaseRequirement = new LinkedList<TransitionRequirement>();
        guard.transitions.Add(new StateTransition(chase, chaseRequirement));
        chaseRequirement.AddLast(new EnemyVisible(this.gameObject));
        chaseRequirement.AddLast(new ProximityToLocation(this.transform.position, this.transform, 15f));

        LinkedList<TransitionRequirement> chaseToGuardRequirement = new LinkedList<TransitionRequirement>();
        chase.transitions.Add(new StateTransition(guard, chaseToGuardRequirement));
        chaseToGuardRequirement.AddLast(new DistanceFromLocation(this.transform.position, this.transform, 15f));


        //set initial state to wander
        this.GetComponent<NPC_AI>().SetInitialState(guard);
    }
}