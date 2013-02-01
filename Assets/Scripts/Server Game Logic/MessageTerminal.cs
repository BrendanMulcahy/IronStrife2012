using UnityEngine;
using System.Collections;

public class MessageTerminal : MonoBehaviour
{
    private static MessageTerminal _instance;
    public static MessageTerminal Main
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("MessageTerminal").GetComponent<MessageTerminal>();
            }
            return _instance;
        }
    }

    [RPC]
    void SpawnNPC(string type, Vector3 position, NetworkViewID animationID, NetworkViewID transformID)
    {
        Debug.Log("Spawning new NPC of type " + type + " at  ["+position.x + ", "+position.y + " +, "+position.z + "]");
        GameObject newNPC = GameObject.Instantiate(Resources.Load("NPCs/" + type)) as GameObject;
        newNPC.GetComponents<NetworkView>()[0].viewID = transformID;
        newNPC.GetComponents<NetworkView>()[1].viewID = animationID;
    }

    [RPC]
    void BroadcastGameState(int newGoodScore, int newEvilScore)
    {
        GameState.goodScore = newGoodScore;
        GameState.evilScore = newEvilScore;
    }

    [RPC]
    void GameStarted()
    {
        Debug.Log("The game has started!");
        PopupMessage.Display("The game has started!", 3.0f);
    }

}
