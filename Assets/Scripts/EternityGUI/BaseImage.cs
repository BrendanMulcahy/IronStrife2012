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

        /// <summary>
        /// Creates a new BaseImage with the given texture and position. Must provide a fully qualified resource path as the image name.
        /// </summary>
        /// <param name="imageName">Full Resources folder path and name of file to use as the texture</param>
        /// <param name="position">Screen position to place the element at</param>
        public static BaseImage Create(string imageName, Vector3 position)
        {
            var go = new GameObject(imageName + "BaseElement");
            go.layer = 12;
            var gt = go.AddComponent<GUITexture>();
            var tex = Resources.Load(imageName) as Texture2D;
            var baseImage = go.AddComponent<BaseImage>();
            gt.texture = tex;
            gt.pixelInset = new Rect(0, 0, tex.width, tex.height);
            gt.transform.position = position.ScreenToViewport();
            gt.transform.localScale = new Vector3();

            return baseImage;
        }

        public static BaseImage Create(Texture2D tex, Vector3 position)
        {
            var go = new GameObject(tex.name + "BaseElement");
            var gt = go.AddComponent<GUITexture>();
            var baseImage = go.AddComponent<BaseImage>();
            gt.texture = tex;
            gt.pixelInset = new Rect(0, 0, tex.width, tex.height);
            gt.transform.position = position;
            gt.transform.localScale = new Vector3();

            return baseImage;
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

        internal void SetHeight(int newHeight)
        {
            Resize((int)this.guiTexture.pixelInset.width, newHeight);
        }

        internal void SetWidth(int newWidth)
        {
            Resize(newWidth, (int)this.guiTexture.pixelInset.height);
        }
    }
}