using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Spell
{
    public float castTime;
    public int manaCost;
    public GameObject caster;
    public Texture2D spellImage;

    public abstract String name { get; }
    public abstract SpellAffectType AffectType { get; }

    public Spell()
    {
        this.GetSpellIcon();
        this.InitializeSpellValues();
    }

    protected abstract void InitializeSpellValues();
    
    public static explicit operator int(Spell s)
    {
        return (PlayerAbilities.IndexOf(s));
    }

    public static explicit operator Spell(int i)
    {
        return PlayerAbilities.GetSpell(i);
    }

    public void GetSpellIcon()
    {
        string teamColor = Util.MyLocalPlayerTeam == 1 ? "Blue" : "Red";
        Debug.Log("Team color is " + teamColor);
        spellImage = Resources.Load("SpellIcons/" + name + teamColor) as Texture2D;

        if (spellImage == null)
        {
            Debug.Log("No spell icon was found for " + name + ". Loading the default one.");
            spellImage = Resources.Load("SpellIcons/Cast" + teamColor) as Texture2D;

        }
    }

    public SpellTargetType TargetType
    {
        get
        {
            if (this is ITargetSpell)
                return SpellTargetType.Target;
            if (this is ISelfSpell)
                return SpellTargetType.Self;
            if (this is IPointSpell)
                return SpellTargetType.Point;

            else return 0;
        }
    }

}

public enum SpellTargetType { Self, Target, Point, Area, Global } //how is this spell targetted
public enum SpellAffectType { Allies, Enemies, Neutrals, All } //who does this spell affect