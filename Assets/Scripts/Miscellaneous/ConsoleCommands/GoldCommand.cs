using UnityEngine;

class GoldCommand : ConsoleCommand
{
    /// <summary>
    /// The friendly names of the console command for calling the command.
    /// </summary>
    public override string[] Names { get { string[] names = { "gold" }; return names; } }

    /// <summary>
    /// A string containing the message to display to the screen when "help ____" is entered.
    /// </summary>
    public override string HelpMessage { get { return "Usage:\n gold <amount>\tGives you <amount> gold.\ngold\t Shows you how much gold you have."; } }

    /// <summary>
    /// Executes the console command. Takes an arbitrary number of parameters, decided by the command itself.
    /// </summary>
    /// <param name="parameters"></param>
    public override void Execute(params string[] parameters)
    {
        switch (parameters.Length)
        {
            case 0:
                Debug.Log("Current gold: " + Util.MyLocalPlayerObject.GetInventory().Gold);
                break;
            case 1:
                int amount = 0;
                if (int.TryParse(parameters[0], out amount))
                    Util.MyLocalPlayerObject.GetInventory().Gold += amount;
                Debug.Log("Current gold: " + Util.MyLocalPlayerObject.GetInventory().Gold);

                break;

        }
    }
}
