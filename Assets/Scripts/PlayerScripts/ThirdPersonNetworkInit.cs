using UnityEngine;
using System.Collections;

public class ThirdPersonNetworkInit : MonoBehaviour
{
    void OnNetworkInstantiate(NetworkMessageInfo msg)
    {

        //GetComponent<Inventory>().enabled = false;
        //GetComponent<AbilityManager>().enabled = false;
        //GetComponent<NetworkController>().enabled = false;
        //GetComponent<ServerController>().enabled = false;

        //// Server looking at client
        //if (Network.isServer)
        //{
        //    Debug.Log("You are the server looking at " + gameObject.name + ".");
        //    GetComponent<ThirdPersonController>().enabled = true;
        //    GetComponent<PlayerMotor>().enabled = true;
        //    gameObject.AddComponent<PlayerDamageReceiver>();

        //}
        //// Client looking at client
        //else
        //{
        //    Debug.Log("You are a client looking at " + gameObject.name + ".");
        //    GetComponent<ThirdPersonSimpleAnimation>().enabled = false;
        //    GetComponent<ThirdPersonController>().enabled = false;
        //    GetComponent<NetworkSyncAnimation>().enabled = true;

        //    gameObject.AddComponent<PlayerDamageReceiver>().isClientView = true;

        //}
    }

    [RPC]
    public void SetOwnership()
    {        // Store this game object for later reference from other scripts (just for ease of access)
        Debug.Log(gameObject.name + " is your character.");

        Util.MyLocalPlayerObject = this.gameObject;

        if (Network.isClient)
        {
            GetComponent<NetworkController>().enabled = true;
            GetComponent<NetworkController>().StartMonitoringCameraMovement();
            GetComponent<ServerController>().enabled = false;
            GetComponent<ThirdPersonSimpleAnimation>().enabled = true;
            GetComponent<NetworkSyncAnimation>().enabled = false;

        }
        else
        {
            gameObject.GetComponent<ServerController>().enabled = true;
            Destroy(GetComponent<NetworkController>());
        }

        GetComponent<Inventory>().enabled = true;
        GetComponent<AbilityManager>().enabled = true;

        Destroy(transform.FindChild("Name Label").gameObject);
        GetComponent<ThirdPersonController>().enabled = true;
        GetComponent<ThirdPersonController>().isLocallyControlledPlayer = true;


        gameObject.AddComponent<PlayerGUI>();
        gameObject.GetComponent<LineRenderer>().enabled = true;
        gameObject.GetComponent<TrajectorySimulator>().enabled = true;

        gameObject.AddComponent<PlayerObjectInteractor>();
        gameObject.SendMessage("OnSetOwnership", SendMessageOptions.DontRequireReceiver);

        
    }

    [RPC]
    void PlayerDisconnected()
    {
        Debug.Log(gameObject.name + " has disconnected.");
        Destroy(gameObject);
    }
}