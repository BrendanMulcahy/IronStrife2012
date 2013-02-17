using UnityEngine;

public class GuardStats : NPCStats
{
    protected override void Start()
    {
        base.Start();
        Health.SetInitialValues(250, 250);
        PhysicalDefense.IncrementBaseValue(25);
        MagicalDefense.IncrementBaseValue(15);
        MoveSpeed.IncrementBaseValue(10.0f - MoveSpeed.baseValue);

        reward = new KillReward(2000, 1000);
        attackRange = 2.3f;
        teamNumber = 1;

        ThirdPersonSimpleAnimation.InitializeAnimation(animation);
        var genericAnimation = GetComponent<NPCGenericAnimation>();
        genericAnimation.aniNames[NPCGenericAnimation.AnimationName.Idle] = "Idle01";
        genericAnimation.aniNames[NPCGenericAnimation.AnimationName.Attack] = "Combo4";
        genericAnimation.aniNames[NPCGenericAnimation.AnimationName.Die] = "Death";
        genericAnimation.aniNames[NPCGenericAnimation.AnimationName.Run] = "Run";
        genericAnimation.aniNames[NPCGenericAnimation.AnimationName.GetHit] = "IdleShieldBlock";


        Died += GuardStats_Died;
    }

    public override int PhysicalDamageModifier { get { return 25; } }

    void GuardStats_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        ShowDyingAnimation();
        Destroy(this.gameObject, 10f);
    }

    private void ShowDyingAnimation()
    {

    }

}