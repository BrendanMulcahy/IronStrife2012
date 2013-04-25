using UnityEngine;
using System.Collections;

public class BloodswornBlade : RelicBuff {

    protected override void AddBuffEffects()
    {
        ((PlayerStats)this.gameObject.GetCharacterStats()).Strength.IncrementModifierValue(20);
    }

    protected override void RemoveBuffEffects()
    {
        ((PlayerStats)this.gameObject.GetCharacterStats()).Strength.IncrementModifierValue(-20);
    }
}
