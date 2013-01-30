using UnityEngine;
class SetTimeCommand : ConsoleCommand
{
    public override string[] Names { get { string[] names = { "settime" }; return names; } }


    public override string HelpMessage
    {
        get { return "Sets the current time of day"; }
    }

    public override void Execute(params string[] parameters)
    {
        GameTime.SetTime(float.Parse(parameters[0]));
    }
}
