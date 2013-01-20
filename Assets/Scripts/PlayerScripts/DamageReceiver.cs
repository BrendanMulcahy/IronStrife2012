using UnityEngine;
using System.Collections;

[PlayerComponent(PlayerScriptType.AllEnabled)]
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
        var attackerStats = attacker.GetCharacterStats();
        var strengthMod = attackerStats.Strength.DamageModifier;
        var weaponMod = attacker.GetInventory().currentWeapon.damage;

        var damage = new Damage(strengthMod + weaponMod, attacker, DamageType.Physical);


        characterStats.ApplyDamage(attacker, damage);
    }
}
