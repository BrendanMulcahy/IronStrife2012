using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCSpawnZone : MonoBehaviour
{
    public Object EnemyToSpawn { get; set; }

    private List<GameObject> npcList;
    private short numberLivingEnemies = 0;
    private short respawnTime = 4; // seconds for respawn
    private float respawnCounter = 3;  //seconds since last respawn
    private short maxLivingEnemies = 4; // Number of maximum enemies that can be alive at this spawn point
    private System.DateTime startTimer = System.DateTime.Now;

    private Vector3 spawnArea;

    // Use this for initialization
    void Start()
    {
        EnemyToSpawn = Resources.Load("NPC");	// This loads the "NPC" object from the Resources folder in the project.
        // If you want to spawn more or different things, put them in the Resources folder
        npcList = new List<GameObject>();
        //GameObject.Find(PlayerPrefs.GetString("username")).transform.position is the players position
        //Network.Instantiate (EnemyToSpawn,	new Vector3(0, 0, 0), Quaternion.identity, 0);

        int area = UnityEngine.Random.Range(1, 6);

        switch (area)
        {
            case 1:
                spawnArea = new Vector3(607, 35, 155);
                break;
            case 2:
                spawnArea = new Vector3(352, 11, 205);
                break;
            case 3:
                spawnArea = new Vector3(388, 36, 364);
                break;
            case 4:
                spawnArea = new Vector3(431, 3, 62);
                break;
            case 5:
                spawnArea = new Vector3(104, 35, 151);
                break;

        }
        DebugGUI.Print("Spawn area = " + area);
        spawnArea = new Vector3(104, 35, 151);
   
    }

    // Update is called once per frame
    void Update()
    {
        checkForDeaths();	//checks to see if any npc's died, if they did, update numberLivingEnemies and npcList

        spawnUnit();		//spawns unit based on conditionals in method
    }

    void spawnUnit()
    {
        respawnCounter = Mathf.Floor((float)(System.DateTime.Now).Subtract(startTimer).TotalSeconds);	//subtracts the time difference between now and the start timer
        if (respawnCounter >= respawnTime)	//checks time difference
            if (numberLivingEnemies < maxLivingEnemies)	//if the spawn zone isn't full
            {
                for (int x = 0; x < maxLivingEnemies - numberLivingEnemies; x++)
                {
                    //Not working for some reason.
                    MasterGameLogic.Main.NPCManager.ServerSpawnNPC("NPC", spawnArea + new Vector3(UnityEngine.Random.Range(-10, 10), 1, UnityEngine.Random.Range(-10, 10)), this);
                    
                    //npcList.Add((GameObject)Network.Instantiate(EnemyToSpawn, spawnArea + new Vector3(UnityEngine.Random.Range(-10, 10), 1 ,UnityEngine.Random.Range(-10, 10)), Quaternion.identity, 0));
                }

                numberLivingEnemies = maxLivingEnemies;
                startTimer = System.DateTime.Now;	//make start timer current time
            }
            else    //if there are no npc's to repopulate, it misses the respawn window
            {
                startTimer = System.DateTime.Now;	//make start timer current time
            }
    }

    void checkForDeaths()
    {
        List<GameObject> tempList = new List<GameObject>();
        numberLivingEnemies = 0;

        foreach (NPCRecord npc in MasterGameLogic.Main.NPCManager.NPCs)
        {
            numberLivingEnemies++;
        }


    }
}