using UnityEngine;

public class Health : RegeneratingStat
{
    public void Strength_Changed(GameObject sender, StatChangedEventArgs e)
    {
        var difference = e.newValue - e.oldValue;
        float currentPercentage = (float)CurrentValue / MaxValue;
        this.MaxValue = Mathf.Max(1, CurrentValue + (difference * StrengthStat.healthPerStrength));

        //Retain percentage on stat gain (dota-esque)
        this.CurrentValue = (int)(this.MaxValue * currentPercentage);
    }
}