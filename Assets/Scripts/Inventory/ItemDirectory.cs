﻿using UnityEngine;
using System.Collections.Generic;

public static class ItemDirectory
{
    private static Dictionary<string, Item> items = new Dictionary<string, Item>();
    private static bool initialized = false;

    public static Item Get(string key)
    {
        if (!initialized) Initialize();
        if (items.ContainsKey(key))
            return items[key];
        else
            return null;
    }

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
            items.Add(ta.name, Weapon.FromName<Weapon>(ta.name));
        }
        Object[] shieldXML = Resources.LoadAll("Items/Shield", typeof(TextAsset));
        foreach (Object o in shieldXML)
        {
            var ta = (TextAsset)o;
            items.Add(ta.name, Shield.FromName<Shield>(ta.name));
        }
        Object[] consumableXML = Resources.LoadAll("Items/Consumable", typeof(TextAsset));
        foreach (Object o in consumableXML)
        {
            var ta = (TextAsset)o;
            items.Add(ta.name, Consumable.FromName<Consumable>(ta.name));
        }
        initialized = true;
    }
}