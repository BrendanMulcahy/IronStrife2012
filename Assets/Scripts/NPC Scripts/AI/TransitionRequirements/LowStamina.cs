﻿using UnityEngine;
using System.Collections;


    public class LowStamina : TransitionRequirement
    {
        private const float LOWSTAMINA_THRESHOLD = 0.33f;
        
        public LowStamina(NPCStats npcStats)
        {
            this.npcStats = npcStats;
        }

        public override bool IsSatisfied()
        {
            if (npcStats.Stamina.CurrentValue < LOWSTAMINA_THRESHOLD * npcStats.Stamina.MaxValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
