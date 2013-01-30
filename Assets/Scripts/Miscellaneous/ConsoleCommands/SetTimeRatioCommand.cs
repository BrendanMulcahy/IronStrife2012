using UnityEngine;
class SetTimeRatioCommand : ConsoleCommand
{
    public override string[] Names { get { string[] names = { "settimeratio" }; return names; } }


    public override string HelpMessage
    {
        get { return "Changes the rate at which time changes"; }
    }

    public override void Execute(params string[] parameters)
    {
        GameTime.SetDayCycleInMinutes(float.Parse(parameters[0], System.Globalization.NumberStyles.Any));
    }
}
