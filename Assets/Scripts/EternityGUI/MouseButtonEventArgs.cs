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
}
