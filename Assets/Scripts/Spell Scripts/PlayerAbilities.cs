using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public static class PlayerAbilities
{
    private static ArrayList spells = new ArrayList();
    private static Dictionary<string, Spell> spellDictionary = new Dictionary<string, Spell>();
    public static Spell GetSpell(int g) { if (!isInitialized) Initialize(); return spells[g] as Spell; }
    private static bool isInitialized = false;

    public static Spell GetSpell(string s)
    {
        if (!isInitialized) Initialize();
        return spellDictionary[s];
    }

    public static void LoadAllSpells()
    {
        spells.Add(null);
        var allAssemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
        {
            foreach (Type t in allAssemblyTypes)
            {
                if (t.IsSubclassOf(typeof(Spell)))
                {
                    Spell instance = Activator.CreateInstance(t) as Spell;
                    spells.Add(instance);
                    spellDictionary.Add(instance.name, instance);
                    
                }
            }
        }
    }

    static void Initialize()
    {
        LoadAllSpells();

        isInitialized = true;
    }

    public static String StringList()
    {
        if (!isInitialized) Initialize();
        String s = "";
        s += "Spells :\n";
        foreach (Spell spell in spells)
        {
            if (spell == null) continue;
            s += spells.IndexOf(spell) + ") " + spell.name + "\n";
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