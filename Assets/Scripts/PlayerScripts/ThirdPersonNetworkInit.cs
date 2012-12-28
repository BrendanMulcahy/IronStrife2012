using UnityEngine;
using System.Collections;


public class ThirdPersonNetworkInit : MonoBehaviour
{
    void OnNetworkInstantiate(NetworkMessageInfo msg)
    {

        // Server looking at client
        if (Network.isServer)
        {
            DebugGUI.Print("You are the server looking at " + gameObject.name + ".");
            GetComponent<ThirdPersonController>().enabled = true;
            GetComponent<PlayerMotor>().enabled = true;
            var cs = gameObject.GetCharacterStats();
            cs.StartMonitoringRegeneration();
            cs.StartSyncingHMS();
            gameObject.AddComponent<PlayerDamageReceiver>();

        }
        // Client looking at client
        else
        {
            DebugGUI.Print("You are a client looking at " + gameObject.name + ".");
            GetComponent<ThirdPersonSimpleAnimation>().enabled = false;
            GetComponent<ThirdPersonController>().enabled = false;
            GetComponent<NetworkController>().enabled = false;
            GetComponent<NetworkSyncAnimation>().enabled = true;
            gameObject.AddComponent<PlayerDamageReceiver>().isClientView = true;

        }
    }

    [RPC]
    public void SetOwnership()
    {
        if (Network.isClient)
        {
            GetComponent<NetworkController>().enabled = true;
            GetComponent<NetworkController>().StartMonitoringCameraMovement();
            GetComponent<ThirdPersonSimpleAnimation>().enabled = true;
            GetComponent<NetworkSyncAnimation>().enabled = false;

        }
        else
        {
            gameObject.AddComponent<ServerController>();
            Destroy(GetComponent<NetworkController>());
        }

        GetComponent<InventoryManager>().enabled = true;
        GetComponent<AbilityManager>().enabled = true;


        Destroy(transform.FindChild("Name Label").gameObject);
        GetComponent<ThirdPersonController>().enabled = true;
        GetComponent<ThirdPersonController>().isLocallyControlledPlayer = true;
        DebugGUI.Print(gameObject.name + " is your character. You should put the character controls on it and set up message relaying to the server.");
        Camera.main.SendMessage("SetTarget", transform);

        gameObject.AddComponent<PlayerGUI>();
        gameObject.GetComponent<LineRenderer>().enabled = true;
        gameObject.GetComponent<TrajectorySimulator>().enabled = true;
        
        // Store this game object for later reference from other scripts (just for ease of access)
        Util.MyLocalPlayerObject = this.gameObject;
    }

    [RPC]
    void PlayerDisconnected()
    {
        DebugGUI.Print(gameObject.name + " has disconnected.");
        Destroy(gameObject);
    }

    [RPC]
    public void EquipDefaultItems(string charactertype)
    {

    }
}