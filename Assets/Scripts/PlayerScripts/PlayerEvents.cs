using System;
using UnityEngine;

/// <summary>
/// An event handler for a unit dying.
/// </summary>
/// <param name="deadUnit">The unit that has been killed</param>
/// <param name="e">Unit died event args</param>
public delegate void UnitDiedEventHandler(GameObject deadUnit, UnitDiedEventArgs e);
public delegate void PlayerRespawnedEventHandler(PlayerRespawnedEventArgs e);
public delegate void HealedEventHandler(HealedEventArgs e);
public delegate void DamageEventHandler(DamageEventArgs e);

public class UnitDiedEventArgs
{
    public GameObject killer;
    public Vector3 deathPosition;
    public KillReward reward;
}

public class PlayerRespawnedEventArgs
{
    public Vector3 respawnPosition;
}

public class HealedEventArgs
{
    public StrifeGameObject healer;
    public int healAmount;
}

public class DamageEventArgs
{
    public StrifeGameObject attacker;
    public int damageAmount;
}
