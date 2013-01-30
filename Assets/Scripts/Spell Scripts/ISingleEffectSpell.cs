using UnityEngine;

public interface ISingleEffectSpell {
    void ApplyEffectsToTarget(GameObject caster, GameObject target, Vector3 effectLocation);
}