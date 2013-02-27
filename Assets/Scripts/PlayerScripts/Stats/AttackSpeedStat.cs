public class AttackSpeedStat : BuffableStat
{
    public AttackSpeedStat(int amount) : base(amount) { }

    internal void Agility_Changed(UnityEngine.GameObject sender, StatChangedEventArgs e)
    {
        var difference = e.newValue - e.oldValue;
        this.IncrementModifierValue(difference * AgilityStat.attackSpeedPerAgility);
    }
}