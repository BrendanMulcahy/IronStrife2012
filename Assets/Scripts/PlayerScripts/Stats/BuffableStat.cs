[System.Serializable]
public abstract class BuffableStat
{
    public int baseValue;
    protected int totalModifiedValue;
    public int ModifiedValue
    {
        get
        {
            return baseValue + totalModifiedValue;
        }
    }
    public string name;

    public BuffableStat(int baseValue)
    {
        name = this.GetType().Name;
        this.baseValue = 0;
        IncrementBaseValue(baseValue);
    }

    /// <summary>
    /// Event that is fired when the base value of this Stat is changed
    /// </summary>
    public event StatChangedEventHandler BaseValueChanged;
    /// <summary>
    /// Event that is fired when the Modified value of this Stat is changed. Does not fire when the base value changes
    /// </summary>
    public event StatChangedEventHandler ModifiedValueChanged;

    public BuffableStat() { }

    /// <summary>
    /// Increments the modifier value of this stat by the given amount.
    /// </summary>
    /// <param name="value"></param>
    public virtual void IncrementModifierValue(int value)
    {
        if (this.ModifiedValueChanged != null)
        {
            ModifiedValueChanged(null, new StatChangedEventArgs() { oldValue = ModifiedValue, newValue = ModifiedValue + value });
        }
        this.totalModifiedValue += value;

    }

    /// <summary>
    /// Increments the base value of this stat by the given amount.
    /// </summary>
    /// <param name="value"></param>
    public virtual void IncrementBaseValue(int value)
    {
        if (this.BaseValueChanged != null)
        {
            BaseValueChanged(null, new StatChangedEventArgs() { oldValue = baseValue, newValue = baseValue + value });
        }
        this.baseValue += value;
    }

    public override string ToString()
    {
        return GetType().Name + ": " + baseValue + " + " + totalModifiedValue;
    }
}