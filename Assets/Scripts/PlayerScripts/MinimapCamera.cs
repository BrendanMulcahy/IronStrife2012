using UnityEngine;
using System.Collections;

public class MinimapCamera : MonoBehaviour {

    Transform target;
    float height = 800;
    bool dead = false;

    void Start () {

    }

    void LateUpdate () {
        if (target != null && !dead)
        {
            transform.position = target.transform.position;
            var pos = transform.position;
            pos.y += height;
            transform.position = pos;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        gameObject.GetComponent<Camera>().enabled = true;
        this.enabled = true;

        newTarget.gameObject.GetCharacterStats().Died += MinimapCamera_Died;
        ((PlayerStats)newTarget.gameObject.GetCharacterStats()).Respawned += MinimapCamera_Respawned;
    }

    void MinimapCamera_Respawned(PlayerRespawnedEventArgs e)
    {
        dead = false;
    }

    void MinimapCamera_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        dead = true;
    }
}
