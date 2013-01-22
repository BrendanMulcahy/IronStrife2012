using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class that stores a reference to every item in the game. The "Get" method generates an item from its name and returns a reference to it
/// </summary>
public static class ItemFactory
{
    private static Dictionary<string, Item> items = new Dictionary<string, Item>();
    private static bool initialized = false;
    private static Dictionary<string, ItemType> itemTypes = new Dictionary<string, ItemType>();

    /// <summary>
    /// Returns an item given the name
    /// </summary>
    /// <param name="key">The name of the item</param>
    /// <returns>An Item object identified by the given name</returns>
    public static Item Get(string key)
    {
        if (!initialized) Initialize();

        if (items.ContainsKey(key))
        {
            var type = itemTypes[key];
            switch (type)
            {
                case ItemType.Consumable:
                    return Item.FromName<Consumable>(key);
                case ItemType.Weapon:
                    return Item.FromName<Weapon>(key);
                case ItemType.Shield:
                    return Item.FromName<Shield>(key);
                case ItemType.Armor:
                    return Item.FromName<Armor>(key);
                //case ItemType.Accessory:
                //    return Item.FromName<Accessory>(key);

                default:
                    Debug.LogError("Invalid item type.");
                    return null;
            }
        }
        else
            return null;
    }

    /// <summary>
    /// Returns a Dictionary of all items in the game.
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, Item> GetAllItems()
    {
        if (!initialized) Initialize();
        return items;
    }

    [StaticAutoLoad]
    public static void Initialize()
    {
        if (initialized) return;

        Object[] weaponXML = Resources.LoadAll("Items/Weapon", typeof(TextAsset));
        foreach (Object o in weaponXML)
        {
            var ta = (TextAsset)o;
            Weapon w = Item.FromName<Weapon>(ta.name);
            items.Add(ta.name, w);
            itemTypes.Add(ta.name, w.itemType);
        }
        Object[] shieldXML = Resources.LoadAll("Items/Shield", typeof(TextAsset));
        foreach (Object o in shieldXML)
        {
            var ta = (TextAsset)o;
            Shield s = Item.FromName<Shield>(ta.name);
            items.Add(ta.name, s);
            itemTypes.Add(ta.name, s.itemType);
        }
        Object[] consumableXML = Resources.LoadAll("Items/Consumable", typeof(TextAsset));
        foreach (Object o in consumableXML)
        {
            var ta = (TextAsset)o;
            Consumable c = Item.FromName<Consumable>(ta.name);
            items.Add(ta.name, c);
            itemTypes.Add(ta.name, c.itemType);
        }
        initialized = true;
    }

    /// <summary>
    /// Gets an item and allocates it a networkviewid so it can be uniquely identified over the network.
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public static Item CreateItemForPlayer(string itemName)
    {
        if (Network.isClient)
        {
            Debug.LogError("Only the server can create an item for a player.");
            return null;
        }
        var itemToReturn = ItemFactory.Get(itemName);
        itemToReturn.viewID = Network.AllocateViewID();
        ItemFactory.ItemViewIDs[itemToReturn.viewID] = itemToReturn;

        return itemToReturn;
    }

    /// <summary>
    /// Static dictionary containing mappings of all item NetworkViewIDs to the items they represent.
    /// </summary>
    internal static Dictionary<NetworkViewID, Item> ItemViewIDs = new Dictionary<NetworkViewID, Item>();

    /// <summary>
    /// Gets an Item from a given NetworkViewID
    /// </summary>
    /// <param name="viewID"></param>
    /// <returns></returns>
    internal static Item GetFromViewID(NetworkViewID viewID, string itemName)
    {
        if (!ItemViewIDs.ContainsKey(viewID))
        {
            var item = ItemFactory.Get(itemName);
            item.viewID = viewID;
            ItemViewIDs[viewID] = item;
            Debug.LogWarning("Setting " + viewID + " to the item " + item.name + " created from itemName " + itemName);


        }
        if (ItemViewIDs[viewID].name != itemName)
        {
            Debug.LogError("A NetworkViewID was assigned to two items: " + ItemViewIDs[viewID].name + " and " + itemName + ".");
        }

        return ItemViewIDs[viewID];
    }
}