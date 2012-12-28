using UnityEngine;

class EquipCommand : ConsoleCommand
{
    /// <summary>
    /// The friendly names of the console command for calling the command.
    /// </summary>
    public override string[] Names { get { string[] names = { "equip" }; return names; } }

    /// <summary>
    /// A string containing the message to display to the screen when "help ____" is entered.
    /// </summary>
    public override string HelpMessage { get { return "Equips an item"; } }

    /// <summary>
    /// Executes the console command. Takes an arbitrary number of parameters, decided by the command itself.
    /// </summary>
    /// <param name="parameters">The parameters inputted to the console window.</param>
    public override void Execute(params string[] parameters)
    {

        if (parameters.Length <= 1)
        {
            DebugGUI.Print("no item name given.");
            return;
        }
        var type = parameters[0];
        string itemName = parameters[1];
        for (int g = 2; g < parameters.Length; g++)
        {
            itemName += " " + parameters[g];
        }
        EquippableItem w = new EquippableItem();
        switch (type)
        {
            case "weapon":
                w = Item.FromName<Weapon>(itemName);
                break;
            case "shield":
                w = Item.FromName<Shield>(itemName);
                break;
        }

        w.Equip(Util.MyLocalPlayerObject);
        w.Serialize();
    }
}
