namespace EternityGUI
{
    using UnityEngine;

    public class MouseEventArgs
    {
        public bool leftButton;
        public bool rightButton;
        public bool middleButton;
        public Vector3 screenPosition;

        public bool handled = false;

        public static MouseEventArgs Current
        {
            get 
            {
                return new MouseEventArgs()
                {
                    leftButton = Input.GetMouseButton(0),
                    rightButton = Input.GetMouseButton(1),
                    middleButton = Input.GetMouseButton(2),
                    screenPosition = Input.mousePosition
                };
            }
        }
    }

    public delegate void MouseEventHandler(BaseElement sender, MouseEventArgs e);

    public class MouseDropEventArgs
    {
        public BaseElement draggedObject;
        public BaseElement dropTarget;
        public Vector3 screenPosition;

        public bool handled = false;

        public MouseDropEventArgs(BaseElement draggedObject, BaseElement dropTarget, Vector3 screenPosition)
        {
            this.draggedObject = draggedObject;
            this.dropTarget = dropTarget;
            this.screenPosition = screenPosition;
        }
    }

    public delegate void MouseDropEventHandler(BaseElement sender, MouseDropEventArgs e);

}
