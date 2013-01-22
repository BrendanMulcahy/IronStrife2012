using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EternityGUI;
using System;

[PlayerComponent(PlayerScriptType.AllDisabled, PlayerScriptType.ClientOwnerEnabled, PlayerScriptType.ServerOwnerEnabled)]
/// <summary>
/// Handles player inventory, equipping items, unequipping items, consuming items, etc.
/// Contains information about currently equipped weapons, armor, shield, etc.
/// Also contains a reference to the inventory panel.
/// </summary>
public class Inventory : MonoBehaviour
{

    /// <summary>
    /// The list of items that a player currently owns.
    /// </summary>
    public List<Item> Items { get; set; }

    public Item this[int index]
    {
        get
        {
            return Items[index];
        }
    }

    // Is the inventory GUI visible?
    bool visible = false;

    /// <summary>
    /// Currently equipped weapon
    /// </summary>
    public Weapon currentWeapon;
    /// <summary>
    /// Currently equipped shield
    /// </summary>
    public Shield currentShield;
    /// <summary>
    /// Equipped weapon's GameObject
    /// </summary>
    public GameObject currentWeaponGameobject;
    /// <summary>
    /// Equipped shield's GameObject
    /// </summary>
    public GameObject currentShieldGameObject;
    /// <summary>
    /// Returns the currently equipped weapon's type
    /// </summary>
    public WeaponType CurrentWeaponType { get { if (currentWeapon != null) return currentWeapon.weaponType; else return WeaponType.None; } }
    /// <summary>
    /// Event that is fired when this player changes his currently equipped weapon
    /// </summary>
    public event WeaponChangedEventHandler WeaponChanged;
    /// <summary>
    /// The player's inventory panel. Only exists on the owner's client and is null for everyone else.
    /// </summary>
    private InventoryPanel inventoryPanel;

    private int _gold;
    /// <summary>
    /// Returns the player's current gold amount
    /// </summary>
    public int Gold { get { return _gold; } set { _gold = value; } }

    public delegate void ItemAddedEventHandler(Inventory sender, Item newItem);
    public delegate void ItemRemovedEventHandler(Inventory sender, Item newItem);

    /// <summary>
    /// Event that is fired when an item is added
    /// </summary>
    public event ItemAddedEventHandler ItemAdded;
    /// <summary>
    /// Event that is fired when an item is removed (dropped or used)
    /// </summary>
    public event ItemRemovedEventHandler ItemRemoved;

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

    /// <summary>
    /// Attempts to equip the selected EquippableItem.
    /// </summary>
    /// <param name="item"></param>
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
        if ((item = (EquippableItem)this.Get(itemName)) != null && item is EquippableItem)
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
            Debug.LogError(e.Message + "\n" + gameObject.name + " tried to equip something that was not valid.");

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
        var itemToPurchase = ItemFactory.Get(itemName);
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

    /// <summary>
    /// Attempts to purchase the given item from the given shop.
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="shopID"></param>
    public void TryPurchaseItem(string itemName, NetworkViewID shopID)
    {
        var itemToPurchase = ItemFactory.Get(itemName);
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

    /// <summary>
    /// Attempts to consume the given item from this inventory.
    /// </summary>
    /// <param name="itemName"></param>
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
        if (item != null && item.itemType == ItemType.Consumable)
        {
            networkView.RPC("ConsumeItem", RPCMode.All, itemName);
        }
    }

    [RPC]
    void CommitConsumeItem(string itemName)
    {
        Item item = this.Get(itemName);
        ((Consumable)item).Consume(this.gameObject);
        Items.Remove(item);
        OnItemRemoved(item);

    }

    /// <summary>
    /// Adds the given item to this inventory.
    /// </summary>
    /// <param name="itemName"></param>
    [RPC]
    void AddItemToInventory(string itemName)
    {
        var newItem = ItemFactory.Get(itemName);
        Items.Add(newItem);
        if (this.gameObject.IsMyLocalPlayer())
        {
            PopupMessage.LocalDisplay("You picked up a " + itemName + ".");
        }
        newItem.container = this;

        OnItemAdded(newItem);
    }

    /// <summary>
    /// Removes the given item from this inventory.
    /// </summary>
    /// <param name="itemName"></param>
    [RPC]
    void RemoveItemFromInventory(string itemName)
    {
        var toRemove = ItemFactory.Get(itemName);
        Items.Remove(toRemove);

        OnItemRemoved(toRemove);
    }

    private void OnItemAdded(Item newItem)
    {
        if (ItemAdded != null)
        {
            ItemAdded(this, newItem);
        }
    }

    private void OnItemRemoved(Item removed)
    {
        if (ItemRemoved != null)
        {
            ItemRemoved(this, removed);
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
            networkView.RPCToGroup("AddItemToInventory", 2, player, i.name);
        }

        //Equip items on remote client
        networkView.RPCToGroup("CommitEquipItem", 2, player, currentWeapon.name, (int)currentWeapon.itemType);
        networkView.RPCToGroup("CommitEquipItem", 2, player, currentShield.name, (int)currentShield.itemType);

    }

    /// <summary>
    /// Attempts to drop the given item
    /// </summary>
    /// <param name="item"></param>
    internal void TryDropItem(Item item)
    {
        if (Network.isServer)
            ServerTryDropItem(item.name);
        else
        {
            networkView.RPC("ServerTryDropItem", RPCMode.Server, item.name);
        }
    }

    [RPC]
    void ServerTryDropItem(string name)
    {
        var newItemView = Network.AllocateViewID();
        networkView.RPC("CommitDropItem", RPCMode.All, name, newItemView);
    }

    [RPC]
    void CommitDropItem(string name, NetworkViewID newItemView)
    {
        var toDrop = Get(name);
        Debug.Log("You are trying to drop " + toDrop.name + " goldcost: " + toDrop.goldCost);

        GameObject worldItemPrefab = Resources.Load("Items/WorldItems/" + name) as GameObject;
        if (!worldItemPrefab)
        {
            worldItemPrefab = Resources.Load("Items/WorldItems/PlaceholderItem") as GameObject;
            worldItemPrefab.name = name + "_Placeholder";
            worldItemPrefab.AddComponent<WorldItem>().itemName = name;
        }

        var instance = GameObject.Instantiate(worldItemPrefab) as GameObject;
        instance.transform.position = this.transform.position + this.transform.forward * 1.0f + Vector3.up * 2f;
        instance.networkView.viewID = newItemView;

        RemoveItemFromInventory(name);

    }
}

public delegate void WeaponChangedEventHandler(GameObject sender, WeaponChangedEventArgs e);

public class WeaponChangedEventArgs
{
    public Weapon newWeapon;
    public Weapon oldWeapon;
}