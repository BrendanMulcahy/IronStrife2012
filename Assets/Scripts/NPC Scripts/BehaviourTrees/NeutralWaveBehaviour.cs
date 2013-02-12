using UnityEngine;

public class NeutralWaveBehaviour : AIBehaviourTreeBuilder
{
    protected override void GenerateBehaviourTree()
    {
        var assault = this.gameObject.AddComponent<Assault>();
    }
}