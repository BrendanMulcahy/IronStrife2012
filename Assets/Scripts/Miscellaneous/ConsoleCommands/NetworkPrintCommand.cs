using UnityEngine;

public class NetworkPrintCommand : NetworkConsoleCommand
{

    public override void ExecuteCommand(GameObject invokerObject, params string[] parameters)
    {
        var totalString = "";
        foreach (string s in parameters)
        {
            totalString += s + " ";
        }
        Debug.Log(invokerObject.name + " says:\n" + totalString);
    }

    public override string Name
    {
        get { return "networkprint"; }
    }
}