using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Represents an equippable item. This class can be used by itself for an equippable cosmetic item that has no side effects when worn.
/// </summary>
public class EquippableItem : Item
{
    [XmlAttribute("rotationX")]
    public float rotationX = 0;
    [XmlAttribute("rotationY")]
    public float rotationY = 0;
    [XmlAttribute("rotationZ")]
    public float rotationZ = 0;

    [XmlAttribute("positionX")]
    public float positionX = 0;
    [XmlAttribute("positionY")]
    public float positionY = 0;
    [XmlAttribute("positionZ")]
    public float positionZ = 0;

    /// <summary>
    /// The name of this item's model
    /// </summary>
    [XmlAttribute("modelAssetName")]
    public string modelAssetName;

    /// <summary>
    /// The transform equip location to use.
    /// </summary>
    [XmlAttribute("equipLocationPath")]
    public string equipLocationPath;
    // TODO : Replace this with a smart tag system for equip locations. Automatically find a child with some tag and equip an item to that location.

    /// <summary>
    /// Use this item from the inventory. AKA equip it to your player.
    /// </summary>
    /// <param name="target"></param>
    public override void Use(GameObject target)
    {
        container.TryEquipItem(this);
    }

    /// <summary>
    /// Equips the item on the target Game Object. Attempts to find the child object of the GO to attach it to according to the "equipLocationPath" field.
    /// Rotates and translates the item according to those fields, as well.
    /// </summary>
    /// <param name="target">The gameObject to equip this item to.</param>
    /// <returns>Returns the game object of the equipped item.</returns>
    public virtual GameObject Equip(GameObject target)
    {
        var itemPrefab = Resources.Load(modelAssetName) as Object;
        var itemGameObject = GameObject.Instantiate(itemPrefab) as GameObject;
        var location = target.transform.Find(equipLocationPath);
        itemGameObject.transform.parent = location;
        itemGameObject.transform.localPosition = new Vector3(positionX, positionY, positionZ);
        itemGameObject.transform.localRotation = Quaternion.Euler(new Vector3(rotationX, rotationY, rotationZ));
        itemGameObject.layer = LayerMask.NameToLayer("Player");
        return itemGameObject;
    }
}

