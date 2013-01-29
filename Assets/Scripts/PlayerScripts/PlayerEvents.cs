﻿using System;
using UnityEngine;

/// <summary>
/// An event handler for a unit dying.
/// </summary>
/// <param name="deadUnit">The unit that has been killed</param>
/// <param name="e">Unit died event args</param>
public delegate void UnitDiedEventHandler(GameObject deadUnit, UnitDiedEventArgs e);
public delegate void PlayerRespawnedEventHandler(PlayerRespawnedEventArgs e);
public delegate void HealedEventHandler(GameObject sender, HealedEventArgs e);
public delegate void DamageEventHandler(GameObject sender, DamageEventArgs e);

public delegate void StatChangedEventHandler(GameObject sender, StatChangedEventArgs e);
public delegate void FloatChangedEventHandler(GameObject sender, FloatChangedEventArgs e);


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
    public bool handled = false;
    public GameObject healer;
    public int healAmount;
}

public class DamageEventArgs
{
    public bool handled = false;
    public Damage damage;
    public GameObject attacker;
    public Vector3 damageLocation;
}

public class StatChangedEventArgs
{
    public int oldValue;
    public int newValue;
}

public class FloatChangedEventArgs
{
    public float oldValue;
    public float newValue;
}