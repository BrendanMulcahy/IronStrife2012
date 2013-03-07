using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

class SerializeSpellsCommand : ConsoleCommand
{
    /// <summary>
    /// The friendly names of the console command for calling the command.
    /// </summary>
    public override string[] Names { get { string[] names = { "serializespells" }; return names; } }

    /// <summary>
    /// A string containing the message to display to the screen when "help ____" is entered.
    /// </summary>
    public override string HelpMessage { get { return "Re-Serializes all spells to new files with the given suffix appended"; } }

    /// <summary>
    /// Executes the console command. Takes an arbitrary number of parameters, decided by the command itself.
    /// </summary>
    /// <param name="parameters"></param>
    public override void Execute(params string[] parameters)
    {
        foreach (Spell spell in PlayerAbilities.AllSpells())
        {
            Serialize(spell, parameters[0]);
        }
    }

    private void Serialize(Spell spell, string suffix)
    {
        Type type = spell.GetType();
        using (XmlTextWriter writer = new XmlTextWriter(File.Create(Application.dataPath + "/Resources/Spells/" + spell.Name + suffix + ".xml"), Encoding.UTF8))
        {
            XmlSerializer serializer = new XmlSerializer(type);
            serializer.Serialize(writer, spell);
        }
    }
}
