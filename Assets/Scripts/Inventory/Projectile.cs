using UnityEngine;
using System.Collections;

/// <summary>
/// The base class for projectile attacks and other effects
/// </summary>
public class Projectile : MonoBehaviour {

    protected Vector3 moveDirection;
    protected float velocityMagnitude;
    protected Vector3 velocity;
    protected float gravity = 3.0f;
    protected Transform creator;
    protected GameObject stuckIn;

    /// <summary>
    /// Gives the projectile its initial velocity and direction, as well as its creator.
    /// </summary>
    /// <param name="moveDirection">The direction to move. Normalized.</param>
    /// <param name="velocity">The velocity to move at</param>
    /// <param name="creator">The creator of the projectile</param>
    public void InitializeProjectile(Vector3 moveDirection, float velocity, Transform creator)
    {
        this.moveDirection = moveDirection.normalized;
        this.velocityMagnitude = velocity;
        this.velocity = moveDirection * velocity;
        transform.rotation = Quaternion.LookRotation(moveDirection);
        this.creator = creator;
    }

    void Awake()
    {
        Util.SetLayerRecursively(transform.root.gameObject, 15);
    }

	// Update is called once per frame
	void Update () {
        velocity.y -= gravity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(velocity);
        transform.position += transform.forward * velocity.magnitude * Time.deltaTime;
	}

    void OnTriggerStay(Collider other)
    {
        if (!other.GetComponent<Projectile>() && other.transform.root != creator.transform.root)
        CollideWith(other);
    }

    /// <summary>
    /// Called when this projectile hits another object.
    /// </summary>
    /// <param name="other"></param>
    public virtual void CollideWith(Collider other)
    {
        collider.enabled = false;
        var dr = other.transform.root.GetComponent<DamageReceiver>();
        if (dr!=null)
            dr.ApplyRangedHit(creator.gameObject);
        this.enabled = false;
        this.stuckIn = other.gameObject;
        this.transform.parent = other.transform;
        Destroy(this.gameObject, 30.0f);
    }
}
