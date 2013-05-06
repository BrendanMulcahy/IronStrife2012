using UnityEngine;
using System.Collections;

public class WeaponCollider : MonoBehaviour 
{
    /// <summary>
    /// Represents whether the weapon is "active" aka it can collide with enemies and deal damage.
    /// </summary>
    public bool isActive = false;
    ArrayList gameObjectsHitThisSwing;
    int teamNumber;
    /// <summary>
    /// Number of targets that can be hit in one swing.
    /// </summary>
    int maxCleaveLevel = 1;
    
    /// <summary>
    /// Number of targets hit in the current swing.
    /// </summary>
    int currentCleavelLevel = 0;

    /// <summary>
    /// Amount damage is reduced for each cleave target.
    /// </summary>
    //float cleaveReduction = .75f;

    void Start () 
    {
        isActive = false;
        gameObjectsHitThisSwing = new ArrayList();
        collider.isTrigger = true;
        gameObject.layer = 17;
        teamNumber = this.gameObject.GetTeamNumber();
    }

    public void StartSwingCollisionChecking()
    {
        isActive = true;
        currentCleavelLevel = 0;
        teamNumber = this.gameObject.GetTeamNumber();
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
                if (other.gameObject.GetTeamNumber() != this.teamNumber)
                {
                    ApplyWeaponHitting(other);
                }
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
            currentCleavelLevel++;
            if (currentCleavelLevel >= maxCleaveLevel)
                StopSwingCollisionChecking();
            DamageReceiver.AddDamageParticle(this.transform.position);
        }
    }

    internal void StopSwingCollisionChecking()
    {
        isActive = false;
    }
}
