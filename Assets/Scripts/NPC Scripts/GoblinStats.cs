using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.NPC_Scripts
{
    public class GoblinStats : NPCStats
    {
        protected override void Start()
        {
            base.Start();
            reward = new KillReward(400, 1000);
            attackRange = 2.3f;
        }

        protected override void Awake()
        {
            base.Awake();

            var genericAnimation = GetComponent<NPCGenericAnimation>();
        }

        public override int PhysicalDamageModifier { get { return 12; } }

    }
}
