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

        float lastClickTime = 0;
        float maxDoubleClickInterval = .25f;
        public Vector3 dragOffset;


        private UIElementContainer container;
        /// <summary>
        /// The UIElementContainer that holds this BaseElement. Null if this is a free element.
        /// </summary>
        public UIElementContainer Container { get { return container; } set { container = value; } }

        public bool dragging = false;

        /// <summary>
        /// Creates a new BaseElement with the given texture and position. Must provide a fully qualified resource path as the image name.
        /// </summary>
        /// <param name="imageName">Full Resources folder path and name of file to use as the texture</param>
        /// <param name="position">Screen position to place the element at</param>
        public static BaseElement Create(string imageName, Vector3 position)
        {
            var go = new GameObject(imageName + "BaseElement");
            var gt = go.AddComponent<GUITexture>();
            var tex = Resources.Load(imageName) as Texture2D;
            var baseElement = go.AddComponent<BaseElement>();
            gt.texture = tex;
            gt.pixelInset = new Rect(0, 0, tex.width, tex.height);
            gt.transform.position = position;
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
            if (MouseUp != null)
            {
                MouseUp(this, MouseEventArgs.Current);
            }

            dragging = false;
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
            CorrectLayer();
        }

        private void CorrectLayer()
        {
            var pos = transform.position;
            pos.z = EternityUtil.GetElementLayer(this.gameObject);
            transform.position = pos;
        }

        protected virtual void HandleDragging()
        {
            var mousePos = Input.mousePosition;
            this.transform.position = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, EternityUtil.GetElementLayer(this.gameObject)) - dragOffset;

        }

        internal virtual void Resize(float columnWidth, float rowHeight)
        {
            guiTexture.pixelInset = new Rect(0, 0, columnWidth, rowHeight);
        }

        internal virtual void Resize()
        {
            var tex = guiTexture.texture;
            guiTexture.pixelInset = new Rect(0, 0, tex.width, tex.height);
        }
    }
}