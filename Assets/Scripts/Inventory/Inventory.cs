using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EternityGUI;
using System;

[PlayerComponent(PlayerScriptType.AllDisabled, PlayerScriptType.ClientOwnerEnabled, PlayerScriptType.ServerOwnerEnabled)]
/// <summary>
/// Handles player inventory, equipping items, unequipping items.
/// Contains information about currently equipped weapons, armor, shield, etc.
/// Also contains GUI functions for displaying that information
/// </summary>
public class Inventory : MonoBehaviour
{

    /// <summary>
    /// The list of items that a player currently owns.
    /// </summary>
    public List<Item> Items { get; set; }

    // Is the inventory GUI visible?
    bool visible = false;

    // Currently equipped gear
    public Weapon currentWeapon;
    public Shield currentShield;
    public GameObject currentWeaponGameobject;
    public GameObject currentShieldGameObject;
    public WeaponType CurrentWeaponType { get { if (currentWeapon != null) return currentWeapon.weaponType; else return WeaponType.None; } }

    public event WeaponChangedEventHandler WeaponChanged;

    private InventoryPanel inventoryPanel;

    private int _gold;
    public int Gold { get { return _gold; } set { _gold = value; } }

    public delegate void ItemAddedEventHandler(Inventory sender, Item newItem);

    public event ItemAddedEventHandler ItemAdded;

    void Awake()
    {
        Items = new List<Item>();
        if (Network.isServer)
            AddDefaultInventoryItems();
        
    }

    void OnSetOwnership()
    {
        inventoryPanel = InventoryPanel.Create(this, new Vector3(100, 100).ScreenToViewport());
    }

