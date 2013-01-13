using UnityEngine;
using System.Collections;

namespace Assets.Scripts.NPC_Scripts.AI.TransitionRequirements
{
    class EnemyVisible : TransitionRequirement
    {
        public override bool IsSatisfied()
        {
         //   throw new System.NotImplementedException();
            //check if other object is a character
            //then check if teams are different
            return false;
        }
    }
}