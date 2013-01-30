using UnityEngine;
using System.Collections;


    public class Flee : NPC_BehaviorState
    {

        public override void Run()
        {
            npcController.Move();
        }

        public override void Enable()
        {
           // Vector3 awayFromEnemy = transform.position - npcAI.LastSeenEnemy.transform.position;
            //npcController.TargetMoveDirection = awayFromEnemy.normalized;
        }

        public override void Disable()
        {
            //throw new System.NotImplementedException();
        }
    }

