using UnityEngine;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System;
using System.IO;
using I18N;
using System.Text;
using System.Xml;

[XmlRoot("Weapon")]
public class Weapon : EquippableItem
{
    [XmlAttribute("damage")]
    public int damage;

    [XmlAttribute("weaponType")]
    public WeaponType weaponType;

    [XmlAttribute("numHands")]
    public int numHands;

    public Weapon()
    {
        equipLocationPath = "MainC/ROOTJ/Spine01J/Spine02J/Spine03J/Spine04J/rClavicleJ/rShoulderJ/rElbowJ/rWristJ";

    }
}

