using UnityEngine;

class SpawnCommand : ConsoleCommand
{
    /// <summary>
    /// The friendly names of the console command for calling the command.
    /// </summary>
    public override string[] Names { get { string[] names = { "spawn" }; return names; } }

    /// <summary>
    /// A string containing the message to display to the screen when "help ____" is entered.
    /// </summary>
    public override string HelpMessage { get { return "Spawns an object"; } }

    /// <summary>
    /// Executes the console command. Takes an arbitrary number of parameters, decided by the command itself.
    /// </summary>
    /// <param name="parameters"></param>
    public override void Execute(params string[] parameters)
    {
	
		switch (parameters.Length)
		{
			case 1:
                Util.Spawn(parameters[0], Util.MyLocalPlayerObject.transform.position + Util.MyLocalPlayerObject.transform.forward *2);
				break;
			case 4:
				Util.Spawn(parameters[0], new Vector3(int.Parse(parameters[1]), int.Parse(parameters[2]), int.Parse(parameters[3])));
				break;
			default:
			Debug.Log("invalid parameters for spawn function");
				break;
		}
	
    }
}
