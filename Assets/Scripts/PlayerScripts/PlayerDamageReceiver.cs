using UnityEngine;
using System.Collections;

/// <summary>
/// Class that acts as an interface for physical and magical attacks to apply their damage and effects to a player's game object.
/// </summary>
public class PlayerDamageReceiver : DamageReceiver {

    private ThirdPersonController controller;
    public bool isClientView;

    public override void Start()
    {
        base.Start();
        controller = gameObject.GetComponent<ThirdPersonController>();
    }

    /// <summary>
    /// Called when a player is being hit by an enemy unit.
    /// </summary>
    /// <param name="attacker"></param>
    public override void ApplyHit(GameObject attacker)
    {
        var effectiveStrength = attacker.GetCharacterStats().EffectiveStrength;
        if (controller.IsDefending)
        {
            effectiveStrength -= inventory.currentShield.blockAmount;
            effectiveStrength = Mathf.Max(0, effectiveStrength);
        }
        playerMotor.ApplyForce(new Force((transform.position - attacker.transform.position).normalized/5 * effectiveStrength, .25f));
        characterStats.ApplyDamage(attacker, effectiveStrength);
    }

}
