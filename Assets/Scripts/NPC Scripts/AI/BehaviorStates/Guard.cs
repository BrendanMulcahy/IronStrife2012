using UnityEngine;

public class Guard : NPC_BehaviorState
{

    public Vector3 guardLocation;

    public override void Run()
    {
        if (Vector3.Distance(guardLocation, this.transform.position) > .1f)
        {
            npcController.TargetMoveDirection = guardLocation - this.transform.position;
            npcController.Move();
        }
        else
        {
            npcController.TargetMoveDirection = new Vector3();
        }
    }

    public override void Enable()
    {

    }

    public override void Disable()
    {

    }
}