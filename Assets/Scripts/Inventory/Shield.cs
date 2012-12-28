﻿using System;
using UnityEngine;
using System.Xml.Serialization;

[XmlRoot("Shield")]
public class Shield : EquippableItem
{
    [XmlAttribute("blockAmount")]
    public int blockAmount;

    public Shield()
    {
        equipLocationPath = "MainC/ROOTJ/Spine01J/Spine02J/Spine03J/Spine04J/lClavicleJ/lShoulderJ/lElbowJ/lForearmJ";
    }
}
