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


}