namespace EternityGUI
{
    using UnityEngine;

    /// <summary>
    /// A basic button object. Provides callback for clicking, dragging, etc.
    /// </summary>
    public class BaseElement : MonoBehaviour
    {

        public event MouseEventHandler Click;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseEnter;
        public event MouseEventHandler MouseLeave;
        public event MouseEventHandler DoubleClick;
        public event MouseEventHandler MouseWheelChanged;
        public event MouseDropEventHandler Dropped;

        float lastClickTime = 0;
        float maxDoubleClickInterval = .25f;
        public Vector3 dragOffset;

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

        private UIElementContainer container;
        /// <summary>
        /// The UIElementContainer that holds this BaseElement. Null if this is a free element.
        /// </summary>
        public UIElementContainer Container { get { return container; } set { container = value; } }

        public bool dragging = false;
        public int layerOffset = 0;

        /// <summary>
        /// Creates a new BaseElement with the given texture and position. Must provide a fully qualified resource path as the image name.
        /// </summary>
        /// <param name="imageName">Full Resources folder path and name of file to use as the texture</param>
        /// <param name="position">Screen position to place the element at</param>
        public static BaseElement Create(string imageName, Vector3 position)
        {
            var go = new GameObject(imageName + "BaseElement");
            go.layer = 12;
            var gt = go.AddComponent<GUITexture>();
            var tex = Resources.Load(imageName) as Texture2D;
            var baseElement = go.AddComponent<BaseElement>();
            gt.texture = tex;
            gt.pixelInset = new Rect(0, 0, tex.width, tex.height);
            gt.transform.position = position.ScreenToViewport();
            gt.transform.localScale = new Vector3();

            return baseElement;
        }

        public static BaseElement Create(Texture2D tex, Vector3 position)
        {
            var go = new GameObject(tex.name + "BaseElement");
            var gt = go.AddComponent<GUITexture>();
            var baseElement = go.AddComponent<BaseElement>();
            gt.texture = tex;
            gt.pixelInset = new Rect(0, 0, tex.width, tex.height);
            gt.transform.position = position;
            gt.transform.localScale = new Vector3();

            return baseElement;
        }

        internal virtual void OnMouseDown()
        {
            if (MouseDown != null)
            {
                MouseDown(this, MouseEventArgs.Current);
            }

            dragOffset = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0) - gameObject.transform.position;
            dragging = true;
        }

        internal virtual void OnMouseUp()
        {
            if (dragging)
            {
                OnDragRelease();
            }
            dragging = false;

            if (MouseUp != null)
            {
                MouseUp(this, MouseEventArgs.Current);
            }

        }

        private void OnDragRelease()
        {
            Debug.Log("Dropped element " + gameObject.name + " at " + Input.mousePosition);
            var guiLayer = Camera.main.GetComponent<GUILayer>();

            var beforePos = this.transform.position;
            this.transform.position = new Vector3(-100, -100, -100);
            var element = guiLayer.HitTest(Input.mousePosition);
            this.transform.position = beforePos;

            if (Dropped != null)
            {
                if (!element)
                {
                    Dropped(this, new MouseDropEventArgs(this, null, Input.mousePosition));
                }
                else
                {
                    Dropped(this, new MouseDropEventArgs(this, element.GetComponent<BaseElement>(), Input.mousePosition));
                }
            }

        }

        internal virtual void OnMouseUpAsButton()
        {
            var currentTime = Time.realtimeSinceStartup;
            if (currentTime - lastClickTime <= maxDoubleClickInterval)
            {
                OnDoubleClick();
                return;
            }

            lastClickTime = currentTime;
            if (Click != null)
            {
                Click(this, MouseEventArgs.Current);
            }
        }

        internal virtual void OnDoubleClick()
        {
            if (DoubleClick != null)
            {
                DoubleClick(this, MouseEventArgs.Current);
            }
        }

        internal virtual void OnMouseEnter()
        {
            if (MouseEnter != null)
            {
                MouseEnter(this, MouseEventArgs.Current);
            }

        }

        internal virtual void OnMouseExit()
        {
            if (dragging) return;

            if (MouseLeave != null)
            {
                MouseLeave(this, MouseEventArgs.Current);
            }
        }

        public void Update()
        {
            if (dragging)
                HandleDragging();
        }

        void LateUpdate()
        {
            CorrectLayer();
        }

        protected virtual void CorrectLayer()
        {
            var pos = transform.position;
            pos.z = EternityUtil.GetElementLayer(this.gameObject);
            if (dragging)
                pos.z += 1;
            pos.z += layerOffset;
            transform.position = pos;

        }

        protected virtual void HandleDragging()
        {
            var mousePos = Input.mousePosition;
            this.transform.position = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, EternityUtil.GetElementLayer(this.gameObject)) - dragOffset;

        }

        internal virtual void ResetSize()
        {
            var tex = guiTexture.texture;
            guiTexture.pixelInset = new Rect(0, 0, tex.width, tex.height);
        }

        internal virtual void Resize(int newWidth, int newHeight)
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