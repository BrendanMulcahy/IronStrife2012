using UnityEngine;
using System.Collections;

public abstract class ProjectileSpell : Spell, ITargetSpell, ISingleEffectSpell
{
    public abstract float ProjectileSpeed { get; }
    public abstract void ApplyEffectsToTarget(GameObject caster, GameObject target, Vector3 effectLocation);

    public void Execute(GameObject caster, Vector3 direction, GameObject homingTarget)
    {
        GameObject projectileObject;
        var prefab = Resources.Load("SpellEffects/" + Name + "Projectile");
        if (prefab)
            projectileObject = Object.Instantiate(prefab) as GameObject;
        else
            projectileObject = new GameObject(GetType().Name + "Projectile");
        projectileObject.transform.position = caster.collider.bounds.center + caster.transform.forward * 1.1f;
        var proj = projectileObject.AddComponent<SpellProjectileCollider>();
        proj.Initialize(caster, homingTarget, direction, ProjectileSpeed);
        proj.spell = this;
    }
}

public class SpellProjectileCollider : MonoBehaviour
{
    SphereCollider sphereCollider;
    protected GameObject caster;
    GameObject homingTarget;
    float initialMagnitude;
    public Spell spell;
    Vector3 direction;
    Vector3 velocity;

    public virtual void Initialize(GameObject caster, GameObject homingTarget, Vector3 direction, float initialSpeed)
    {
        this.caster = caster;
        this.homingTarget = homingTarget;
        this.initialMagnitude = initialSpeed;

        this.velocity = direction * initialSpeed;

        sphereCollider = this.gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = 1.0f; sphereCollider.isTrigger = true;

        var rb = this.gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        this.homingTarget = homingTarget;

        gameObject.layer = 15;
    }

    void Update()
    {
        if (homingTarget)
        {
            var towardsHomingTarget = homingTarget.collider.bounds.center - this.transform.position;
            velocity = Vector3.RotateTowards(velocity, towardsHomingTarget, 50f, 50f).normalized * initialMagnitude;
        }
        transform.position += velocity * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.transform.root == caster.transform.root) return;

        if (other.gameObject.GetCharacterStats())
            CollideWith(other);

        Destroy(this.gameObject);
    }

    protected virtual void CollideWith(Collider other)
    {
        ((ISingleEffectSpell)spell).ApplyEffectsToTarget(caster, other.gameObject, transform.position);
    }
}

public abstract class ProjectileAreaEffectSpell : Spell, ITargetSpell, IAreaEffectSpell
{
    public abstract float ProjectileSpeed { get; }
    public abstract void ApplyEffectsToTarget(GameObject caster, GameObject target, Vector3 effectLocation);
    public abstract float Radius { get; }

    public virtual GameObject ParticleEffect
    {
        get
        {
            var prefab = Resources.Load("SpellEffects/" + this.GetType().Name) as GameObject;
            return prefab;
        }
    }

    public void Execute(GameObject caster, Vector3 direction, GameObject homingTarget)
    {
        GameObject projectileObject;
        var prefab = Resources.Load("SpellEffects/" + Name + "Projectile");
        if (prefab)
            projectileObject = Object.Instantiate(prefab) as GameObject;
        else
            projectileObject = new GameObject(GetType().Name + "Projectile");
        projectileObject.transform.position = caster.collider.bounds.center + caster.transform.forward * 1.1f;

        var proj = projectileObject.AddComponent<AreaSpellProjectileCollider>();
        proj.prefab = ParticleEffect;
        proj.Initialize(caster, homingTarget, direction, ProjectileSpeed);
        proj.spell = this;
    }

}

public class AreaSpellProjectileCollider : SpellProjectileCollider
{
    public GameObject prefab;

    protected override void OnTriggerEnter(Collider other)
    {
        if (caster.transform.root == other.transform.root) return;

        GameObject spellObj;
        if (prefab)
            spellObj = Object.Instantiate(prefab) as GameObject;
        else
        {
            Debug.LogWarning("No prefab found for " + spell.Name + "'s spell effect.");
            var typeName = spell.Name;
            spellObj = new GameObject(caster + "'s " + typeName);
        }
        spellObj.transform.position = this.transform.position;

        var objectsInRange = Physics.OverlapSphere(this.transform.position, ((IAreaEffectSpell)spell).Radius, 1 << 9);
        foreach (Collider collider in objectsInRange)
        {
            ((IAreaEffectSpell)spell).ApplyEffectsToTarget(caster, collider.transform.root.gameObject, this.transform.position);
        }

        Destroy(this.gameObject);
    }
}