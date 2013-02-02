using UnityEngine;

public class Mana : RegeneratingStat
{
    public void Intelligence_Changed(GameObject sender, StatChangedEventArgs e)
    {
        var difference = e.newValue - e.oldValue;
        float currentPercentage = (float)CurrentValue / MaxValue;
        this.SetMaxValue(Mathf.Max(1, MaxValue + (difference * IntelligenceStat.manaPerIntel)));

        //Retain percentage on stat gain (dota-esque)
        this.SetCurrentValue((int)(this.MaxValue * currentPercentage));
    }

}
