using UnityEngine;

public class SpellScrollEffect : ItemEffect
{
    public override void ActivateEffect()
    {
        var spellToLearn = PlayerAbilities.GetSpell(parameters[0]);

        networkView.RPCToServer("TryLearnSpell", (int)spellToLearn);
        Destroy(this);
    }
}