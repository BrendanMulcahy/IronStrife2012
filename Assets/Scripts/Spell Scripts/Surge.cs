using UnityEngine;

public class Surge : Spell, ISelfSpell
{
    public void Execute(GameObject caster)
    {
        caster.AddComponent<SpeedBuff>().SetSource(caster);
    }

}

public class SpeedBuff : Buff
{
    GameObject trail;

    protected override void AddBuffEffects()
    {
        this.gameObject.GetCharacterStats().MoveSpeed.IncrementModifierValue(5.0f);
        trail = Instantiate(Resources.Load("SpellEffects/SurgeTrail")) as GameObject;
        trail.transform.SetParentAndCenter(gameObject.transform);
        trail.transform.localPosition = new Vector3(0, 1.5f, 0);
    }

    protected override void RemoveBuffEffects()
    {
        this.gameObject.GetCharacterStats().MoveSpeed.IncrementModifierValue(-5.0f);
        Destroy(trail);
    }

    protected override float duration
    {
        get { return 5.0f; }
    }

    protected override bool DuplicateBuffAllowed()
    {
        return true;
    }

}