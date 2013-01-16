using UnityEngine;
namespace EternityGUI
{
    public class ItemElement : BaseImage
    {
        public Item item;

        public static ItemElement Create(Item item)
        {
            var go = new GameObject(item.name + "InventoryIcon");
            go.layer = 12;
            var gt = go.AddComponent<GUITexture>();
            var tex = item.inventoryIcon;
            var baseElement = go.AddComponent<ItemElement>();
            gt.texture = tex;
            gt.pixelInset = new Rect(0, 0, tex.width, tex.height);
            gt.transform.position = new Vector3();
            gt.transform.localScale = new Vector3();

            baseElement.item = item;

            return baseElement;
        }
    }
}