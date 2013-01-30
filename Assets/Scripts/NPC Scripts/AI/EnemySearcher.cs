using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class EnemySearcher : MonoBehaviour {
	public List<GameObject> charactersNearby = new List<GameObject>();
	public float searchradius = 20f;
	
	// Use this for initialization
	void Start () {
		var collider = this.gameObject.AddComponent<SphereCollider>();
		gameObject.layer = 16;
        collider.isTrigger = true;
		collider.radius = searchradius;
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
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (charactersNearby.Contains (other.gameObject))
		charactersNearby.Remove(other.gameObject);	
	}
	
	
    private IEnumerator CheckForClosestEnemy()
	{
		charactersNearby = charactersNearby.OrderBy(x => (x.transform.position - this.transform.position)).ToList();
		foreach( GameObject go in charactersNearby)
		{
			Debug.Log(go.name);	
		}
		yield return new WaitForSeconds(1.0f);
		
	}
}
