using UnityEngine;

public class IntelligenceStat : BuffableStat
{
    /// <summary>
    /// Amount of spell damage given per point of intelligence.
    /// </summary>
    public const int spellDamagePerIntelligence = 5;
    
    /// <summary>
    /// Amount of mana given per point of intelligence
    /// </summary>
    public const int manaPerIntel = 12;

    /// <summary>
    /// Amount to increase casting speed by (percentage based) 
    /// 100% increase will double number of spells cast per unit time
    /// </summary>
    public const float spellCastSpeedPerIntelligence = 2.0f;

    public IntelligenceStat(int amount) : base(amount) { }

    /// <summary>
    /// Returns the total damage modifier that this Intelligence stat gives to spell attacks
    /// </summary>
    /// <returns></returns>
    public int DamageModifier
    {
        get
        {
            return ModifiedValue * spellDamagePerIntelligence;
        }
    }
}