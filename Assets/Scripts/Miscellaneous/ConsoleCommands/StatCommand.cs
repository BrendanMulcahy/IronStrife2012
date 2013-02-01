using UnityEngine;

class StatCommand : ConsoleCommand
{
    /// <summary>
    /// The friendly names of the console command for calling the command.
    /// </summary>
    public override string[] Names { get { string[] names = { "stats" }; return names; } }

    /// <summary>
    /// A string containing the message to display to the screen when "help ____" is entered.
    /// </summary>
    public override string HelpMessage { get { return "Displays the stats of a player"; } }

    /// <summary>
    /// Executes the console command. Takes an arbitrary number of parameters, decided by the command itself.
    /// </summary>
    /// <param name="parameters"></param>
    public override void Execute(params string[] parameters)
    {
        GameObject player;
        if (parameters.Length == 1)
        {
            player = GameObject.Find(parameters[0]);
        }
        else
        {
            player = Util.MyLocalPlayerObject;
        }

        var stats = player.GetCharacterStats();

        Debug.Log("Stats for " + player.name + ":");
        Debug.Log(stats.ToString());
    }
}
