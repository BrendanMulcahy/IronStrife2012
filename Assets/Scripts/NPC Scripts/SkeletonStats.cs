﻿using UnityEngine;

public class SkeletonStats : NPCStats
{
    protected override void Start()
    {
        base.Start();
        Died += SkeletonStats_Died;
        reward = new KillReward(500, 500);
    }

    void SkeletonStats_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        RewardPlayersInArea(e.deathPosition, e.killer, e.reward);
        ShowDyingAnimation();
        Destroy(this.GetComponent<NPC_AI>());
    }

    void ShowDyingAnimation()
    {
        this.animation.Play("die");
        Destroy(this.gameObject, 4.0f);
    }
}