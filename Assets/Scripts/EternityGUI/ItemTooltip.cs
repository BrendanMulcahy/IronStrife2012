namespace EternityGUI
{
    using UnityEngine;
    using System.Linq;
    using System.Collections;

    public class ItemTooltip : Tooltip
    {
        public BaseText text;
        private ItemElement itemElement;
        Vector3 mouseOffset = new Vector3(.02f, 0, 0);

        public static ItemTooltip Create(ItemElement itemElement)
        {
            var go = new GameObject(itemElement.name + "Tooltip");
            var tooltip = go.AddComponent<ItemTooltip>();
            go.transform.localScale = new Vector3(1, 1, 1);

            tooltip.CreateBackground("WhiteSquareTransparent");

            tooltip.text = BaseText.Create(itemElement.item);

            tooltip.text.guiText.alignment = TextAlignment.Left;
            tooltip.text.guiText.anchor = TextAnchor.UpperLeft;

            tooltip.itemElement = itemElement;
            tooltip.text.transform.parent = tooltip.transform;
            tooltip.text.transform.localScale = new Vector3();
            tooltip.text.layerOffset = 50;
            tooltip.text.guiText.pixelOffset = new Vector2(5, -5);

            tooltip.height = (int)tooltip.text.guiText.GetScreenRect().height;
            tooltip.width = (int)tooltip.text.guiText.GetScreenRect().width;

            var actualTextRect = EternityUtil.FormatGuiTextArea(tooltip.text.guiText, 300);
            tooltip.width = (int)(actualTextRect.width * 1.1f);
            tooltip.height = (int)actualTextRect.height;

            tooltip.background.GetComponent<BaseImage>().preserveAspectRatio = false;
            tooltip.background.GetComponent<BaseImage>().Resize((int)(tooltip.width), (int)tooltip.height);

            tooltip.background.transform.position -= new Vector3(0, tooltip.height, 0).ScreenToViewport();

            itemElement.Destroyed += tooltip.itemRemovedFromInventory;

            EternityUtil.CurrentTooltip = tooltip;
            return tooltip;
        }

        private void itemRemovedFromInventory(BaseElement sender)
        {
            if (this)
                Destroy(this.gameObject);
        }

        void CreateBackground(string backgroundName)
        {
            var baseImage = BaseImage.Create(backgroundName, new Vector3());
            baseImage.gameObject.layer = 12;
            baseImage.layerOffset = 10;
            baseImage.Resize(300, 450);
            background = baseImage.gameObject;
            background.transform.parent = this.transform;
            background.transform.position = new Vector3();

        }

        public override void Update()
        {
            this.transform.position = Input.mousePosition.ScreenToViewport() + mouseOffset;

            if (Input.mousePosition.y - height < 0)
            {
                var pos = this.transform.position;
                var verticalOffset = height - Input.mousePosition.y;
                pos.y += verticalOffset / Screen.height;
                this.transform.position = pos;
            }
            if (Input.mousePosition.x + width > Screen.width)
            {
                var pos = this.transform.position;
                var horizontalOffset = Input.mousePosition.x + width - Screen.width;
                pos.x -= horizontalOffset / Screen.width;
                this.transform.position = pos;
            }

            if (!EternityUtil.HitTestAll(Input.mousePosition).ToList().Contains(itemElement.guiTexture))
            {
                Destroy(this.gameObject);
            }
        }
    }
}