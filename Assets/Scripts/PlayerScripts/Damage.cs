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

    /// <summary>
    /// The location where this damage was taken
    /// </summary>
    public Vector3 location;

    public Damage(int amount, GameObject source, Vector3 location, DamageType damageType = DamageType.Physical)
    {
        this.amount = amount;
        this.source = source;
        this.damageType = damageType;
        this.location = location;
    }
}
