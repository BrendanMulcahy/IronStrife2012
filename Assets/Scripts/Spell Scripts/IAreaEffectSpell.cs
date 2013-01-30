using UnityEngine;
public interface IAreaEffectSpell
{
    float Radius { get; }
    void ApplyEffectsToTarget(GameObject caster, GameObject target, Vector3 location);
}
