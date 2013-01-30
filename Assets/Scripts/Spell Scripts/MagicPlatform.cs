using System;
using UnityEngine;

public class MagicPlatform : Spell, ISelfSpell
{

    public override string Name
    {
        get { return "Magic Platform"; }
    }

    public override SpellAffectType AffectType
    {
        get { return SpellAffectType.None; }
    }

    protected override void InitializeSpellValues()
    {
        manaCost = 40;
        castTime = 0f;
    }

    public void Execute(GameObject caster)
    {
        GameObject platform = GameObject.Instantiate(Resources.Load("SpellEffects/MagicPlatform")) as GameObject;
        platform.transform.position = caster.transform.position + caster.transform.forward * 1f;
        platform.transform.rotation = caster.transform.rotation;
        platform.AddComponent<MagicPlatformEffect>();
    }
}

public class MagicPlatformEffect : MonoBehaviour
{
    bool floating = false;
    float velocity = 2.0f;
    float duration = 10.0f;

    void Start()
    {
        Invoke("StartFloating", 2.0f);
        Destroy(this.gameObject, duration);
    }

    void StartFloating()
    {
        floating = true;
    }

    void Update()
    {
        if (floating)
        {
            var pos = this.transform.position;
            pos.y += velocity * Time.deltaTime;
            this.transform.position = pos;
        }
    }
}