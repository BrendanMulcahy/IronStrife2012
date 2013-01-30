using UnityEngine;

public class Stamina : RegeneratingStat
{
    public void Agility_Changed(GameObject sender, StatChangedEventArgs e)
    {
        var difference = e.newValue - e.oldValue;
        float currentPercentage = (float)CurrentValue / MaxValue;
        this.MaxValue = Mathf.Max(1, CurrentValue + (difference * AgilityStat.staminaPerAgility));

        //Retain percentage on stat gain (dota-esque)
        this.CurrentValue = (int)(this.MaxValue * currentPercentage);
    }
}
