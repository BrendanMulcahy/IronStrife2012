using UnityEngine;
using System.Collections;

namespace Assets.Scripts.NPC_Scripts.AI.TransitionRequirements
{
    class LowHealth : TransitionRequirement
    {
        private const float LOWHEALTH_THRESHOLD = 0.33f;

        public LowHealth(NPCStats npcStats)
        {
            this.npcStats = npcStats;
        }

        public override bool IsSatisfied()
        {
            if (npcStats.Health < LOWHEALTH_THRESHOLD * npcStats.MaxHealth)
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
