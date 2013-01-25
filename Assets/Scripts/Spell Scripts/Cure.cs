using UnityEngine;

public class Cure : Spell, ISelfSpell
{

    public void Execute(GameObject caster)
    {
        caster.GetCharacterStats().CureHealth(30);
        var particle = GameObject.Instantiate(Resources.Load("Particles/SparkleRising") as GameObject) as GameObject;

        particle.transform.SetParentAndCenter(caster.transform.root);
        particle.transform.localPosition += Vector3.up * 1.7f;
        Util.DestroyInSeconds(particle, 3.0f);
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
