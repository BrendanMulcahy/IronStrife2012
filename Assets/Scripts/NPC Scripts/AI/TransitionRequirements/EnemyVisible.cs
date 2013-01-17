using UnityEngine;
using System.Collections.Generic;

public class EnemyVisible : TransitionRequirement
{
    public List<GameObject> charactersNearby = new List<GameObject>();

    public GameObject gameObject;
    public float searchRadius = 20f;

    public EnemyVisible(GameObject gameObject)
    {
        this.gameObject = gameObject;
        AddEnemySearcher();
    }

    private void AddEnemySearcher()
    {
        var sphereGO = new GameObject("EnemySearcher");
        sphereGO.transform.SetParentAndCenter(gameObject.transform);
        var searcher = sphereGO.AddComponent<EnemySearcher>();
        searcher.enemyVisible = this;
        var collider = sphereGO.AddComponent<SphereCollider>();
        collider.radius = this.searchRadius;
        collider.isTrigger = true;
    }

    public override bool IsSatisfied()
    {
        if (charactersNearby.Count > 0)
        {
            return true;
        }
        return false;
    }
}