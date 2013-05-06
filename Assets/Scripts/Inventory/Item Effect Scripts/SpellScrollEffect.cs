using UnityEngine;

public class SpellScrollEffect : ItemEffect
{
    public override void ActivateEffect()
    {
        var spellName = parameters[0].Replace('_', ' ');
        var spellToLearn = PlayerAbilities.GetSpell(spellName);

        networkView.RPCToServer("TryLearnSpell", (int)spellToLearn);
        Destroy(this);
    }
}