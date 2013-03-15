using UnityEngine;

public class SkeletonStats : NPCStats
{
    protected override void Start()
    {
        base.Start();
        Died += SkeletonStats_Died;
        reward = new KillReward(500, 500);
        attackRange = 2.3f;
        TeamNumber = 0;

    }

    protected override void Awake()
    {
        base.Awake(); 

        var genericAnimation = GetComponent<NPCGenericAnimation>();
        genericAnimation.aniNames[NPCGenericAnimation.AnimationName.Idle] = "idle";
        genericAnimation.aniNames[NPCGenericAnimation.AnimationName.Run] = "run";
        genericAnimation.aniNames[NPCGenericAnimation.AnimationName.Attack] = "attack";
        genericAnimation.aniNames[NPCGenericAnimation.AnimationName.Die] = "die";
        genericAnimation.aniNames[NPCGenericAnimation.AnimationName.GetHit] = "gethit";
    }

    public override int PhysicalDamageModifier { get { return 15; } }

    void SkeletonStats_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        ShowDyingAnimation();
        Destroy(this.GetComponent<NPC_AI>());
    }

    void ShowDyingAnimation()
    {
        this.animation.Play("die"); 
        Destroy(this.gameObject, 4.0f);
    }
}