using UnityEngine;

// Events
public partial class StrifeGameObject
{
    public event UnitDiedEventHandler Died;
    public void OnDeath(UnitDiedEventArgs unitDiedEventArgs)
    {
        if (Died != null)
        {
            Died(gameObject, unitDiedEventArgs);
        }
    }

    public event DamageEventHandler Damaged;
    public void OnDamage(DamageEventArgs e)
    {
        if (Damaged != null)
        {
            Damaged(e);
        }
    }

    public event HealedEventHandler Healed;
    public void OnDamage(HealedEventArgs e)
    {
        if (Healed != null)
        {
            Healed(e);
        }
    }

}