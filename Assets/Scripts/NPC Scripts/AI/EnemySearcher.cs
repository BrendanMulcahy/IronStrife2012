using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


public class EnemySearcher : MonoBehaviour {
	public List<GameObject> enemiesNearby = new List<GameObject>();
	public float searchradius = 20f;

    private int team;
	
	// Use this for initialization
	void Awake () {
        if (!collider)
        {
            var sphere = this.gameObject.AddComponent<SphereCollider>();
            gameObject.layer = 16;
            sphere.isTrigger = true;
            sphere.radius = searchradius;
        }
        team = transform.root.gameObject.GetCharacterStats().TeamNumber;
		StartCoroutine(CheckForClosestEnemy());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<CharacterStats>() && (other.transform.root != this.transform.root))
		{
            if (other.gameObject.GetCharacterStats().TeamNumber != this.team)
            {
                Debug.Log(other.gameObject.name + "'s team is " + other.gameObject.GetCharacterStats().TeamNumber);
                enemiesNearby.Add(other.gameObject);
                other.gameObject.GetCharacterStats().Died += EnemySearcher_Died;
            }
		}
	}

    void EnemySearcher_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        if (enemiesNearby.Contains(deadUnit.gameObject))
            enemiesNearby.Remove(deadUnit.gameObject);	
    }
	
	void OnTriggerExit(Collider other)
	{
		if (enemiesNearby.Contains (other.gameObject))
		enemiesNearby.Remove(other.gameObject);	
	}
	
	
    private IEnumerator CheckForClosestEnemy()
	{
		while (true)
		{
			enemiesNearby = enemiesNearby.OrderBy(x => ( x ? x.transform.position : new Vector3() - this.transform.position).magnitude).ToList();
			yield return new WaitForSeconds(1.0f);
		}
		
	}
}
