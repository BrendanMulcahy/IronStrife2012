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

    public event StatChangedEventHandler BaseChanged;
    public event StatChangedEventHandler Changed;

    public BuffableStat() { }

    public virtual void ChangeModifierValue(int value)
    {
        if (this.Changed != null)
        {
            Changed(null, new StatChangedEventArgs() { oldValue = ModifiedValue, newValue = ModifiedValue + value });
        }
        this.totalModifiedValue += value;

    }

    public virtual void ChangeBaseValue(int value)
    {
        if (this.BaseChanged != null)
        {
            BaseChanged(null, new StatChangedEventArgs() { oldValue = baseValue, newValue = baseValue + value });
        }
        this.baseValue += value;
    }
}