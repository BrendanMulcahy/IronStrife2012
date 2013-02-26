using UnityEngine;

public class Flameburst : PointAreaSpell
{
    public override void ApplyEffectsToTarget(GameObject caster, GameObject target, Vector3 location)
    {
        var targetStats = target.GetCharacterStats();
        if (targetStats)
        {
            var casterStats = caster.GetCharacterStats() as PlayerStats;
            var spellDamage = casterStats.Intelligence.DamageModifier;
            var totalDamage = (int)(spellDamage * (1.33f));
            targetStats.ApplyDamage(caster, new Damage(totalDamage, caster, location, DamageType.Magical));
        }
    }

}
