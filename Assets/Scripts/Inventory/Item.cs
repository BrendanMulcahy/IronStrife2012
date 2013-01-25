using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using I18N;
using System.Xml;

/// <summary>
/// Represents the base class for a player item. Other classes like EquippableItem and Consumable implement these features.
/// </summary>
public abstract class Item
{
    /// <summary>
    /// The item's name
    /// </summary>
    [XmlAttribute("name")]
    public string name;

    /// <summary>
    /// A description of the item. Displayed when looking at the item in a shop or your inventory
    /// </summary>
    [XmlAttribute("description")]
    public string description;

    /// <summary>
    /// The type of this item
    /// </summary>
    [XmlAttribute("itemType")]
    public ItemType itemType;

    /// <summary>
    /// Cost to purchase this item from a shop
    /// </summary>
    [XmlAttribute("goldCost")]
    public int goldCost;

    /// <summary>
    /// The rarity of this item. Determines which shops carry it.
    /// </summary>
    [XmlAttribute("availability")]
    public ItemAvailability availability;

    /// <summary>
    /// The inventory this item is contained in
    /// </summary>
    [NonSerialized][XmlIgnore]
    public Inventory container;
    /// <summary>
    /// The NetworkViewID uniquely identifying this item.
    /// </summary>
    [NonSerialized][XmlIgnore]
    public NetworkViewID viewID;

    private Texture2D _inventoryIcon;
    /// <summary>
    /// The 2D icon of this item
    /// </summary>
    public Texture2D inventoryIcon
    {
        get 
        {
            if (_inventoryIcon == null)
            {
                _inventoryIcon = Resources.Load("Items/Icons/" + name) as Texture2D;

                if (_inventoryIcon == null) 
                    _inventoryIcon = Resources.Load("Items/Icons/DefaultIcon") as Texture2D;
            }
            return _inventoryIcon;
        }
    }

    /// <summary>
    /// Creates an item from a name.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public static T FromName<T>(string itemName) where T : Item
    {
        var type = typeof(T);
        TextAsset xmlFile = Resources.Load("Items/" + type.ToString() + "/" + itemName, typeof(TextAsset)) as TextAsset;
        if (!xmlFile) return null;
        Byte[] bytes = Encoding.UTF8.GetBytes(xmlFile.text);
        using (MemoryStream stream = new MemoryStream(bytes))
        {
            var item = (T)(new XmlSerializer(typeof(T))).Deserialize(stream);
            return item;
        }
    }

    public void Serialize()
    {
        Type type = this.GetType();
        using (XmlTextWriter writer = new XmlTextWriter(File.Create(Application.dataPath + "/Resources/Items/" + type.ToString() + "/" + name + "2.xml"), Encoding.UTF8))
        {
            XmlSerializer serializer = new XmlSerializer(type);
            serializer.Serialize(writer, this);
        }
    }

    public char GetItemID()
    {
        return ItemFactory.GetIndex(this.name);
    }

    /// <summary>
    /// Uses this item.
    /// </summary>
    public abstract void Use(GameObject target);
}

/// <summary>
/// Describes the availability of an item in a store. Default is "Regular" (if undefined in XML file)
/// </summary>
public enum ItemAvailability
{
    /// <summary>
    /// Available in a regular store
    /// </summary>
    Regular = 0,

    /// <summary>
    /// Available in a special store
    /// </summary>
    Rare,

    /// <summary>
    /// Not available in stores
    /// </summary>
    Unavailable,


}
