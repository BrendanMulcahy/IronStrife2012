using UnityEngine;
using System;

public class NPCStats : CharacterStats
{
    public float attackRange = 1.5f;
    public float attackLength = 1.5f;

    protected override void OnDeath(UnitDiedEventArgs unitDiedEventArgs)
    {
        base.OnDeath(unitDiedEventArgs);
    }
}
