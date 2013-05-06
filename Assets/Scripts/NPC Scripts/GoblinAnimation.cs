using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.NPC_Scripts
{
    class GoblinAnimation : NPCGenericAnimation
    {
        protected override void SetAnimationNames()
        {
            this.aniNames[AnimationName.Attack] = "sword attack";
            this.aniNames[AnimationName.Die] = "die hard";
            this.aniNames[AnimationName.GetHit] = "hit front";
            this.aniNames[AnimationName.Run] = "run fast";
            this.aniNames[AnimationName.Idle] = "idle";
        }
    }
}
