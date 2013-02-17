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

    public override float Radius
    {
        get { return 10.0f; }
    }

    public override string Name
    {
        get { return "Frost Blast"; }
    }

    public override SpellAffectType AffectType
    {
        get { return SpellAffectType.Enemies; }
    }

    protected override void InitializeSpellValues()
    {
        manaCost = 35;
        castTime = 1.0f;
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
}
