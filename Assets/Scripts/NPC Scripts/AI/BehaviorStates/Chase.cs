using UnityEngine;
using System.Collections;

public class Chase : NPC_BehaviorState
{

	protected override void Start()
	{
		base.Start();
		
	}

    public override void Run()
    {
		if (npcAI.Searcher.charactersNearby.Count > 0)
		{
			var target = npcAI.Searcher.charactersNearby[0];
			npcController.TargetMoveDirection = (target.transform.position - this.transform.position);
			npcController.MoveSpeed = npcAI.WalkSpeed;
			npcController.Move();
		}
    }

    public override void Enable()
    {
        //throw new System.NotImplementedException();
    }

    public override void Disable()
    {
        //throw new System.NotImplementedException();
    }


	
}

