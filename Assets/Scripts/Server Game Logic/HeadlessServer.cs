using System.Collections;
using UnityEngine;

public class HeadlessServer : MonoBehaviour
{
    internal void StartHeadlessServer(int port, string name, string description)
    {
        Debug.Log("Starting headless server with name = " + name + " and description = " + description);
        Network.InitializeServer(20, port, false);
        MasterServer.RegisterHost("IronStrife", name, description);

        StartCoroutine(SendHeartbeat());
    }

    private IEnumerator SendHeartbeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            // TODO: Send the number of connected players, etc.
        }
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        if (Network.connections.Length == 0)
        {
            TurnOffServer();
        }
    }

    private void TurnOffServer()
    {
        Application.Quit();
    }
}