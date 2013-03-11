using UnityEngine;
class RefreshCommand : NetworkConsoleCommand
{    
    public override string HelpMessage
    {
        get { return "Refreshrd your Mana, Health, and Stamina"; }
    }

    public override void ExecuteCommand(GameObject invokerObject, params string[] parameters)
    {
        invokerObject.GetCharacterStats().ApplyHealing(invokerObject, 999999);
        invokerObject.GetCharacterStats().Mana.CurrentValue = invokerObject.GetCharacterStats().Mana.MaxValue;
        invokerObject.GetCharacterStats().Stamina.CurrentValue = invokerObject.GetCharacterStats().Stamina.MaxValue;
    }

    public override string Name
    {
        get { return "refresh"; }
    }

    public override void ApplyLocalEffects(GameObject invokerObject, params string[] parameters)
    {
        invokerObject.GetCharacterStats().ApplyHealing(invokerObject, 999999);
        invokerObject.GetCharacterStats().Mana.CurrentValue = invokerObject.GetCharacterStats().Mana.MaxValue;
        invokerObject.GetCharacterStats().Stamina.CurrentValue = invokerObject.GetCharacterStats().Stamina.MaxValue;
    }
}
