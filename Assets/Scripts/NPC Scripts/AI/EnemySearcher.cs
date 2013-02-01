using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


public class EnemySearcher : MonoBehaviour {
	public List<GameObject> charactersNearby = new List<GameObject>();
	public float searchradius = 20f;
	
	// Use this for initialization
	void Awake () {
        if (!collider)
        {
            var sphere = this.gameObject.AddComponent<SphereCollider>();
            gameObject.layer = 16;
            sphere.isTrigger = true;
            sphere.radius = searchradius;
        }
		StartCoroutine(CheckForClosestEnemy());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<CharacterStats>() && (other.transform.root != this.transform.root))
		{
			charactersNearby.Add(other.gameObject);
            other.gameObject.GetCharacterStats().Died += EnemySearcher_Died;
		}
	}

    void EnemySearcher_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        if (charactersNearby.Contains(deadUnit.gameObject))
            charactersNearby.Remove(deadUnit.gameObject);	
    }
	
	void OnTriggerExit(Collider other)
	{
		if (charactersNearby.Contains (other.gameObject))
		charactersNearby.Remove(other.gameObject);	
	}
	
	
    private IEnumerator CheckForClosestEnemy()
	{
		while (true)
		{
			charactersNearby = charactersNearby.OrderBy(x => ( x ? x.transform.position : new Vector3() - this.transform.position).magnitude).ToList();
			yield return new WaitForSeconds(1.0f);
		}
		
	}
}
