using UnityEngine;

public class AgilityStat : BuffableStat
{
    /// <summary>
    /// Value to increase movespeed per agility
    /// </summary>
    public const float moveSpeedPerAgility = .2f;

    /// <summary>
    /// Percentage to increase swing speed per agility. 100% increase would double attacks per second
    /// </summary>
    public const int attackSpeedPerAgility = 1;
    /// <summary>
    /// The amount of maximum stamina a player gains per point of agility
    /// </summary>
    public const int staminaPerAgility = 5;

    /// <summary>
    /// The amount of ranged damage given by Agility per point
    /// </summary>
    public const int rangedDamagePerAgility = 1;

    public AgilityStat(int amount) : base(amount) { }
}
