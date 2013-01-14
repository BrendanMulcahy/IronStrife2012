using UnityEngine;

public class Damage
{
    /// <summary>
    /// The type of damage.
    /// </summary>
    public DamageType damageType;

    /// <summary>
    /// The amount of damage done by this damage object. Before any reductions but after all modifications by the attacker
    /// </summary>
    public int amount;

    /// <summary>
    /// The source GameObject responsible for this damage
    /// </summary>
    public GameObject source;
}
