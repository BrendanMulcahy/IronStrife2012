using System;
using UnityEngine;
using System.Reflection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DefaultSceneObjectAttribute : Attribute
{
    /// <summary>
    /// The name for the GameObject to create
    /// </summary>
    public string gameObjectName;

    /// <summary>
    /// The name of the prefab if there is one associated.
    /// </summary>
    public string prefabName;

    public DefaultSceneObjectAttribute(string gameObjectName, string prefabName = null)
    {
        this.gameObjectName = gameObjectName;
        this.prefabName = prefabName;
    }
}