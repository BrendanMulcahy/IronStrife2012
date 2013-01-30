using UnityEngine;

public class Surge : Spell, ISelfSpell
{
    public override string Name
    {
        get { return "Surge"; }
    }

    public override SpellAffectType AffectType
    {
        get { return SpellAffectType.Allies; }
    }

    public void Execute(GameObject caster)
    {
        caster.AddComponent<SpeedBuff>().SetSource(caster);
    }

    protected override void InitializeSpellValues()
    {
        this.castTime = 0.0f;
        this.manaCost = 25;
    }
}

public class SpeedBuff : Buff
{
    GameObject trail;

    protected override void AddBuffEffects()
    {
        this.gameObject.GetCharacterStats().MoveSpeed.ChangeModifierValue(5.0f);
        trail = Instantiate(Resources.Load("SpellEffects/SurgeTrail")) as GameObject;
        trail.transform.SetParentAndCenter(gameObject.transform);
        trail.transform.localPosition = new Vector3(0, 1.5f, 0);
    }

    protected override void RemoveBuffEffects()
    {
        this.gameObject.GetCharacterStats().MoveSpeed.ChangeModifierValue(-5.0f);
        Destroy(trail);
    }

    protected override float duration
    {
        get { return 5.0f; }
    }

}