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
            if (npcStats.Health.CurrentValue < LOWHEALTH_THRESHOLD * npcStats.Health.MaxValue)
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
