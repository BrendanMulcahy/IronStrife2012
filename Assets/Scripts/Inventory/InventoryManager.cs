using UnityEngine;
using System.Collections;

/// <summary>
/// Handles player inventory, equipping items, unequipping items.
/// Contains information about currently equipped weapons, armor, shield, etc.
/// Also contains GUI functions for displaying that information
/// </summary>
public class InventoryManager : MonoBehaviour
{

    /// <summary>
    /// The list of items that a player currently owns.
    /// </summary>
    public ArrayList Items { get; set; }

    // Is the inventory GUI visible?
    bool visible = false;

    // Currently equipped gear
    public Weapon currentWeapon;
    public Shield currentShield;
    public GameObject currentWeaponGameobject;
    public GameObject currentShieldGameObject;
    public WeaponType CurrentWeaponType { get { if (currentWeapon != null) return currentWeapon.weaponType; else return WeaponType.None; } }

    public event WeaponChangedEventHandler weaponChanged;

    private Rect inventoryWindowRect = new Rect(70, 70, 600, 400);

    void Awake()
    {
        Items = new ArrayList();
    }

    void OnGUI()
    {
        if (visible)
        {
            GUI.Window(0, inventoryWindowRect, ShowInventoryWindow, "Inventory");
        }
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
    }

    void ShowInventoryWindow(int id)
    {
        GUILayout.BeginVertical();
        foreach (EquippableItem item in Items)
        {
            if (GUILayout.Button(item.name))
            {
                TryEquipItem(item);
            }
        }
        GUILayout.EndVertical();
        GUI.DragWindow();
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
        if ((item = (EquippableItem)this.Contains(itemName)) !=null && item is EquippableItem)
        {
            networkView.RPC("CommitEquipItem", RPCMode.All, itemName, (int)item.itemType);
        }
        else
        {
            DebugGUI.Print(gameObject.name + " tried to equip " + itemName + " but he doesn't have one.");
        }
    }

    private Item Contains(string itemName)
    {
        foreach (Item item in Items)
        {
            if (item.name == itemName)
                return item;
        }
        return null;
    }

    [RPC] void CommitEquipItem(string itemName, int itemType)
    {
        DebugGUI.Print("Trying to equip " + itemName + " of type " + ((ItemType)itemType).ToString());
        EquippableItem item = new EquippableItem();
        switch ((ItemType)itemType)
        {
            case ItemType.Weapon:
                item = Item.FromName<Weapon>(itemName);
                break;
            case ItemType.Shield:
                item = Item.FromName<Shield>(itemName);
                break;
                // And so on....


        }
        Weapon oldWeapon = null;
        var GO = item.Equip(gameObject);
        if (item is Weapon)
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
            traj.SetWeaponGameObject(GO);

            if (weaponChanged!=null)
            weaponChanged(new WeaponChangedEventArgs() { oldWeapon = oldWeapon, newWeapon = currentWeapon });

        }
        if (item is Shield)
        {
            if (currentWeapon.numHands == 2)
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

    [RPC] void CommitUnequipItem(int itemType)
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

}

public delegate void WeaponChangedEventHandler(WeaponChangedEventArgs e);

public class WeaponChangedEventArgs
{
    public Weapon newWeapon;
    public Weapon oldWeapon;
}