using UnityEngine;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System;

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

