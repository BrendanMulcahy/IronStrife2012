using UnityEngine;

public class SkeletonStats : NPCStats
{
    protected override void Start()
    {
        base.Start();
        Died += SkeletonStats_Died;
        reward = new KillReward(500, 500);
        attackRange = 1.5f;
        teamNumber = 0;
    }

    protected override void Awake()
    {
        base.Awake();

    }

    public override int PhysicalDamageModifier { get { return 15; } }

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