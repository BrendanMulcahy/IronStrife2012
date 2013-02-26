using System.Collections;
using UnityEngine;

public class MagicHook : Spell, ITargetSpell
{

    public void Execute(GameObject caster, Vector3 direction, GameObject homingTarget)
    {
        var hookGameObject = new GameObject(caster.name + "'s Hook");
        hookGameObject.transform.position = caster.transform.position + caster.transform.forward * .3f + Vector3.up * 2f;  //Start hook in front of player slightly
        hookGameObject.AddComponent<MagicHookEffect>().Initialize(caster, direction);
    }
}

public class MagicHookEffect : MonoBehaviour
{
    GameObject caster;
    Transform casterTransform;
    Vector3 direction;
    public void Initialize(GameObject caster, Vector3 direction) { this.caster = caster; this.casterTransform = caster.transform; this.direction = direction; }
    float velocity;
    LineRenderer lineRenderer;
    private bool grappled = false;
    float maxGrappleDistance;
    float distanceTraveled = 0;
    Vector3 casterVerticalOffset;
    float grappleSpeed;

    void Start()
    {
        grappleSpeed = 35.0f;
        maxGrappleDistance = 100f;
        velocity = grappleSpeed;
        casterVerticalOffset = Vector3.up * 2.0f;
        var collider = this.gameObject.AddComponent<SphereCollider>();
        var rigidbody = this.gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true; rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        collider.radius = .1f; collider.isTrigger = true;
        var lineRendererGO = Instantiate(Resources.Load("SpellEffects/MagicHook")) as GameObject;
        lineRendererGO.transform.SetParentAndCenter(this.transform);
        lineRenderer = lineRendererGO.GetComponent<LineRenderer>();
    }

    void Update()
    {
        var toMove = direction * velocity * Time.deltaTime;
        transform.position += toMove;
        distanceTraveled += toMove.magnitude;
        lineRenderer.SetPosition(0, casterTransform.position + casterVerticalOffset);
        lineRenderer.SetPosition(1, this.transform.position);

        if (distanceTraveled >= maxGrappleDistance)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root == casterTransform || this.grappled) return;
        this.grappled = true;
        if (other.GetComponent<Terrain>())
        {
            Destroy(this.gameObject);
            return;
        }

        if (other.GetComponent<CharacterController>() || other.tag == "Relic")
            StartCoroutine(BeginPullingObject(other.gameObject));
        else
            StartCoroutine(BeginGrapplingToLocation(this.transform.position));

        this.velocity = 0;
        Destroy(this.collider);
    }

    private IEnumerator BeginGrapplingToLocation(Vector3 grappleLocation)
    {
        caster.SendMessage("HaltGravity", SendMessageOptions.DontRequireReceiver);
        while (Vector3.Distance(casterTransform.position + casterVerticalOffset, grappleLocation) > .75f)
        {
            Vector3 towardsDirection = grappleLocation - (casterTransform.position + casterVerticalOffset);
            casterTransform.position += towardsDirection.normalized * grappleSpeed * Time.deltaTime;
            yield return null;

        }
        caster.SendMessage("ResumeGravity", SendMessageOptions.DontRequireReceiver);

        Destroy(this.gameObject);
        yield break;
    }

    private IEnumerator BeginPullingObject(GameObject other)
    {
        other.SendMessage("HaltGravity", SendMessageOptions.DontRequireReceiver);
        while (Vector3.Distance(casterTransform.position, other.transform.position) > 1f)
        {
            Vector3 towardsDirection = other.transform.position - casterTransform.position;
            other.transform.position -= towardsDirection.normalized * grappleSpeed * Time.deltaTime;
            this.transform.position = other.transform.position + casterVerticalOffset;
            yield return null;
        }
        other.SendMessage("ResumeGravity", SendMessageOptions.DontRequireReceiver);

        Destroy(this.gameObject);
        yield break;
    }


}
