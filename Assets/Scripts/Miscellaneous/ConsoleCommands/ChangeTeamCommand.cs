using UnityEngine;

class ChangeTeamCommand : ConsoleCommand
{
    /// <summary>
    /// The friendly names of the console command for calling the command.
    /// </summary>
    public override string[] Names { get { string[] names = { "team" }; return names; } }

    /// <summary>
    /// A string containing the message to display to the screen when "help ____" is entered.
    /// </summary>
    public override string HelpMessage { get { return "USAGE:\nteam <#>\tChanges your team to the team number selected. Use 1 or 2."; } }

    /// <summary>
    /// Executes the console command. Takes an arbitrary number of parameters, decided by the command itself.
    /// </summary>
    /// <param name="parameters"></param>
    public override void Execute(params string[] parameters)
    {
    
        if (parameters.Length == 1)
        {
            CharacterStats charStats = Util.MyLocalPlayerObject.GetCharacterStats();
            charStats.TeamNumber = int.Parse(parameters[0]);
            return;
        }
        else if (parameters.Length != 2)
        {
            Debug.Log("Need player name and team value as parameters");
            return;
        }
        GameObject player = GameObject.Find(parameters[0]);

        if (player != null)
        {
            CharacterStats charStats = player.GetComponent <CharacterStats>();
            charStats.TeamNumber = int.Parse(parameters[1]);
            Debug.Log("Successful team change for player \"" + parameters[0] + "\"");
        }
        else
        {
            Debug.Log("Player with the given name cannot be found.");
        }
    
    }
}
