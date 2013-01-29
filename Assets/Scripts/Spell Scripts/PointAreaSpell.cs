using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PointAreaSpell : Spell, IPointSpell
{
    public abstract float Radius { get; }
    public abstract void ApplyEffectsToTarget(GameObject caster, GameObject target, Vector3 location);

    public virtual GameObject ParticleEffect
    {
        get
        {
            var prefab = Resources.Load("SpellEffects/" + this.GetType().Name) as GameObject;
            return prefab;
        }
    }

    public void Execute(GameObject caster, Vector3 targetPoint)
    {
        var prefab = this.ParticleEffect;
        GameObject spellObj;
        if (prefab)
            spellObj = Object.Instantiate(prefab) as GameObject;
        else
        {
            var typeName = this.GetType().Name;
            spellObj = new GameObject(caster + "'s " + typeName);
        }
        spellObj.transform.position = targetPoint;
        var effect = spellObj.AddComponent<PointAreaSpellEffect>();
        effect.radius = Radius;
        effect.spell = this;
        effect.caster = caster;
    }
}

public class PointAreaSpellEffect : MonoBehaviour
{
    public float radius;
    public PointAreaSpell spell;
    public GameObject caster;
    List<Transform> targetsHit = new List<Transform>();

    void Start()
    {
        var collider = this.gameObject.AddComponent<SphereCollider>();
        collider.radius = radius;
        collider.isTrigger = true;
        var rigidbody = this.gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        StartCoroutine(DisableNextFrame());
    }

    private IEnumerator DisableNextFrame()
    {
        yield return null;
        this.collider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!targetsHit.Contains(other.transform.root))
        {// TODO : Check spell affect type and make sure it only hits allies or enemies, or both
            targetsHit.Add(other.transform.root);
            spell.ApplyEffectsToTarget(caster, other.transform.root.gameObject, this.transform.position);
        }
    }
}