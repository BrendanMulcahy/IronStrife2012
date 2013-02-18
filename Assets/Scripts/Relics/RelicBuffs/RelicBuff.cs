using UnityEngine;

/// <summary>
/// Base class for buffs given to players for holding a relic or having a teammate hold a relic
/// </summary>
public abstract class RelicBuff : MonoBehaviour
{
    /// <summary>
    /// The relic that produced this RelicBuff
    /// </summary>
   protected Relic relic;
   public Relic Relic { get { return relic; } set { relic = value; } }

    void Start()
    {
        AddBuffEffects();
    }
    
    /// <summary>
    /// Called when this component is removed.
    /// </summary>
    void OnDestroy()
    {
        RemoveBuffEffects();
    }

    protected abstract void AddBuffEffects();
    protected abstract void RemoveBuffEffects();
}