using UnityEngine;

public class Fireball : ProjectileAreaEffectSpell
{

    public override float ProjectileSpeed
    {
        get { return 15.0f; }
    }
    public override float Radius
    {
        get { return 5.0f; }
    }

    public override void ApplyEffectsToTarget(GameObject caster, GameObject target, Vector3 effectLocation)
    {
        var stats = target.GetCharacterStats();
        if (stats)
            stats.ApplyDamage(caster, new Damage(35, caster, effectLocation, DamageType.Magical));
    }

    public override string Name
    {
        get { return "Fireball"; }
    }

    public override SpellAffectType AffectType
    {
        get { return SpellAffectType.Enemies; }
    }

    protected override void InitializeSpellValues()
    {
        manaCost = 25;
        castTime = 2.0f;
    }
}