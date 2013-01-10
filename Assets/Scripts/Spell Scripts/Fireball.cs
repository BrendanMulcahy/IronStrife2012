using UnityEngine;
using System.Collections.Generic;

public class Fireball : Spell, ITargetSpell
{
    public void Execute(GameObject caster, Vector3 direction, GameObject homingTarget)
    {
        GameObject fireBallObject = GameObject.Instantiate(Resources.Load("SpellEffects/Fireball")) as GameObject;
        fireBallObject.name = caster.name + "'s Fireball";
        fireBallObject.transform.position = caster.transform.position + caster.transform.forward * 3f + Vector3.up*2f;
        var fireballEffect = fireBallObject.AddComponent<FireballEffect>();
        fireballEffect.Initialize(direction, 15.0f, caster);
    }

    public override string name
    {
        get { return "Fireball"; }
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

public class FireballEffect : Projectile
{
    SphereCollider sphereCollider;
    GameObject caster;

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
    public void Initialize(Vector3 direction, float velocity, GameObject caster)
    {
        this.velocity = direction * velocity; 
        this.moveDirection = direction;
        this.caster = caster;
        creator = caster.transform;
        sphereCollider = this.gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = 1.0f; sphereCollider.isTrigger = true;
        var rb = this.gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true; rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        StartCoroutine(Util.DestroyInSeconds(this.gameObject, 5.0f));
    }
    public override void CollideWith(Collider other)
    {
        if (other.transform.root != caster.transform.root) 
        Explode();
    }

    void Explode()
    {
        var graphic = new GameObject("Fireball Explosion");
        graphic.AddComponent<DetonatorFireball>();
        graphic.transform.position = this.transform.position;
        this.velocity = Vector3.zero;

        sphereCollider.radius = 3.0f;
        this.gameObject.AddComponent<FireballExplosionEffect>().caster = caster;
        
        Destroy(this);
    }
}

public class FireballExplosionEffect : MonoBehaviour
{
    List<GameObject> targetsHit = new List<GameObject>();
    public GameObject caster;

    void Start()
    {
        StartCoroutine(Util.DestroyInSeconds(this.gameObject, 1.0f));
    }

    void OnTriggerEnter(Collider other)
    {
        var targetToHit = other.transform.root.gameObject;
        if (!targetsHit.Contains(targetToHit))
        {
            targetsHit.Add(targetToHit);
            var dr = targetToHit.GetDamageReceiver();
            if (dr)
                dr.ApplyHit(caster);
            var stats = targetToHit.GetCharacterStats();
            if (stats)
                stats.ApplyDamage(caster, 35);
        }
    }
}
