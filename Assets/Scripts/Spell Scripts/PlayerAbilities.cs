using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;

public static class PlayerAbilities
{
    private static ArrayList spells = new ArrayList();
    public static Spell GetSpell(int g) { if (!isInitialized) Initialize(); return spells[g] as Spell; }
    private static bool isInitialized = false;

    public static void LoadAllSpells()
    {
        spells.Add(null);
        var allAssemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
        {
            foreach (Type t in allAssemblyTypes)
            {
                if (t.IsSubclassOf(typeof(Spell)))
                {
                    spells.Add(Activator.CreateInstance(t));
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