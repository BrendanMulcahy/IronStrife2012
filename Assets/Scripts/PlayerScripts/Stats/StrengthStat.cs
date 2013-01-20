using UnityEngine;

public class StrengthStat : BuffableStat
{
    /// <summary>
    /// Amount of health gained per point of strength
    /// </summary>
    public const int healthPerStrength = 10;

    /// <summary>
    /// Amount of melee damage increase per strength point
    /// </summary>
    public const int meleeDamagePerStrength = 5;

    public StrengthStat(int amount) : base(amount) { }

    public int DamageModifier
    {
        get
        {
            return meleeDamagePerStrength * ModifiedValue;
        }
    }

}