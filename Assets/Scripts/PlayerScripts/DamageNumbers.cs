using System.Collections.Generic;
using UnityEngine;

[PlayerComponent(PlayerScriptType.AllEnabled)]
public class DamageNumbers : MonoBehaviour
{
    List<GameObject> floatingNumbers;

    void Start()
    {
        gameObject.GetCharacterStats().Damaged += DamageNumbers_Damaged;
        gameObject.GetCharacterStats().Healed += DamageNumbers_Healed;
    }

    void DamageNumbers_Healed(GameObject sender, HealedEventArgs e)
    {
        if (e == null) Debug.Log("ITS NULL LOLOLOZZ");
        AddFloatingHealNumber(e);
    }

    void DamageNumbers_Damaged(GameObject sender, DamageEventArgs e)
    {
        if (e == null) Debug.Log("ITS NULL LOLOLOZZ");
        AddFloatingDamageNumber(e);
    }

    private void AddFloatingDamageNumber(DamageEventArgs e)
    {
        var go = new GameObject(e.damage.ToString());
        go.transform.SetParentAndCenter(this.transform);
        var fdn = go.AddComponent<FloatingDamageNumber>();
        fdn.amount = e.damage.amount;
        fdn.target = this.transform;
    }

    private void AddFloatingHealNumber(HealedEventArgs e)
    {
        var go = new GameObject(e.healAmount.ToString());
        go.transform.SetParentAndCenter(this.transform);
        var fdn = go.AddComponent<FloatingHealNumber>();
        fdn.amount = e.healAmount;
        fdn.target = this.transform;
    }
}