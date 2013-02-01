using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCSpawnCamp : MonoBehaviour {

    public GameObject neutralType;
    //public Collider spawnZone;
    public const float RESPAWNINTERVAL = 4.0f;
    public bool debug = false;
    public int amountToSpawn = 3;

    private LinkedList<GameObject> charactersWithinArea = new LinkedList<GameObject>();
    private float lastSpawntime;

	// Use this for initialization
	private void Start () {
        lastSpawntime = GameTime.CurrentTime;
        collider.isTrigger = true;
        this.gameObject.layer = 16; //puts this on the PlayerSearch layer
	}
	
	// Update is called once per frame
	private void Update () {
        if (IsRespawnTime() && CanRespawn())
        {
            SpawnNeutrals();
        }
	}

    /// <summary>
    /// Checks the current game time to see if it is time to respawn the neutral units in this camp
    /// </summary>
    private bool IsRespawnTime()
    {
        float currentTime = GameTime.CurrentTime;
        if (currentTime - lastSpawntime >= RESPAWNINTERVAL)
        {
            if (debug) { Debug.Log("It is time to spawn neutrals."); }
            lastSpawntime = currentTime;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if this camp can spawn a new set of neutral units
    /// </summary>
    private bool CanRespawn()
    {
        bool canSpawn = charactersWithinArea.Count == 0;

        if (debug)
        {
            if (canSpawn)
            {
                Debug.Log("Can spawn neutrals.");
            }
            else
            {
                foreach (GameObject g in charactersWithinArea)
                {
                    Debug.Log(g.name + " is in the spawn zone.");
                }
            }
        }

        return canSpawn;
    }

    /// <summary>
    /// Spawns new neutral units in the camp of the type and amount specified
    /// </summary>
    private void SpawnNeutrals()
    {
        for (int i = amountToSpawn; i > 0; i--)
        {
            NPCManager.Main.ServerSpawnNPC("SkeletonNPC", transform.position);
        }
    }

    //Keep track of the npcs and players in the area
    private void OnTriggerEnter(Collider collider)
    {
        charactersWithinArea.AddLast(collider.gameObject);
    }

    //Keep track of the npcs and players in the area
    private void OnTriggerExit(Collider collider)
    {
        charactersWithinArea.Remove(collider.gameObject);
    }
}
