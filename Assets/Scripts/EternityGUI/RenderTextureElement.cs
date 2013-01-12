namespace EternityGUI
{
    using UnityEngine;
    public class RenderTextureElement : BaseElement
    {

        public static RenderTextureElement Create(RenderTexture renderTexture, float width, float height)
        {
            var element = new GameObject(renderTexture.name + "Element").AddComponent<RenderTextureElement>();
            element.gameObject.AddComponent<GUITexture>();
            element.gameObject.guiTexture.texture = renderTexture;
            element.preserveAspectRatio = false;
            element.Resize((int)width, (int)height);
            if (width > height)
            {
                renderTexture.width = (int)width; renderTexture.height = (int)width;
            }
            else
            {
                renderTexture.width = (int)height; renderTexture.height = (int)height;
            }
            renderTexture.DiscardContents();

            element.transform.localScale = new Vector3();
            return element;
        }

        protected override void HandleDragging()
        {

        }
    }
}