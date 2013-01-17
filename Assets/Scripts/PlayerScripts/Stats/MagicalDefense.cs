using UnityEngine;

/// <summary>
/// Represents a character's ability to resist magical attacks.
/// The modified value represents the amount of magical damage that will be absorbed by each source of damage.
/// IE. A value of 45 (%) will allow a player to take only 55% of all magical damage done to him.
/// </summary>
public class MagicalDefense : BuffableStat
{
    public MagicalDefense()
    {
        this.name = "MagicalDefense";
        this.baseValue = 0;
    }

    public MagicalDefense(int amount) : base(amount) { }

    public float PercentageTaken
    {
        get
        {
            return 1f - (ModifiedValue / 100f);
        }
    }

    public float PercentageReduced
    {
        get
        {
            return (ModifiedValue / 100f);
        }
    }
}
