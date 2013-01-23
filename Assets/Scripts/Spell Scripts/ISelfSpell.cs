using UnityEngine;

public interface ISelfSpell
{
    void Execute(GameObject caster);
}

public interface ISelfSpellWithViewID
{
    void Execute(GameObject caster, NetworkViewID viewID);

}
