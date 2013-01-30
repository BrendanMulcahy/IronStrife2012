using UnityEngine;
class RefreshCommand : ConsoleCommand
{
    public override string[] Names { get { string[] names = { "refresh" }; return names; } }
    

    public override string HelpMessage
    {
        get { return "Refresh Mana Hp and Stamina"; }
    }

    public override void Execute(params string[] parameters)
    {
        GameObject CurrentPlayer = Util.MyLocalPlayerObject;
        CurrentPlayer.GetCharacterStats().ApplyHealing(CurrentPlayer,999999);
        CurrentPlayer.GetCharacterStats().Mana.CurrentValue = CurrentPlayer.GetCharacterStats().Mana.MaxValue;
        CurrentPlayer.GetCharacterStats().Stamina.CurrentValue = CurrentPlayer.GetCharacterStats().Stamina.MaxValue;
    }
}
