namespace EternityGUI
{
    using UnityEngine;

    public class BaseText : BaseElement
    {
        void Start()
        {
            this.draggable = false;
        }

        public static BaseText Create(Item i)
        {
            Color rarityColor = Color.black;
            if (i.availability == ItemAvailability.Unavailable) rarityColor = Color.red;
            else if (i.availability == ItemAvailability.Rare) rarityColor = Color.yellow;

            var go = new GameObject(i.name + "TooltipText");
            var text = go.AddComponent<GUIText>();
            var baseText = go.AddComponent<BaseText>();
            text.font = Util.OFLGoudyStMTT;
            text.text = "<size=24>" + i.name + "</size> " + "<i><size=14>" + i.availability + " " + i.itemType + "</size></i>\n" + i.description + "\nGold Cost: " + i.goldCost;

            text.fontSize = 18;
            text.anchor = TextAnchor.UpperLeft;
            text.alignment = TextAlignment.Left;

            return baseText;
        }

        internal override void ResetSize()
        {

        }

        internal override void Resize(int newWidth, int newHeight)
        {

        }
    }
}