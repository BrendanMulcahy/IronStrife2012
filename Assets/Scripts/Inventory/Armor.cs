using UnityEngine;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System;

/// <summary>
/// Represents an armor item object.
/// </summary>
[XmlRoot("Armor")]
public class Armor : EquippableItem
{
    [XmlAttribute("defense")]
    public float defense;

    public Armor()
    {
        name = "defaultArmor";
    }
}

