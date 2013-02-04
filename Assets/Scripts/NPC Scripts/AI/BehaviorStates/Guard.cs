using UnityEngine;

public class Guard : NPC_BehaviorState
{

    public Vector3 guardLocation;

    public override void Run()
    {
        if (Vector3.Distance(guardLocation, this.transform.position) > .1f)
        {
            npcController.SetTarget(guardLocation);
        }
        else
        {

        }
    }

    public override void Enable()
    {

    }

    public override void Disable()
    {

    }
}