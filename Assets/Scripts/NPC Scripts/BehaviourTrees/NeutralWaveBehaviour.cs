using UnityEngine;
using System.Collections.Generic;
public class NeutralWaveBehaviour : AIBehaviourTreeBuilder
{
    public GameObject Target;

    protected override void GenerateBehaviourTree()
    {
        var assault = this.gameObject.AddComponent<Assault>();
        assault.Target = Target;
        var chase = this.gameObject.AddComponent<Chase>();

        var chaseReqs = new LinkedList<TransitionRequirement>();
        assault.transitions.Add(new StateTransition(chase, chaseReqs));
        chaseReqs.AddLast(new EnemyVisible(this.gameObject));

        var assaultReqs = new LinkedList<TransitionRequirement>();
        chase.transitions.Add(new StateTransition(assault, assaultReqs));
        assaultReqs.AddLast(new EnemyVisibilityLost(this.gameObject));
    }
}