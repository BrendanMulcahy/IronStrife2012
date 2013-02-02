using UnityEngine;

public class Health : RegeneratingStat
{
    public void Strength_Changed(GameObject sender, StatChangedEventArgs e)
    {
        var difference = e.newValue - e.oldValue;
        float currentPercentage = (float)CurrentValue / MaxValue;
        this.SetMaxValue(Mathf.Max(1, MaxValue + (difference * StrengthStat.healthPerStrength)));

        //Retain percentage on stat gain (dota-esque)
        this.SetCurrentValue((int)(this.MaxValue * currentPercentage));
    }
}