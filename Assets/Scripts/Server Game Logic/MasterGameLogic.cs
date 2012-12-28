using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterGameLogic : MonoBehaviour
{
    public GameObject controlPoint1;
    public GameObject controlPoint2;
    public GameObject controlPoint3;
    public GameObject controlPoint4;
    public GameObject controlPoint5;
    
    // Static property to access the game's main master logic object.
    // The object has to be created before this can be used
    static MasterGameLogic _main;
    public static MasterGameLogic Main
    {
        get
        {
            if (Network.isClient) return null;

            if (_main == null)
                _main = GameObject.Find("MasterGameLogic").GetComponent<MasterGameLogic>();
            return _main;
        }
    }

    private NPCManager npcManager;
    public NPCManager NPCManager { get { return npcManager; } }

    private PlayerManager playerManager;
    private bool gameEnded = false;
    public PlayerManager PlayerManager { get { return playerManager; } }



    // Use this for initialization
    void Awake()
    {
        SetUpControlPoints();

        npcManager = new NPCManager();
        playerManager = new PlayerManager(); 
        DebugGUI.Print("initializing master game logic");

        StartCoroutine(MonitorGameState());
    }

    private IEnumerator MonitorGameState()
    {
        yield return new WaitForSeconds(10.0f);
        MessageTerminal.Main.networkView.RPC("BroadcastGameState", RPCMode.Others, GameState.goodScore, GameState.evilScore);
    }

    private void SetUpSpawnZones()
    {
        GameObject spawnZone1 = new GameObject();
        spawnZone1.AddComponent<NPCSpawnZone>();
        spawnZone1.name = "spawnZone1";
    }

    /// <summary>
    /// Sets up the worlds control points on the server only
    /// </summary>
    private void SetUpControlPoints()
    {
        controlPoint1 = Network.Instantiate(Resources.Load("ControlPoint"), Util.SampleFloorIncludingObjects(new Vector3(104, 35, 151)), Quaternion.identity, 0) as GameObject;
        controlPoint2 = Network.Instantiate(Resources.Load("ControlPoint"), Util.SampleFloorIncludingObjects(new Vector3(352, 11, 205)), Quaternion.identity, 0) as GameObject;
        controlPoint3 = Network.Instantiate(Resources.Load("ControlPoint"), Util.SampleFloorIncludingObjects(new Vector3(388, 36, 364)), Quaternion.identity, 0) as GameObject;
        controlPoint4 = Network.Instantiate(Resources.Load("ControlPoint"), Util.SampleFloorIncludingObjects(new Vector3(431, 3, 62)), Quaternion.identity, 0) as GameObject;
        controlPoint5 = Network.Instantiate(Resources.Load("ControlPoint"), Util.SampleFloorIncludingObjects(new Vector3(607, 35, 155)), Quaternion.identity, 0) as GameObject;
        controlPoint1.GetComponent<ControlPoint>().enabled = true;
        controlPoint1.GetComponent<ControlPoint>().ServerInitialize();
        controlPoint2.GetComponent<ControlPoint>().enabled = true;
        controlPoint2.GetComponent<ControlPoint>().ServerInitialize();
        controlPoint3.GetComponent<ControlPoint>().enabled = true;
        controlPoint3.GetComponent<ControlPoint>().ServerInitialize();
        controlPoint4.GetComponent<ControlPoint>().enabled = true;
        controlPoint4.GetComponent<ControlPoint>().ServerInitialize();
        controlPoint5.GetComponent<ControlPoint>().enabled = true;
        controlPoint5.GetComponent<ControlPoint>().ServerInitialize();

        controlPoint1.name = "Blue Base Control Point";
        controlPoint2.name = "Fortress Control Point";
        controlPoint3.name = "Farm Control Point";
        controlPoint4.name = "Swamp Control Point";
        controlPoint5.name = "Red Base Control Point";

        controlPoint1.networkView.RPC("ChangeName", RPCMode.OthersBuffered, controlPoint1.name);
        controlPoint2.networkView.RPC("ChangeName", RPCMode.OthersBuffered, controlPoint2.name);
        controlPoint3.networkView.RPC("ChangeName", RPCMode.OthersBuffered, controlPoint3.name);
        controlPoint4.networkView.RPC("ChangeName", RPCMode.OthersBuffered, controlPoint4.name);
        controlPoint5.networkView.RPC("ChangeName", RPCMode.OthersBuffered, controlPoint5.name);
   
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            AddNewSpawnZone();
        }

        if (!gameEnded)
        GameLogicLoop();

    }

    private void GameLogicLoop()
    {
        if (GameState.goodScore >= GameState.scoreLimit)
        {
            gameEnded = true;
            MessageTerminal.Main.networkView.RPC("GameEnded", RPCMode.All, 1);
        }
        else if (GameState.evilScore >= GameState.scoreLimit)
        {
            gameEnded = true;
            MessageTerminal.Main.networkView.RPC("GameEnded", RPCMode.All, 2);
        }
    }

    static void AddNewSpawnZone()
    {
        GameObject spawner = new GameObject();
        spawner.AddComponent<NPCSpawnZone>();
        spawner.name = "SPAWNER";
    }
}