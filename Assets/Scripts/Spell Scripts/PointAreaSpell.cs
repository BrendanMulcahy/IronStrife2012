using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PointAreaSpell : Spell, IPointSpell, IAreaEffectSpell
{
    public abstract void ApplyEffectsToTarget(GameObject caster, GameObject target, Vector3 location);

    public virtual GameObject ParticleEffect
    {
        get
        {
            var prefab = Resources.Load("SpellEffects/" + this.GetType().Name) as GameObject;
            return prefab;
        }
    }

    public void Execute(GameObject caster, Vector3 targetPoint)
    {
        var prefab = this.ParticleEffect;
        GameObject spellObj;
        if (prefab)
            spellObj = Object.Instantiate(prefab) as GameObject;
        else
        {
            var typeName = this.GetType().Name;
            spellObj = new GameObject(caster + "'s " + typeName);
        }
        spellObj.transform.position = targetPoint;

        var casterTeam = caster.GetTeamNumber();

        var objectsInRange = Physics.OverlapSphere(targetPoint, this.Radius, 1 << 9);
        foreach (Collider other in objectsInRange)
        {
            switch (AffectType)
            {

                case SpellAffectType.All:
                    ApplyEffectsToTarget(caster, other.transform.root.gameObject, targetPoint);
                    break;

                case SpellAffectType.Enemies:
                    if (other.gameObject.GetTeamNumber() != casterTeam)
                        ApplyEffectsToTarget(caster, other.transform.root.gameObject, targetPoint);
                    break;

                case SpellAffectType.Neutrals:
                    if (other.gameObject.GetTeamNumber() == 0)
                        ApplyEffectsToTarget(caster, other.transform.root.gameObject, targetPoint);
                    break;
                case SpellAffectType.Allies:
                    if (other.gameObject.GetTeamNumber() == casterTeam)
                        ApplyEffectsToTarget(caster, other.transform.root.gameObject, targetPoint);
                    break;
            }
        }
    }

    public abstract float Radius { get; }
}