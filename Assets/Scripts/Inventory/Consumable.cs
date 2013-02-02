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

    [XmlAttribute("buffParameters")]
    public string[] buffParameters;

    /// <summary>
    /// names of the parameters for display in the tooltip.
    /// </summary>
    [XmlAttribute("parameterNames")]
    public string[] parameterNames;

    /// <summary>
    /// Called when a player consumes this item.
    /// </summary>
    /// <param name="consumer"></param>
    public virtual void Consume(GameObject consumer)
    {
        var buff = consumer.AddComponent(buffScript) as ItemEffect;
        buff.parameters = buffParameters;

    }

    /// <summary>
    /// Called when a user uses this item from their inventory.
    /// </summary>
    /// <param name="target"></param>
    public override void Use(GameObject target)
    {
        container.TryConsumeItem(this);
    }

    public override string TooltipText
    {
        get
        {
            var text =  base.TooltipText;
            for (int g = 0; g < buffParameters.Length; g++)
            {
                text += "\n";
                text += parameterNames[g].Replace('_', ' ') + ": " + buffParameters[g];
            }
            return text;
        }
    }
}