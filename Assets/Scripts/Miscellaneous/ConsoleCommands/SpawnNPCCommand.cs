using UnityEngine;

class SpawnNPCCommand : ConsoleCommand
{
    /// <summary>
    /// The friendly names of the console command for calling the command.
    /// </summary>
    public override string[] Names { get { string[] names = { "npc" }; return names; } }

    /// <summary>
    /// A string containing the message to display to the screen when "help ____" is entered.
    /// </summary>
    public override string HelpMessage { get { return "Spawns a new NPC on you."; } }

    /// <summary>
    /// Executes the console command. Takes an arbitrary number of parameters, decided by the command itself.
    /// </summary>
    /// <param name="parameters"></param>
    public override void Execute(params string[] parameters)
    {
        if (parameters.Length == 2)
        {
            if (parameters[0] == "guard")
            {
                Vector3 position = Util.MyLocalPlayerObject.transform.position + Util.MyLocalPlayerObject.transform.forward * 2;
                MasterGameLogic.Main.NPCManager.ServerSpawnNPC("GuardNPC", position).networkView.RPC("ChangeTeam", RPCMode.All, int.Parse(parameters[1]));
            }
        }
        else
        {
            Vector3 position = Util.MyLocalPlayerObject.transform.position + Util.MyLocalPlayerObject.transform.forward * 2;
            MasterGameLogic.Main.NPCManager.ServerSpawnNPC("NPC", position);
        }
    }
}
