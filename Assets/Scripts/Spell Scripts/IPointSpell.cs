using UnityEngine;

public interface IPointSpell
{
    void Execute(GameObject caster, Vector3 targetPoint);

    float Radius { get; }
}