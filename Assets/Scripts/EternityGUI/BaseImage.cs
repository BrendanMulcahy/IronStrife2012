namespace EternityGUI
{
    using UnityEngine;

    /// <summary>
    /// A basic button object. Provides callback for clicking, dragging, etc.
    /// </summary>
    public class BaseImage : BaseElement
    {
        public bool preserveAspectRatio = true;
        protected float _textureRatio = -1;
        protected virtual float TextureRatio
        {
            get
            {
                if (_textureRatio == -1)
                {
                    _textureRatio = (float)guiTexture.texture.width / (float)guiTexture.texture.height;
                }
                return _textureRatio;
            }
        }

        void Start() { draggable = true; }

        internal override void ResetSize()
        {
            var tex = guiTexture.texture;
            guiTexture.pixelInset = new Rect(0, 0, tex.width, tex.height);
        }

        internal override void Resize(int newWidth, int newHeight)
        {
            this.transform.localScale = new Vector3();
            var newInset = new Rect(0, 0, newWidth, newHeight);
            if (preserveAspectRatio)
            {
                if (guiTexture.texture.width > guiTexture.texture.height)
                {
                    newInset.height = newInset.width * (1 / TextureRatio);
                }
                else
                {
                    newInset.width = newInset.height * (TextureRatio);
                }
            }
            this.guiTexture.pixelInset = newInset;
        }
    }
}