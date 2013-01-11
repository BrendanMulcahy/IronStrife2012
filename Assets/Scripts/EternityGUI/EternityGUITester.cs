namespace EternityGUI
{
    using UnityEngine;

    [EternityGUITest]
    public class GridPanelTester : MonoBehaviour
    {
        GridPanel panel;
        void Start()
        {
            panel = GridPanel.Create(new Vector3(.2f, .2f), new Vector2(300, 300), 3, 3);
            for (int g = 0; g < 9; g++)
            {
                panel.AddChild(BaseElement.Create("GUI/Button", new Vector3()));
                panel.elements[g].Click += EternityGUITester_Click;
            }
        }

        void EternityGUITester_Click(BaseElement sender, MouseEventArgs e)
        {
            PopupMessage.LocalDisplay("You pressed element " + panel.elements.IndexOf(sender), .1f);
        }
    }
}