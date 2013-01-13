using UnityEngine;
using System.Collections;

namespace Assets.Scripts.NPC_Scripts.AI.TransitionRequirements
{
    class LowStamina : TransitionRequirement
    {
        private const float LOWSTAMINA_THRESHOLD = 0.33f;
        
        public LowStamina(NPCStats npcStats)
        {
            this.npcStats = npcStats;
        }

        public override bool IsSatisfied()
        {
            if (npcStats.Stamina < LOWSTAMINA_THRESHOLD * npcStats.MaxStamina)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}