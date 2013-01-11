namespace EternityGUI
{
    using UnityEngine;
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class EternityGUITestAttribute : Attribute { }

    public class EternityGUITestLoader : MonoBehaviour
    {
        void Start()
        {
            var allTestClasses = Util.GetClassesWithAttribute<EternityGUITestAttribute>();
            foreach (Type t in allTestClasses)
            {
                new GameObject(t.Name + "Test").AddComponent(t);
            }
        }
    }
}