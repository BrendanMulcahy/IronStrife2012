using UnityEngine;
using System.Collections;

public class CloakOfTheArchThief : RelicBuff {

    float amountIncreased;
    protected override void AddBuffEffects()
    {
        amountIncreased = this.gameObject.GetCharacterStats().MoveSpeed.ModifiedValue * .25f;
        Stats.MoveSpeed.IncrementModifierValue(amountIncreased);
    }

    protected override void RemoveBuffEffects()
    {
        Stats.MoveSpeed.IncrementModifierValue(-amountIncreased);
    }
}
