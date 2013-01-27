﻿using UnityEngine;

public class Flameburst : PointAreaSpell
{
    public override void ApplyEffectsToTarget(GameObject caster, GameObject target)
    {
        var targetStats = target.GetCharacterStats();
        if (targetStats)
        {
            var casterStats = caster.GetCharacterStats();
            var spellDamage = casterStats.Intelligence.DamageModifier;
            var totalDamage = (int)(spellDamage * (1.33f));
            targetStats.ApplyDamage(caster, new Damage(totalDamage, caster, DamageType.Magical));
        }
    }

    public override string name
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
        get { return 4.0f; }
    }
}
