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
        ChangeBaseValue(baseValue);
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

    public virtual void ChangeModifierValue(int value)
    {
        if (this.ModifiedValueChanged != null)
        {
            ModifiedValueChanged(null, new StatChangedEventArgs() { oldValue = ModifiedValue, newValue = ModifiedValue + value });
        }
        this.totalModifiedValue += value;

    }

    public virtual void ChangeBaseValue(int value)
    {
        if (this.BaseValueChanged != null)
        {
            BaseValueChanged(null, new StatChangedEventArgs() { oldValue = baseValue, newValue = baseValue + value });
        }
        this.baseValue += value;
    }
}