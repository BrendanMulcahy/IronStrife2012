﻿using UnityEngine;

/// <summary>
/// Represents a character's ability to resist physical attacks.
/// The modified value represents the amount of physical damage that will be absorbed by each attack.
/// IE. A value of 45 (%) will allow a player to take only 55% of all physical damage done to him.
/// </summary>
public class PhysicalDefense : BuffableStat
{
    public PhysicalDefense()
    {
        this.name = "Armor";
        this.baseValue = 0;
    }
}
