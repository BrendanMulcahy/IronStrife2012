using System.Collections.Generic;
using System.Collections;

using UnityEngine;

public class AreaSpellEffect : MonoBehaviour
{
    public float radius;
    public IAreaEffectSpell spell;
    public GameObject caster;
    List<Transform> targetsHit = new List<Transform>();

    void Start()
    {
        var rigidbody = this.gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        StartCoroutine(DisableInSeconds(.1f));

        var collider = this.gameObject.AddComponent<SphereCollider>();
        collider.radius = radius;
        collider.isTrigger = true;

    }

    private IEnumerator DisableInSeconds(float delay)
    {
        yield return new WaitForSeconds(delay-Time.deltaTime);
        rigidbody.MovePosition(rigidbody.position + Vector3.up * .01f);
        yield return new WaitForSeconds(Time.deltaTime);
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