using UnityEngine;
using System.Collections;

/// <summary>
/// Class that acts as an interface for physical and magical attacks to apply their damage and effects to a player's game object.
/// </summary>
public class DamageReceiver : MonoBehaviour {

    protected CharacterStats characterStats;
    protected PlayerMotor playerMotor;
    protected Inventory inventory;

    public virtual void Start()
    {
        characterStats = gameObject.GetCharacterStats();
        inventory = gameObject.GetInventory();
        playerMotor = gameObject.GetPlayerMotor();
    }

    public virtual void ApplyHit(GameObject attacker)
    {
        playerMotor.ApplyForce(new Force((transform.position - attacker.transform.position).normalized/5 * attacker.GetCharacterStats().EffectiveStrength, .25f));
        characterStats.ApplyDamage(attacker, attacker.GetCharacterStats().EffectiveStrength);
    }
}
