using UnityEngine;
namespace EternityGUI
{
    public class Tooltip : Panel
    {
        void CreateBackground(string backgroundName)
        {
            var baseElement = BaseImage.Create(backgroundName, new Vector3());
            baseElement.gameObject.layer = 12;
            baseElement.layerOffset = -1;
            background = baseElement.gameObject;
            background.transform.parent = transform;
            background.transform.position = new Vector3();

            //baseElement.MouseDown += baseElement_MouseDown;
            //baseElement.MouseUp += baseElement_MouseUp;
        }
    }
}