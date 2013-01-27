using UnityEngine;
namespace EternityGUI
{
    public class ItemElement : BaseImage
    {
        public Item item;
        public ItemTooltip tooltip;

        public static ItemElement Create(Item item)
        {
            var go = new GameObject(item.name + "InventoryIcon");
            go.layer = 12;
            var gt = go.AddComponent<GUITexture>();
            var tex = item.inventoryIcon;
            var itemElement = go.AddComponent<ItemElement>();
            gt.texture = tex;
            gt.pixelInset = new Rect(0, 0, tex.width, tex.height);
            gt.transform.position = new Vector3();
            gt.transform.localScale = new Vector3();

            itemElement.item = item;

            itemElement.MouseEnter += itemElement.baseElement_MouseEnter;
            //itemElement.MouseLeave += itemElement.itemElement_MouseLeave;

            return itemElement;
        }

        void itemElement_MouseLeave(BaseElement sender, MouseEventArgs e)
        {
            if (tooltip)
                Destroy(tooltip.gameObject);
        }

        void baseElement_MouseEnter(BaseElement sender, MouseEventArgs e)
        {
            if (!tooltip)
            {
                tooltip = ItemTooltip.Create(this);
                tooltip.Destroyed += tooltip_Destroyed;
            }
        }

        void tooltip_Destroyed(BaseElement sender)
        {
            tooltip = null;
        }
    }
}