using UnityEngine;

/// <summary>
/// The base script of all Iron Strife behaviour scripts
/// </summary>
public class StrifeScriptBase : MonoBehaviour
{
    private int _teamNumber = -1;
    public virtual int TeamNumber
    {
        get
        {
            if (_teamNumber == -1)
                _teamNumber = this.gameObject.GetTeamNumber();
            return _teamNumber;
        }
    }

    private CharacterStats _stats;
    public CharacterStats Stats
    {
        get
        {
            if (!_stats)
                _stats = GetComponent<CharacterStats>();
            return _stats;
        }
    }

    private Inventory _inventory;
    public Inventory Inventory
    {
        get
        {
            if (!_inventory)
                _inventory = GetComponent<Inventory>();
            return _inventory;
        }
    }

    private AbilityManager _abilityManager;
    public AbilityManager AbilityManager
    {
        get
        {
            if (!_abilityManager)
                _abilityManager = GetComponent<AbilityManager>();
            return _abilityManager;
        }
    }

    private NetworkPlayer _owner = new NetworkPlayer();
    public NetworkPlayer Owner
    {
        get
        {
            if (Network.isClient) return new NetworkPlayer();
            if (_owner == new NetworkPlayer())         
                _owner = gameObject.GetNetworkPlayer();          
            return _owner;
        }
    }

    private DamageReceiver _damageReceiver;
    public DamageReceiver DamageReceiver
    {
        get
        {
            if (!_damageReceiver)
                _damageReceiver = GetComponent<DamageReceiver>();
            return _damageReceiver;
        }
    }

    private ThirdPersonController _thirdPersonController;
    public ThirdPersonController Controller
    {
        get
        {
            if (!_thirdPersonController)
                _thirdPersonController = GetComponent<ThirdPersonController>();
            return _thirdPersonController;
        }
    }
}