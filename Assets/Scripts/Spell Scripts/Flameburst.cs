using UnityEngine;
using System;
using System.Collections.Generic;

public class Flameburst : Spell, IPointSpell
{
    public void Execute(GameObject caster, Vector3 targetPoint)
    {
        //caster.networkView.RPC("SpawnSpellEffectAtPosition", RPCMode.Others, "Flameburst", targetPoint);
        var spellObj = new GameObject(caster.name + "'s Flameburst");
        spellObj.transform.position = targetPoint;
        spellObj.AddComponent<FlameburstEffect>().SetCaster(caster);
    }

    public override string name
    {
        get { return "Flameburst"; }
    }

    public override SpellAffectType AffectType
    {
        get { return SpellAffectType.Enemies; }
    }

    protected override void InitializeSpellValues()
    {
        manaCost = 25;
        castTime = 2.0f;
    }
}

public class FlameburstEffect : MonoBehaviour
{
    public float radius = 4.0f;
    SphereCollider sphereCollider;
    float forceMagnitude = 25.0f;
    int damage = 35;
    GameObject caster;
    List<Transform> targetsHit = new List<Transform>();

    void Start()
    {
        (Instantiate(new GameObject(), transform.position, Quaternion.identity) as GameObject).AddComponent<DetonatorShockwave>();

        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = radius;
        sphereCollider.isTrigger = true;
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        CharacterStats cs;
        if ((cs = other.gameObject.GetCharacterStats()) != null && !targetsHit.Contains(other.transform.root))
        {
            cs.ApplyDamage(caster, damage);
            var force = new Force((other.transform.position - transform.position).normalized*forceMagnitude, .4f);
            other.gameObject.GetPlayerMotor().ApplyForce(force);
            other.gameObject.GetPlayerMotor().ApplyForce(new Force(Vector3.up * forceMagnitude * 1f, .4f));
            targetsHit.Add(other.transform.root);
        }
    }


    internal void SetCaster(GameObject caster)
    {
        this.caster = caster;
    }
}