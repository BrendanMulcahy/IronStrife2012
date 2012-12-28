using UnityEngine;
using System.Collections;

/// <summary>
/// Class for arrow projectiles used with bow attacks
/// </summary>
public class Arrow : Projectile
{
    public override void CollideWith(Collider other)
    {
        base.CollideWith(other);
        transform.GetComponentInChildren<TrailRenderer>().enabled = false;
    }
}

