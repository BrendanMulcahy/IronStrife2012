﻿using UnityEngine;
using System.Collections;


    public class LowMana : TransitionRequirement
    {
        private const float LOWMANA_THRESHOLD = 0.33f;

        public LowMana(NPCStats npcStats)
        {
            this.npcStats = npcStats;
        }

        public override bool IsSatisfied()
        {
            if (npcStats.Mana.CurrentValue < LOWMANA_THRESHOLD * npcStats.Mana.MaxValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
