using UnityEngine;

public class BurningBlade : Spell, ISelfSpell
{

    public override string Name
    {
        get { return "Burning Blade"; }
    }

    public override SpellAffectType AffectType
    {
        get { return SpellAffectType.Allies; }
    }

    protected override void InitializeSpellValues()
    {
        manaCost = 35;
        castTime = 2.5f;
    }

    public void Execute(GameObject caster)
    {
        caster.AddComponent<BurningBladeEffect>().SetSource(caster);
    }
}

public class BurningBladeEffect : Buff
{
    GameObject burnEffect;

    protected override float duration
    {
        get { return 20.0f; }
    }

    protected override void AddBuffEffects()
    {
        var weapon = gameObject.GetInventory().currentWeaponGameobject;
        burnEffect = Instantiate(Resources.Load("SpellEffects/FlameWeaponEffect")) as GameObject;
        burnEffect.transform.parent = weapon.transform;
        burnEffect.transform.localPosition = new Vector3(-5.662436e-07f, -0.05265072f, -6.601214e-06f);
        burnEffect.transform.localRotation = Quaternion.identity;

        ((PlayerStats)gameObject.GetCharacterStats()).Strength.ChangeModifierValue(20);
    }

    protected override void RemoveBuffEffects()
    {
        ((PlayerStats)gameObject.GetCharacterStats()).Strength.ChangeModifierValue(-20);
        if (burnEffect != null && burnEffect.GetComponent<ParticleSystem>()!=null)
        {
            StartCoroutine(FadeParticles(burnEffect.GetComponent<ParticleSystem>()));
        }
    }

    private System.Collections.IEnumerator FadeParticles(ParticleSystem particleSystem, float totalFadeTime = 3.0f)
    {
        float fadeTimeRemaining = totalFadeTime;
        float interval = .25f;
        float initialEmissionRate = particleSystem.emissionRate;
        while (fadeTimeRemaining > 0f)
        {
            particleSystem.emissionRate -= initialEmissionRate * (interval / totalFadeTime);
            yield return new WaitForSeconds(interval);
            fadeTimeRemaining -= interval;
        }
        Destroy(particleSystem.gameObject);
    }
}