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
        gameObjectsHitThisSwing.Add(other.transform.root);
        Debug.Log(other.transform.root.gameObject.name + " has been hit now.");
        DamageReceiver dr;
        if (dr = other.transform.root.gameObject.GetDamageReceiver())
        {
            dr.ApplyHit(transform.root.gameObject);
            PlayerSoundGenerator sound = transform.root.gameObject.GetComponent<PlayerSoundGenerator>();
            if (sound != null)
                transform.root.gameObject.GetComponent<PlayerSoundGenerator>().PlaySwingAttackHitSound();
        }
    }
}
