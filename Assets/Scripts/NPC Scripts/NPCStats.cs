using UnityEngine;
using System;

public class NPCStats : CharacterStats
{
    public float attackRange = 1.5f;
    public float attackDuration = 1.5f;

    protected override void Start()
    {
        base.Start();
        if (GetComponent<DamageReceiver>() == null)
            this.gameObject.AddComponent<DamageReceiver>();
        if (!this.GetComponentInChildren<DamageNumbers>())
            this.gameObject.AddComponent<DamageNumbers>();
        if (!this.GetComponentInChildren<PlayerMotor>())
            this.gameObject.AddComponent<PlayerMotor>();
        this.Died += NPCStats_Died;
    }

    void NPCStats_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        Destroy(this.gameObject, 4.0f);

    }

}
