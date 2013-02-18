using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCSpawnCamp : MonoBehaviour {

    public GameObject neutralType;
    //public Collider spawnZone;
    public string enemyType = "SkeletonGuard";
    public int amountToSpawn = 3;

    private LinkedList<GameObject> charactersWithinArea = new LinkedList<GameObject>();


	// Use this for initialization
	private void Start () {
        collider.isTrigger = true;
        this.gameObject.layer = 16; //puts this on the PlayerSearch layer
	}
	
	// Update is called once per frame
	private void Update () {

	}



    /// <summary>
    /// Checks if this camp can spawn a new set of neutral units
    /// </summary>
    private bool CanSpawn()
    {
        bool canSpawn = charactersWithinArea.Count == 0;
        return canSpawn;
    }

    /// <summary>
    /// Spawns new neutral units in the camp of the type and amount specified
    /// </summary>
    public void SpawnNeutrals()
    {
        if (CanSpawn())
        {
            for (int i = amountToSpawn; i > 0; i--)
            {
                Vector3 offset = new Vector3(Random.Range(-2.0f, 2.0f), 0,  Random.Range(-2.0f, 2.0f));
                NPCManager.Main.ServerSpawnNPC(enemyType, transform.position + offset);
            }
        }
    }

    //Keep track of the npcs and players in the area
    private void OnTriggerEnter(Collider collider)
    {
        AddNearbyUnit(collider);
    }

    private void AddNearbyUnit(Collider collider)
    {
        charactersWithinArea.AddLast(collider.gameObject);
        collider.gameObject.GetCharacterStats().Died += NPCSpawnCamp_Died;
    }

    void NPCSpawnCamp_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        RemoveFromList(deadUnit);
    }

    //Keep track of the npcs and players in the area
    private void OnTriggerExit(Collider collider)
    {
        RemoveFromList(collider.gameObject);
    }

    private void RemoveFromList(GameObject go)
    {
        if (charactersWithinArea.Contains(go.gameObject))
        {
            charactersWithinArea.Remove(go.gameObject);
        }
    }
}
