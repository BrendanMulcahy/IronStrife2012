using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// Class for consumable items, including potions, temporary buffers, etc.
/// </summary>
public class Consumable : Item
{
    /// <summary>
    /// The name of the buff script to add to the user of this item.
    /// </summary>
    [XmlAttribute("buffScript")]
    public string buffScript;

    /// <summary>
    /// Called when a player consumes this item.
    /// </summary>
    /// <param name="consumer"></param>
    public virtual void Consume(GameObject consumer)
    {
        consumer.AddComponent(buffScript);
    }

    /// <summary>
    /// Called when a user uses this item from their inventory.
    /// </summary>
    /// <param name="target"></param>
    public override void Use(GameObject target)
    {
        container.TryConsumeItem(this.name);
    }
}