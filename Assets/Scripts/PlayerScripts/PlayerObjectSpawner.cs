using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used to synchronize and instantiate player-generated objects across the network.
/// This includes things like projectiles fired, spell effects cast, or perhaps other cosmetic things.
/// </summary>
public class PlayerObjectSpawner : MonoBehaviour 
{
    PlayerSoundGenerator playerSound;

    void Start()
    {
        playerSound = GetComponent<PlayerSoundGenerator>();
    }

    [RPC]
    private void SpawnArrowPrefab(Vector3 origin, Vector3 directionFired, float velocity)
    {
        var newGO = Instantiate(Resources.Load("ArrowPrefab"), origin, Quaternion.LookRotation(directionFired)) as GameObject;
        newGO.GetComponent<Arrow>().InitializeProjectile(directionFired, velocity, gameObject.transform);
        playerSound.PlayArrowFire();

    }

    [RPC]
    private void SpawnSpellEffectAtPosition(string spellName, Vector3 location)
    {        
        GameObject effectToInstantiate = Resources.Load("SpellEffects/" + spellName, typeof(GameObject)) as GameObject;
        Instantiate(effectToInstantiate, location, Quaternion.identity);
        
    }

    [RPC]
    private void SpawnSpellEffectTowardsDirection(string spellName, Vector3 direction)
    {

        GameObject effectToInstantiate = Resources.Load("SpellEffects/" + spellName, typeof(GameObject)) as GameObject;
        GameObject instance = Instantiate(effectToInstantiate, transform.position, Quaternion.identity) as GameObject;
        instance.transform.rotation = Quaternion.LookRotation(direction);
    }

    [RPC]
    private void SimulateIPointSpellExecute(int spellId, Vector3 targetPoint)
    {
        if (gameObject.IsMyLocalPlayer()) return;
        IPointSpell toCast = (PlayerAbilities.AllSpells()[spellId]) as IPointSpell;
        toCast.Execute(this.gameObject, targetPoint);
    }

    [RPC]
    private void SimulateITargetSpellExecute(int spellId, Vector3 direction)
    {
        if (gameObject.IsMyLocalPlayer()) return;

        ITargetSpell toCast = (PlayerAbilities.AllSpells()[spellId]) as ITargetSpell;
        toCast.Execute(this.gameObject, direction, null);
    }

    [RPC]
    private void SimulateISelfSpellExecute(int spellId)
    {
        if (gameObject.IsMyLocalPlayer()) return;

        ISelfSpell toCast = (PlayerAbilities.AllSpells()[spellId]) as ISelfSpell;
        toCast.Execute(this.gameObject);
    }
}
