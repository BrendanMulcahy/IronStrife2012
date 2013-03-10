using UnityEngine;
using System.Linq;

/// <summary>
/// Represents a console command that is relayed to the server and then executed.
/// </summary>
public abstract class NetworkConsoleCommand
{
    /// <summary>
    /// Relays this command to the server
    /// </summary>
    public void TryExecute(params string[] parameters)
    {
        object[] allParameters = new object[parameters.Length + 1];
        allParameters[0] = Util.MyLocalPlayerObject.networkView.viewID;
        for (int g=0; g < parameters.Length; g++)
        {
            allParameters[g + 1] = parameters[g];
        }
        DebugGUI.Main.networkView.RPCToServer(DebugGUI.Main, "ExecuteNetworkConsoleCommand", this.Name, allParameters);
    }

    public void CommitExecute(params object[] parameters)
    {
        var strings = parameters.Skip(1).Cast<string>().ToArray();
        var go = ((NetworkViewID)parameters[0]).GetGameObject();
        ExecuteCommand(go, strings);
    }
    public abstract void ExecuteCommand(GameObject invokerObject, params string[] parameters);
    public abstract string Name { get; }
   
}