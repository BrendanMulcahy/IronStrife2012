using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using I18N;
using System.Xml;

/// <summary>
/// Represents the base class for a player item. Other classes like EquippableItem implement these features.
/// </summary>
public abstract class Item
{

    [XmlAttribute("name")]
    public string name;

    [XmlAttribute("description")]
    public string description;

    [XmlAttribute("itemType")]
    public ItemType itemType;

    [XmlAttribute("goldCost")]
    public int goldCost;

    [XmlAttribute("availability")]
    public ItemAvailability availability;

    private Texture2D _inventoryIcon;
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
