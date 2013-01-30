using UnityEngine;

public class Flameburst : PointAreaSpell
{
    public override void ApplyEffectsToTarget(GameObject caster, GameObject target, Vector3 location)
    {
        var targetStats = target.GetCharacterStats();
        if (targetStats)
        {
            var casterStats = caster.GetCharacterStats();
            var spellDamage = casterStats.Intelligence.DamageModifier;
            var totalDamage = (int)(spellDamage * (1.33f));
            targetStats.ApplyDamage(caster, new Damage(totalDamage, caster, location, DamageType.Magical));
        }
    }

    public override string Name
    {
        get { return "Flameburst"; }
    }

    public override SpellAffectType AffectType
    {
        get { return SpellAffectType.Enemies; }
    }

    protected override void InitializeSpellValues()
    {
        manaCost = 25;
        castTime = 1.5f;
    }

    public override float Radius
    {
        get { return 15.0f; }
    }
}
