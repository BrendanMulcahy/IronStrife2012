using UnityEngine;

[PlayerComponent(PlayerScriptType.AllEnabled)]
public class PlayerDamageReceiver : DamageReceiver
{
    public override void Start()
    {
        base.Start();
        ((PlayerStats)characterStats).Respawned += PlayerDamageReceiver_Respawned;
    }

    void PlayerDamageReceiver_Respawned(PlayerRespawnedEventArgs e)
    {
        this.enabled = true;
    }
}