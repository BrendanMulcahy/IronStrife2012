using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCSpawnCamp : MonoBehaviour {

    public GameObject neutralType;
    //public Collider spawnZone;
    public const float RESPAWNINTERVAL = 4.0f;

    private LinkedList<GameObject> charactersWithinArea;
    private float lastSpawntime;

	// Use this for initialization
	private void Start () {
        lastSpawntime = GameTime.CurrentTime;
        collider.isTrigger = true;
        this.gameObject.layer = 1 << 16; //puts this on the PlayerSearch layer
	}
	
	// Update is called once per frame
	private void Update () {
        if (IsRespawnTime() && CanRespawn())
        {

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
            Debug.Log("It is time to spawn neutrals.");
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
        if (canSpawn)
        {
            Debug.Log("It is time to spawn neutrals.");
        }
        else
        {
            foreach (GameObject g in charactersWithinArea)
            {
                Debug.Log(g.name + " is in the spawn zone.");
            }
        }
        return canSpawn;
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
