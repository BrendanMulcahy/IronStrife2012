using UnityEngine;

class TeamListCommand : ConsoleCommand
{
    /// <summary>
    /// The friendly names of the console command for calling the command.
    /// </summary>
    public override string[] Names { get { string[] names = { "listteam" }; return names; } }

    /// <summary>
    /// A string containing the message to display to the screen when "help ____" is entered.
    /// </summary>
    public override string HelpMessage { get { return "Shows a list of all players on each team."; } }

    /// <summary>
    /// Executes the console command. Takes an arbitrary number of parameters, decided by the command itself.
    /// </summary>
    /// <param name="parameters"></param>
    public override void Execute(params string[] parameters)
    {
        Debug.Log("GOOD PLAYERS:");
        foreach (GameObject go in MasterGameLogic.Main.PlayerManager.goodPlayers)
        {
            Debug.Log(go.name);
        }

        Debug.Log("EVIL PLAYERS:");
        foreach (GameObject go in MasterGameLogic.Main.PlayerManager.evilPlayers)
        {
            Debug.Log(go.name);
        }
    }
}
