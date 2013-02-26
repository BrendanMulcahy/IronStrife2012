using UnityEngine;

public class FrostBlast : PointAreaSpell
{
    public override void ApplyEffectsToTarget(GameObject caster, GameObject target, Vector3 location)
    {
        var damage = 5 + ((PlayerStats)caster.GetCharacterStats()).Intelligence.ModifiedValue * IntelligenceStat.spellDamagePerIntelligence;
        var stats = target.GetCharacterStats();
        if (stats)
        {
            target.GetCharacterStats().ApplyDamage(caster, new Damage(damage, caster, location, DamageType.Magical));

            target.AddComponent<FrostSlow>();
        }
    }
}

public class FrostSlow : Buff
{
    protected override float duration
    {
        get { return 5f; }
    }

    protected override void AddBuffEffects()
    {
        this.gameObject.GetCharacterStats().MoveSpeed.IncrementModifierValue(-5f);
    }

    protected override void RemoveBuffEffects()
    {
        this.gameObject.GetCharacterStats().MoveSpeed.IncrementModifierValue(5f);
    }

    protected override bool DuplicateBuffAllowed()
    {
        return false;
    }
}