    public void AddDefaultInventoryItems()
    {
        networkView.RPC("AddItemToInventory", RPCMode.All, "Simple Sword");
        TryEquipItem((Weapon)Items[0]);

        networkView.RPC("AddItemToInventory", RPCMode.All, "Health Potion");
        networkView.RPC("AddItemToInventory", RPCMode.All, "Health Potion");
        networkView.RPC("AddItemToInventory", RPCMode.All, "Health Potion");
        networkView.RPC("AddItemToInventory", RPCMode.All, "Mana Potion");
        networkView.RPC("AddItemToInventory", RPCMode.All, "Mana Potion");
        networkView.RPC("AddItemToInventory", RPCMode.All, "Mana Potion");

        networkView.RPC("AddItemToInventory", RPCMode.All, "Shielded Bow");
        networkView.RPC("AddItemToInventory", RPCMode.All, "Shield of the Round");
        TryEquipItem((Shield)Items[8]);


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            visible = !visible;
            if (visible)
                gameObject.DisableControls();
            else
                gameObject.EnableControls();
        }
        if (inventoryPanel)
            inventoryPanel.gameObject.SetActive(visible);

    }

    public void TryEquipItem(EquippableItem item)
    {
        if (Network.isServer)
        {
            ServerTryEquipItem(item.name);
        }
        else
        {
            networkView.RPC("ServerTryEquipItem", RPCMode.Server, item.name);
        }
    }

    [RPC]
    void ServerTryEquipItem(string itemName)
    {
        EquippableItem item;
        if ((item = (EquippableItem)this.Get(itemName)) !=null && item is EquippableItem)
        {
            networkView.RPC("CommitEquipItem", RPCMode.All, itemName, (int)item.itemType);
        }
        else
        {
            Debug.Log(gameObject.name + " tried to equip " + itemName + " but he doesn't have one.");
        }
    }

    /// <summary>
    /// Returns the item in this inventory given the name
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    private Item Get(string itemName)
    {
        foreach (Item item in Items)
        {
            if (item.name == itemName)
                return item;
        }
        return null;
    }

    [RPC] 
    void CommitEquipItem(string itemName, int itemType)
    {
        try
        {
            EquippableItem item = Get(itemName) as EquippableItem;

            Weapon oldWeapon = null;

            var GO = item.Equip(gameObject);
            if (item.itemType == ItemType.Weapon)
            {
                if (currentWeapon != null)
                {
                    oldWeapon = currentWeapon;
                    CommitUnequipItem((int)ItemType.Weapon);

                }
                if (((Weapon)item).numHands == 2 && currentShield != null)
                {
                    CommitUnequipItem((int)ItemType.Shield);
                }
                currentWeapon = (Weapon)item;
                currentWeaponGameobject = GO;
                var traj = GetComponent<TrajectorySimulator>();
                if (traj)
                    traj.SetWeaponGameObject(GO);

                if (WeaponChanged != null)
                    WeaponChanged(this.gameObject, new WeaponChangedEventArgs() { oldWeapon = oldWeapon, newWeapon = currentWeapon });

            }
            if (item.itemType == ItemType.Shield)
            {
                if (currentWeapon != null && currentWeapon.numHands == 2)
                {
                    CommitUnequipItem((int)ItemType.Weapon);
                }
                if (currentShield != null)
                {
                    Destroy(currentShieldGameObject);
                }
                currentShield = (Shield)item;
                currentShieldGameObject = GO;
            }
        }
        catch (InvalidCastException e)
        {
            Debug.LogError(e.Message + "\n"+gameObject.name + " tried to equip something that was not valid.");

        }
    }

    [RPC] 
    void CommitUnequipItem(int itemType)
    {
        ItemType type = (ItemType)itemType;
        if (type == ItemType.Shield)
        {
            Destroy(currentShieldGameObject);
            currentShieldGameObject = null;
            currentShield = null;
        }
        else if (type == ItemType.Weapon)
        {
            Destroy(currentWeaponGameobject);
            currentWeaponGameobject = null;
            currentWeapon = null;
        }
    }

    /// <summary>
    /// Called by a client, run on the server when a client attempts to purchase an item at a shop.
    /// </summary>
    /// <param name="itemname"></param>
    [RPC]
    public void TryPurchaseItem(string itemName, NetworkViewID shopID, NetworkMessageInfo msg)
    {
        var itemToPurchase = ItemDirectory.Get(itemName);
        if (itemToPurchase.goldCost <= this._gold)
        {
            networkView.RPC("AddItemToInventory", RPCMode.All, itemName);
            NetworkView.Find(shopID).RPC("ItemPurchasedSound", RPCMode.All);
        }
        else
        {
            networkView.RPC("InsufficientGoldMessage", msg.sender);
        }
    }

    public void TryPurchaseItem(string itemName, NetworkViewID shopID)
    {
        var itemToPurchase = ItemDirectory.Get(itemName);
        if (itemToPurchase.goldCost <= this._gold)
        {
            networkView.RPC("AddItemToInventory", RPCMode.All, itemName);
            NetworkView.Find(shopID).RPC("ItemPurchasedSound", RPCMode.All);
        }
        else
        {
            InsufficientGoldMessage();
        }
    }

    public void TryConsumeItem(string itemName)
    {
        if (Network.isServer)
            ServerTryConsumeItem(itemName);
        else
            networkView.RPC("ServerTryConsumeItem", RPCMode.Server, itemName);
    }

    [RPC]
    void ServerTryConsumeItem(string itemName)
    {
        Item item = this.Get(itemName);
        if (item!=null && item.itemType == ItemType.Consumable)
        {
            networkView.RPC("ConsumeItem", RPCMode.All, itemName);
        }
    }

    [RPC]
    void ConsumeItem(string itemName)
    {
        Item item = this.Get(itemName);
        ((Consumable)item).Consume(this.gameObject);
        Items.Remove(item);
    }

    [RPC]
    void AddItemToInventory(string itemName)
    {
        var newItem = ItemDirectory.Get(itemName);
        Items.Add(newItem);
        if (this.gameObject.IsMyLocalPlayer())
        {
            PopupMessage.LocalDisplay("You picked up a " + itemName + ".");
        }

        OnItemAdded(newItem);
    }

    private void OnItemAdded(Item newItem)
    {
        if (ItemAdded != null)
        {
            ItemAdded(this, newItem);
        }
    }

    [RPC]
    void InsufficientGoldMessage()
    {
        PopupMessage.LocalDisplay("You don't have enough gold to buy that!");
    }

    /// <summary>
    /// Sends all necessary information to synchronize this object with a newly connected player
    /// </summary>
    /// <param name="player"></param>
    public void SynchronizePlayer(NetworkPlayer player)
    {
        if (Network.isClient) return;

        //Add all currently-held items to remote client's inventory
        foreach (Item i in Items)
        {
            networkView.RPC("AddItemToInventory", player, i.name);
        }

        //Equip items on remote client
        networkView.RPC("CommitEquipItem", player, currentWeapon.name, (int)currentWeapon.itemType);
        networkView.RPC("CommitEquipItem", player, currentShield.name, (int)currentShield.itemType);

    }
}

public delegate void WeaponChangedEventHandler(GameObject sender, WeaponChangedEventArgs e);

public class WeaponChangedEventArgs
{
    public Weapon newWeapon;
    public Weapon oldWeapon;
}