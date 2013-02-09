using UnityEngine;
using System.Linq;

public class CommandLineReader : MonoBehaviour
{
    void Awake()
    {
        var args = System.Environment.GetCommandLineArgs();

        if (args.ToList().Contains("StartHeadlessServer"))
        {
            StartHeadlessServer(args[3], args[4]);
        }
    }

    void StartHeadlessServer(string name, string description)
    {
        Debug.Log("Starting headless server with name = " + name + " and description = " + description);
        Network.InitializeServer(32, 25000, false);
        MasterServer.RegisterHost("IronStrife", name, description);
    }
}