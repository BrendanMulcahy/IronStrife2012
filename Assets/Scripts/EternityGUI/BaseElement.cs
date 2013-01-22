using UnityEngine;
namespace EternityGUI
{
    public abstract class BaseElement : MonoBehaviour
    {
        public event MouseEventHandler Click;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseEnter;
        public event MouseEventHandler MouseLeave;
        public event MouseEventHandler DoubleClick;
        //public event MouseEventHandler MouseWheelChanged;
        public event MouseDropEventHandler Dropped;

        public bool draggable = false;
        float lastClickTime = 0;
        float maxDoubleClickInterval = .25f;
        public Vector3 dragOffset;

        private UIElementContainer container;
        /// <summary>
        /// The UIElementContainer that holds this BaseElement. Null if this is a free element.
        /// </summary>
        public UIElementContainer Container { get { return container; } set { container = value; } }

        public bool dragging = false;
        public int layerOffset = 0;
        private float dragDistance = 0;
        private const float maxDragForClick = 25;
        private Vector3 initialDragPosition = new Vector3();

        /// <summary>
        /// Creates a new BaseElement with the given texture and position. Must provide a fully qualified resource path as the image name.
        /// </summary>
        /// <param name="imageName">Full Resources folder path and name of file to use as the texture</param>
        /// <param name="position">Screen position to place the element at</param>
        public static BaseImage Create(string imageName, Vector3 position)
        {
            var go = new GameObject(imageName + "BaseElement");
            go.layer = 12;
            var gt = go.AddComponent<GUITexture>();
            var tex = Resources.Load(imageName) as Texture2D;
            var baseElement = go.AddComponent<BaseImage>();
            gt.texture = tex;
            gt.pixelInset = new Rect(0, 0, tex.width, tex.height);
            gt.transform.position = position.ScreenToViewport();
            gt.transform.localScale = new Vector3();

            return baseElement;
        }

        public static BaseImage Create(Texture2D tex, Vector3 position)
        {
            var go = new GameObject(tex.name + "BaseElement");
            var gt = go.AddComponent<GUITexture>();
            var baseElement = go.AddComponent<BaseImage>();
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

            if (draggable)
            {
                dragOffset = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0) - gameObject.transform.position;
                initialDragPosition = Input.mousePosition;
                dragging = true;
            }
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
            initialDragPosition = new Vector3();
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
            if (Click != null && !(dragDistance > maxDragForClick))
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
            //Guard statement for un-draggable elements
            if (!draggable) return;

            var mousePos = Input.mousePosition;
            this.transform.position = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, EternityUtil.GetElementLayer(this.gameObject)) - dragOffset;

            dragDistance = Vector3.Magnitude(mousePos - initialDragPosition);
        }

        internal abstract void ResetSize();

        internal abstract void Resize(int newWidth, int newHeight);
    }
}