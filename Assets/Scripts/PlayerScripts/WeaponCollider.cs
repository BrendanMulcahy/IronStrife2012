using UnityEngine;
using System.Collections;

public class WeaponCollider : MonoBehaviour 
{
    /// <summary>
    /// Represents whether the weapon is "active" aka it can collide with enemies and deal damage.
    /// </summary>
    public bool isActive = false;
    ArrayList gameObjectsHitThisSwing;

    void Start () 
    {
        isActive = false;
        gameObjectsHitThisSwing = new ArrayList();
        collider.isTrigger = true;
        gameObject.layer = 17;
    }

    public void StartSwingCollisionChecking()
    {
        isActive = true;
        gameObjectsHitThisSwing = new ArrayList();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            if (other.transform.root == this.transform.root)
            {
                return;
            }

            if (!gameObjectsHitThisSwing.Contains(other.transform.root))
            {
                ApplyWeaponHitting(other);
            }
        }
    }

    private void ApplyWeaponHitting(Collider other)
    {
        PlayerSoundGenerator sound = transform.root.GetComponent<PlayerSoundGenerator>();
        if (sound != null)
        {
            transform.root.gameObject.GetComponent<PlayerSoundGenerator>().PlaySwingAttackHitSound(other);
        }
        gameObjectsHitThisSwing.Add(other.transform.root);
        DamageReceiver dr;
        if (dr = other.transform.root.gameObject.GetDamageReceiver())
        {
            dr.ApplyHit(transform.root.gameObject);
        }
    }

    internal void StopSwingCollisionChecking()
    {
        isActive = false;
    }
}
