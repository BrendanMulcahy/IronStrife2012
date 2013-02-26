using UnityEngine;

/// <summary>
/// Represents an abstract event that occurs on the server when a GameObject dies.
/// </summary>
public abstract class ServerDeathEvent : MonoBehaviour
{
    void Start()
    {
        if (Network.isClient)
        {
            Destroy(this);
            return;
        }

        this.gameObject.GetCharacterStats().Died += DeathEvent_Died;
    }

    void DeathEvent_Died(GameObject deadUnit, UnitDiedEventArgs e)
    {
        DeathEvent();
    }

    protected abstract void DeathEvent();
}