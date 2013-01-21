﻿using System.Xml.Serialization;
using UnityEngine;

public class Consumable : Item
{
    [XmlAttribute("buffScript")]
    public string buffScript;

    public void Consume(GameObject consumer)
    {
        consumer.AddComponent(buffScript);
    }

    public override void Use(GameObject target)
    {
        container.TryConsumeItem(this.name);
    }
}