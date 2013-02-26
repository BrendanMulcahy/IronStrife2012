using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public abstract class Spell
{
    //Initialize these values in InitializeSpellValues()
    public float castTime;
    public int manaCost;
    public GameObject caster;
    public Texture2D spellImage;
    public float cooldown;

    private string _name;
    public string Name { get { return _name; } }


    private SpellAffectType _affectType = SpellAffectType.Enemies;
    public SpellAffectType AffectType { get { return _affectType; } }

    public Spell()
    {
        this.InitializeSpellValues();
        this.GetSpellIcon();

    }

    /// <summary>
    /// Initializes castTime and manaCost
    /// </summary>
    void InitializeSpellValues()
    {
        var textAsset = Resources.Load("Spells/" + this.GetType()) as TextAsset;
        var stream = new MemoryStream(textAsset.bytes);
        XmlDocument doc = new XmlDocument();
        doc.Load(stream);
        this.LoadSpellValuesFromXML(doc.SelectSingleNode("Spell").Attributes);
    }

    protected virtual void LoadSpellValuesFromXML(XmlAttributeCollection attributes)
    {
        manaCost = int.Parse(attributes["manaCost"].Value);
        castTime = float.Parse(attributes["castTime"].Value);
        _affectType = (SpellAffectType)Enum.Parse(typeof(SpellAffectType), attributes["affectType"].Value);
        _name = attributes["name"].Value;

        if (attributes["cooldown"] == null)
            cooldown = 0f;
        else
            cooldown = float.Parse(attributes["cooldown"].Value);
    }
    
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
        spellImage = Resources.Load("SpellIcons/" + Name + teamColor) as Texture2D;

        if (spellImage == null)
        {
            //Debug.Log("No spell icon was found for " + name + ". Loading the default one.");
            spellImage = Resources.Load("SpellIcons/Cast" + teamColor) as Texture2D;

        }
    }

    public SpellTargetType TargetType
    {
        get
        {
            if (this is ITargetSpell)
                return SpellTargetType.Target;
            if (this is ISelfSpell || this is ISelfSpellWithViewID)
                return SpellTargetType.Self;
            if (this is IPointSpell)
                return SpellTargetType.Point;

            else return 0;
        }
    }

}

public enum SpellTargetType { Self, Target, Point, Area, Global } //how is this spell targetted
public enum SpellAffectType { Allies, Enemies, Neutrals, All, None } //who does this spell affect