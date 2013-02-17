using System.Collections;
using UnityEngine;

public class RejuvenationBuff : RelicBuff
{
    CharacterStats stats;

    protected override void AddBuffEffects()
    {
        stats = GetComponent<CharacterStats>();
        StartCoroutine("RestoreHealth");
    }

    protected override void RemoveBuffEffects()
    {
        StopCoroutine("RestoreHealth");
    }

    IEnumerator RestoreHealth()
    {
        while (true)
        {
            stats.ApplyHealing(relic.gameObject, (int)(stats.Health.MaxValue * .02f));   //Restore 2% of the player's HP per second.
            yield return new WaitForSeconds(1.0f);
        }
    }
}