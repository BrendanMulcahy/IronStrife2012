public class Health : RegeneratingStat
{
    public override int CurrentValue
    {
        get
        {
            return _currentValue;
        }
        set
        {
            var temp = _currentValue;
            _currentValue = value;
            OnChanged(this.gameObject, new StatChangedEventArgs() { oldValue = temp, newValue = value });
        }
    }

    public override int MaxValue
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }
}