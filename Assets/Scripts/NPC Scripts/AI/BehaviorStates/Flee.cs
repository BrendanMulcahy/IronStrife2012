using UnityEngine;
using System.Collections;

namespace Assets.Scripts.NPC_Scripts.AI.BehaviorStates
{
    public class Flee : NPC_BehaviorState
    {
        void Start()
        {
            npcAI = GetComponent<NPC_AI>();
            npcController = GetComponent<NPC_Controller>();
            npcStats = GetComponent<NPCStats>();
        }


        public override void Run()
        {
            npcController.Move();
        }

        public override void Enable()
        {
            Vector3 awayFromEnemy = transform.position - npcAI.LastSeenEnemy.transform.position;
            npcController.TargetMoveDirection = awayFromEnemy.normalized;
        }

        public override void Disable()
        {
            throw new System.NotImplementedException();
        }
    }
}
