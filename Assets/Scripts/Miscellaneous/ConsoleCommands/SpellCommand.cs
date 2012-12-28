using UnityEngine;

class SpellCommand : ConsoleCommand
{
    /// <summary>
    /// The friendly names of the console command for calling the command.
    /// </summary>
    public override string[] Names { get { string[] names = { "spells" }; return names; } }

    /// <summary>
    /// A string containing the message to display to the screen when "help ____" is entered.
    /// </summary>
    public override string HelpMessage { get { return "Enumerates all spells implemented in the game"; } }

    /// <summary>
    /// Executes the console command. Takes an arbitrary number of parameters, decided by the command itself.
    /// </summary>
    /// <param name="parameters"></param>
    public override void Execute(params string[] parameters)
    {
        Debug.Log(PlayerAbilities.StringList());
    }
}
