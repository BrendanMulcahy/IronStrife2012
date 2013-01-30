using UnityEngine;
using System.Collections;


public class NormalHealth : TransitionRequirement {
	
	private const float NORMALHEALTH_THRESHOLD = 0.5f;
	
	public NormalHealth (NPCStats npcStats)
	{
		this.npcStats = npcStats;
	}
	
	public override bool IsSatisfied()
        {
            if (npcStats.Health.CurrentPercentage > NORMALHEALTH_THRESHOLD)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
}
