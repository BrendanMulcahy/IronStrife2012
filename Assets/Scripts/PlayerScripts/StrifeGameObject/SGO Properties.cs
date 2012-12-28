using UnityEngine;
using System.Collections;

/// <summary>
/// Specialized GameObject class for Iron Strife character objects. Contains references to all useful components of an Iron Strife object.
/// </summary>
public partial class StrifeGameObject
{
    public GameObject gameObject;

    private CharacterStats _characterStats;
    /// <summary>
    /// Returns the CharacterStats component of the Game Object. Returns null if none attached.
    /// </summary>
    public CharacterStats characterStats
    {
        get
        {
            if (!_characterStats)
            {
                _characterStats = gameObject.GetComponent<CharacterStats>();
            }
            return _characterStats;
        }
    }

    private InventoryManager _inventoryManager;
    /// <summary>
    /// Returns the Inventorymanager component of the Game Object. Returns null if none attached.
    /// </summary>
    public InventoryManager inventoryManager
    {
        get
        {
            if (!_inventoryManager)
            {
                _inventoryManager = gameObject.GetComponent<InventoryManager>();
            }
            return _inventoryManager;
        }
    }


    private AbilityManager _abilityManager;
    /// <summary>
    /// Returns the Inventorymanager component of the Game Object. Returns null if none attached.
    /// </summary>
    public AbilityManager abilityManager
    {
        get
        {
            if (!_abilityManager)
            {
                _abilityManager = gameObject.GetComponent<AbilityManager>();
            }
            return _abilityManager;
        }
    }



    public static implicit operator StrifeGameObject(GameObject go)
    {
        return new StrifeGameObject() { gameObject = go };
    }

}