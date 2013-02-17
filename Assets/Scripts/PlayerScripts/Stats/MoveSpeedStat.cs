public class MoveSpeedStat //Doesnt extend buffable stat because MoveSpeed is a float
{
        public float baseValue;
    protected float totalModifiedValue;
    public float ModifiedValue
    {
        get
        {
            return baseValue + totalModifiedValue;
        }
    }
    public string name;

    public MoveSpeedStat(float baseValue)
    {
        name = this.GetType().Name;
        this.baseValue = 0;
        IncrementBaseValue(baseValue);
    }

    public event FloatChangedEventHandler BaseChanged;
    public event FloatChangedEventHandler Changed;

    public MoveSpeedStat() { }

    public virtual void IncrementModifierValue(float value)
    {
        if (this.Changed != null)
        {
            Changed(null, new FloatChangedEventArgs() { oldValue = ModifiedValue, newValue = ModifiedValue + value });
        }
        this.totalModifiedValue += value;

    }

    public virtual void IncrementBaseValue(float value)
    {
        if (this.BaseChanged != null)
        {
            BaseChanged(null, new FloatChangedEventArgs() { oldValue = baseValue, newValue = baseValue + value });
        }
        this.baseValue += value;
    }

    internal void Agility_Changed(UnityEngine.GameObject sender, StatChangedEventArgs e)
    {
        var difference = e.newValue - e.oldValue;
        this.IncrementModifierValue(difference * AgilityStat.moveSpeedPerAgility);
    }

    public override string ToString()
    {
        return "Move Speed: " + baseValue + " + " + totalModifiedValue;
    }
}