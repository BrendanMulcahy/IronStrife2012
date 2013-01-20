using System.Collections.Generic;
using UnityEngine;

[PlayerComponent(PlayerScriptType.AllEnabled)]
public class DamageNumbers : MonoBehaviour
{
    List<GameObject> floatingNumbers;

    void Start()
    {
        gameObject.GetCharacterStats().Damaged += DamageNumbers_Damaged;
    }

    void DamageNumbers_Damaged(GameObject sender, DamageEventArgs e)
    {
        AddFloatingDamageNumber(e);
    }

    private void AddFloatingDamageNumber(DamageEventArgs e)
    {
        var go = new GameObject(e.damage.ToString());
        go.transform.SetParentAndCenter(this.transform);
        var fdn = go.AddComponent<FloatingDamageNumber>();
        fdn.damage = e.damage;
        fdn.target = this.transform;
    

    }
}