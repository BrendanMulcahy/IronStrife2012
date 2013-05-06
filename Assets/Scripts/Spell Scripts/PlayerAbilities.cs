using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

public static class PlayerAbilities
{
    private static ArrayList spells = new ArrayList();
    private static Dictionary<string, Spell> spellDictionary = new Dictionary<string, Spell>();
    public static Spell GetSpell(int g) { if (!isInitialized) Initialize(); return spells[g] as Spell; }
    private static bool isInitialized = false;

    public static Spell GetSpell(string s)
    {
        if (!isInitialized) Initialize();
        Debug.Log("Finding spell " + s);
        return spellDictionary[s];
    }

    public static void LoadAllSpells()
    {
        spells.Add(null);
        var allAssemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
        {
            foreach (Type t in allAssemblyTypes)
            {
                if (t.IsSubclassOf(typeof(Spell)) && !t.IsAbstract)
                {
                    Spell instance = Activator.CreateInstance(t) as Spell;
                    spells.Add(instance);
                    spellDictionary.Add(instance.Name, instance);
                    
                }
            }
        }
    }

    [StaticAutoLoad]
    public static void Initialize()
    {
        if (!isInitialized)
        {
            LoadAllSpells();
            isInitialized = true;
        }
    }

    public static String StringList()
    {
        if (!isInitialized) Initialize();
        String s = "";
        s += "Spells :\n";
        foreach (Spell spell in spells)
        {
            if (spell == null) continue;
            s += spells.IndexOf(spell) + ") " + spell.Name + "\n";
        }
        return s;
    }

    internal static ArrayList AllSpells()
    {
        if (!isInitialized)
            Initialize();
        return spells;
    }

    internal static int IndexOf(Spell s)
    {
        return spells.IndexOf(s);
    }
}