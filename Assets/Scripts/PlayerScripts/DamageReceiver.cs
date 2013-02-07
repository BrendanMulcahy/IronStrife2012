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
        characterStats.Died += characterStats_Died;
    }

    void characterStats_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        this.enabled = false;
    }

    public virtual void ApplyHit(GameObject attacker)
    {
        if (this.enabled)
        {
            var attackerStats = attacker.GetCharacterStats();
            var totalDamageMod = attackerStats.PhysicalDamageModifier;

            var damage = new Damage(totalDamageMod, attacker, attacker.transform.position, DamageType.Physical);


            characterStats.ApplyDamage(attacker, damage);
        }
    }
}
