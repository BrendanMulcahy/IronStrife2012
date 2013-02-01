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

        var objectsInRange = Physics.OverlapSphere(targetPoint, this.Radius, 1 << 9);
        foreach (Collider other in objectsInRange)
        {
            ApplyEffectsToTarget(caster, other.transform.root.gameObject, targetPoint);
        }
    }

    public abstract float Radius { get; }
}