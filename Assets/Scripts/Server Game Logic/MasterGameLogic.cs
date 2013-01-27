using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterGameLogic : MonoBehaviour
{

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
        npcManager = new NPCManager();
        playerManager = new PlayerManager(); 

    }

    private IEnumerator MonitorGameState()
    {
        yield return new WaitForSeconds(10.0f);
        MessageTerminal.Main.networkView.RPC("BroadcastGameState", RPCMode.Others, GameState.goodScore, GameState.evilScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnded)
            GameLogicLoop();

    }

    private void GameLogicLoop()
    {
        //
    }

}