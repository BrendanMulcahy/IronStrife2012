public class BuffableStat
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

    public BuffableStat(string name, int baseValue)
    {
        this.name = name;
        this.baseValue = baseValue;
    }

    public virtual void Modify(int value)
    {
        this.totalModifiedValue += value;
    }
}