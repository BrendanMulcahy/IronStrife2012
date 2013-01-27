using UnityEngine;

public class Cure : Spell, ISelfSpell
{

    public void Execute(GameObject caster)
    {
        caster.GetCharacterStats().ApplyHealing(caster, 30);
        var particle = GameObject.Instantiate(Resources.Load("Particles/SparkleRising") as GameObject) as GameObject;
        caster.GetCharacterStats().StartCoroutine(Util.TurnOffParticlesInChildren(particle, 1.0f));
        particle.transform.SetParentAndCenter(caster.transform.root);
        particle.transform.localPosition += Vector3.up * 1.7f;
    }

    public override string name
    {
        get { return "Cure"; }
    }

    public override SpellAffectType AffectType
    {
        get { return SpellAffectType.Allies; }
    }

    protected override void InitializeSpellValues()
    {
        manaCost = 15;
        castTime = 1.5f;
    }
}
