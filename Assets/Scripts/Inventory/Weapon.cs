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

    [XmlAttribute("baseAttackTime")]
    public float baseAttackTime;

    public Weapon()
    {
        equipLocationPath = "MainC/ROOTJ/Spine01J/Spine02J/Spine03J/Spine04J/rClavicleJ/rShoulderJ/rElbowJ/rWristJ";

    }

    public override GameObject Equip(GameObject target)
    {
        var go = base.Equip(target);
        go.SetLayerRecursively(17);
        return go;
    }

    public override string TooltipText
    {
        get
        {
            var text = base.TooltipText;
            text += "\nDamage: " + damage;
            text += "\nSpeed: " + baseAttackTime;
            return text;
        }
    }
}

