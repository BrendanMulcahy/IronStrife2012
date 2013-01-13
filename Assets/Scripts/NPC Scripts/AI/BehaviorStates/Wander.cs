using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.NPC_Scripts.AI.TransitionRequirements;

namespace Assets.Scripts.NPC_Scripts.AI.BehaviorStates
{
    public class Wander : NPC_BehaviorState
    {


        void Start()
        {
            npcAI = GetComponent<NPC_AI>();
            npcController = GetComponent<NPC_Controller>();
            npcStats = GetComponent<NPCStats>();

            LinkedList<TransitionRequirement> fleeRequirements = new LinkedList<TransitionRequirement>();
            fleeRequirements.AddLast(new LowHealth(npcStats));
            transitions.Add(new StateTransition(GetComponent<Flee>(), fleeRequirements));

            LinkedList<TransitionRequirement> chaseRequirements = new LinkedList<TransitionRequirement>();
            chaseRequirements.AddLast(new EnemyVisible());
            transitions.Add(new StateTransition(GetComponent<Chase>(), chaseRequirements));

            StartCoroutine(CheckIfContinueWandering());
        }

        public override void Run()
        {
            npcController.Move();
        }

        /// <summary>
        /// Randomly decides if the NPC should wait briefly before walking more
        /// </summary>
        /// <returns>true if the AI should wait, false otherwise</returns>
        private bool ShouldWaitBriefly()
        {
            if (Random.Range(0.0f, 1.0f) > 0.9f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Enable()
        {
            throw new System.NotImplementedException();
        }

        public override void Disable()
        {
           // throw new System.NotImplementedException();
        }

        private IEnumerator CheckIfContinueWandering()
        {
            while (true)
            {
                if (ShouldWaitBriefly())
                {
                    npcController.MoveSpeed = 0.0f;
                }
                else
                {
                    npcController.TargetMoveDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized;
                    npcController.MoveSpeed = npcAI.WalkSpeed;
                }
                yield return new WaitForSeconds(5.0f);
            }
        }
    }
}