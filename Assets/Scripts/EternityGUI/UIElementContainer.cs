namespace EternityGUI
{
    using UnityEngine;
    using System.Collections.Generic;

    public abstract class UIElementContainer : MonoBehaviour
    {
        public List<BaseElement> elements = new List<BaseElement>();

        public virtual void AddChild(BaseImage newChild)
        {
            newChild.transform.parent = this.transform;
            newChild.Container = this;
            elements.Add(newChild);
        }

        internal void RemoveChild(BaseElement baseElement)
        {
            elements.Remove(baseElement);
            Destroy(baseElement.gameObject);
        }
    }
}
