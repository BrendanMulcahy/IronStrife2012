using System.Collections.Generic;
using UnityEngine;

namespace EternityGUI
{
    public class Panel : BaseElement
    {
        public List<UIElementContainer> containers = new List<UIElementContainer>();

        public int width;
        public int height;

        public GameObject background;

        public static Panel Create(string backgroundImage, int width, int height)
        {
            var panel = new GameObject(backgroundImage + "Panel").AddComponent<Panel>();
            panel.gameObject.layer = 12;
            panel.width = width; 
            panel.height = height;
            var gt = panel.gameObject.AddComponent<GUITexture>();
            gt.texture = Resources.Load(backgroundImage) as Texture2D;
            gt.pixelInset = new Rect(0, 0, gt.texture.width, gt.texture.height);

            return panel;
        }

        public void AddContainer(UIElementContainer newContainer)
        {
            newContainer.transform.parent = this.transform;
            containers.Add(newContainer);
        }
    }
}
