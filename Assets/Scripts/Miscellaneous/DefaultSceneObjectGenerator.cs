using UnityEngine;
using System.Reflection;
using System;

public class DefaultSceneObjectGenerator : MonoBehaviour
{
    void Start()
    {
        var allTypes = Assembly.GetExecutingAssembly().GetTypes();
        foreach (Type t in allTypes)
        {
            if (t.IsSubclassOf(typeof(MonoBehaviour)))
            {
                var attributes = t.GetCustomAttributes(typeof(DefaultSceneObjectAttribute), true);
                if (attributes.Length != 0)
                {
                    CreateDefaultSceneObject(attributes[0] as DefaultSceneObjectAttribute, t);
                }
            }
        }
    }

    private void CreateDefaultSceneObject(DefaultSceneObjectAttribute p, Type t)
    {
        if (!GameObject.Find(p.gameObjectName))
        {
            if (p.prefabName != null)
            {
                var go = Instantiate(Resources.Load("DefaultSceneObjects/"+p.prefabName) as GameObject);
                go.name = p.gameObjectName;
            }
            else
            {
                var newGO = new GameObject(p.gameObjectName);
                newGO.AddComponent(t);
            }
        }
    }
}