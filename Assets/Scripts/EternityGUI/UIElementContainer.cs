namespace EternityGUI
{
    using UnityEngine;
    using System.Collections.Generic;

    public abstract class UIElementContainer : MonoBehaviour
    {
        public List<BaseElement> elements = new List<BaseElement>();

        public virtual void AddChild(BaseElement newChild)
        {
            newChild.transform.parent = this.transform;
            elements.Add(newChild);
        }
    }
}
