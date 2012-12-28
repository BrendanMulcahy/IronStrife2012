using UnityEngine;

public interface ITargetSpell
{
    void Execute(GameObject caster, Vector3 direction, GameObject homingTarget);
}
