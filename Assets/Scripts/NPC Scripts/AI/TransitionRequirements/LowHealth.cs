using UnityEngine;
using System.Collections;


    public class LowHealth : TransitionRequirement
    {
        private const float LOWHEALTH_THRESHOLD = 0.3f;

        public LowHealth(NPCStats npcStats)
        {
            this.npcStats = npcStats;
        }

        public override bool IsSatisfied()
        {
            if (npcStats.Health.CurrentPercentage < LOWHEALTH_THRESHOLD)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

