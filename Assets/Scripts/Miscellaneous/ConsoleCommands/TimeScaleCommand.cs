using UnityEngine;

public class TimeScaleCommand : NetworkConsoleCommand
{

    public override void ExecuteCommand(GameObject invokerObject, params string[] parameters)
    {
        MessageTerminal.Main.networkView.RPC("SetTimeScale", RPCMode.All, float.Parse(parameters[0]));
    }

    public override string Name
    {
        get { return "timescale"; }
    }

    public override string HelpMessage
    {
        get { return "Sets the time scale of the game to the given number."; }
    }

    public override void ApplyLocalEffects(GameObject invokerObject, params string[] parameters)
    {

    }
}