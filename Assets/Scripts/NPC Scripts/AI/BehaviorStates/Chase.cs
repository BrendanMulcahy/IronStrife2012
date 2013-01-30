﻿using UnityEngine;
using System.Collections;

public class Chase : NPC_BehaviorState
{
    NPCStats stats;
    bool isAttacking = false;

	protected override void Start()
	{
		base.Start();
        stats = GetComponent<NPCStats>();
	}

    public override void Run()
    {
        if (IsInRangeToAttack() && !isAttacking)
        {

            StartSwinging();
        }
        else if (!isAttacking)
        {
            if (npcAI.Searcher.charactersNearby.Count > 0)
            {
                var target = npcAI.Searcher.charactersNearby[0];
                npcController.TargetMoveDirection = (target.transform.position - this.transform.position);
                npcController.MoveSpeed = npcAI.WalkSpeed;
                npcController.Move();
            }
        }
        else
        {
            npcController.TargetMoveDirection = new Vector3();
            npcController.MoveSpeed = 0;
        }
    }

    private void StartSwinging()
    {
        isAttacking = true;
        npcController.BeginSwingAttack();
    }

    void StopAttacking()
    {
        isAttacking = false;
    }

    bool IsInRangeToAttack()
    {
        if (npcAI.Searcher.charactersNearby.Count > 0)
        {
            return (Vector3.Distance(npcAI.Searcher.charactersNearby[0].transform.position, this.transform.position) < stats.attackRange);
        }
        return false;
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

