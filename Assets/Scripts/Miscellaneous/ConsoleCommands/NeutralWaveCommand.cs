using UnityEngine;

class NeutralWaveCommand : ConsoleCommand
{

    public override string[] Names
    {
        get { string[] names = { "neutralwave" }; return names; }
    }

    public override string HelpMessage
    {
        get { return "Spawns a neutral wave at each of the control points on the map."; }
    }

    public override void Execute(params string[] parameters)
    {
        NPCManager.Main.TrySpawnNeutralWaves();
    }
}