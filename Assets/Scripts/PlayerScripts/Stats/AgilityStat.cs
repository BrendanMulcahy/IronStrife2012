using UnityEngine;

public class AgilityStat : BuffableStat
{
    /// <summary>
    /// Value to increase movespeed per agility
    /// </summary>
    public const float moveSpeedPerAgility = 1.0f;

    /// <summary>
    /// Percentage to increase swing speed per agility. 100% increase would double attacks per second
    /// </summary>
    public const float attackSpeedPerAgility = 2.0f;

    public AgilityStat(int amount) : base(amount) { }
}
