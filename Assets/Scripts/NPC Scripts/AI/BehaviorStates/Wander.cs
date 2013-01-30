using UnityEngine;
using System.Collections;
using System.Collections.Generic;



    public class Wander : NPC_BehaviorState
    {


	protected override void Start()
	{
		base.Start();
    }

        public override void Run()
        {
            npcController.Move();
        }
	
	  public override void Enable()
        {
			StartCoroutine(CheckIfContinueWandering());
            //throw new System.NotImplementedException();
        }

        public override void Disable()
        {
           StopAllCoroutines();
        }

        /// <summary>
        /// Randomly decides if the NPC should wait briefly before walking more
        /// </summary>
        /// <returns>true if the AI should wait, false otherwise</returns>
        private bool ShouldWaitBriefly()
        {
            if (Random.Range(0.0f, 1.0f) > 0.6f)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                    npcController.TargetMoveDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
                    npcController.MoveSpeed = npcAI.WalkSpeed;
                }
                yield return new WaitForSeconds(5.0f);
            }
        }
    }
