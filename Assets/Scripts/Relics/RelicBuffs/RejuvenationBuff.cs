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
            stats.ApplyHealing(relic.gameObject, (int)(stats.Health.MaxValue * .1f));   //Restore 10% of the player's HP per 5 seconds.
            yield return new WaitForSeconds(5.0f);
        }
    }
}