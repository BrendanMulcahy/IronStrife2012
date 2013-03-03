using UnityEngine;
using System.Linq;

public class CommandLineReader : MonoBehaviour
{
    private bool isHeadlessServer = false;
    void Awake()
    {
        var args = System.Environment.GetCommandLineArgs();

        if (args.ToList().Contains("StartHeadlessServer"))
        {
            StartHeadlessServer(int.Parse(args[3]), args[4], args[5]);
        }
    }

    void StartHeadlessServer(int port, string name, string description)
    {
        this.gameObject.AddComponent<HeadlessServer>().StartHeadlessServer(port, name, description);
    }
}