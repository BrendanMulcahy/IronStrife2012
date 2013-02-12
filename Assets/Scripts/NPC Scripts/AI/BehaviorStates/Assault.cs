using UnityEngine;

public class Assault : NPC_BehaviorState
{
    public GameObject Target { get; set; }

    public override void Run()
    {
        
    }

    public override void Enable()
    {
        npcController.SetTarget(Target.transform);
    }

    public override void Disable()
    {

    }
}