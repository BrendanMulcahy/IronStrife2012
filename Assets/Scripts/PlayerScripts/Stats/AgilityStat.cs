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
    public const float attackSpeedPerAgility = 2.0f;
    public const int staminaPerAgility = 5;

    public AgilityStat(int amount) : base(amount) { }
}
