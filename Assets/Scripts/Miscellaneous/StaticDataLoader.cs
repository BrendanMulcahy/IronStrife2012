using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;

/// <summary>
/// This class automatically calls static methods marked with the [StaticAutoLoad] attribute.
/// </summary>
[DefaultSceneObject("StaticDataLoader")]
public class StaticDataLoader : MonoBehaviour
{
    public List<string> methodsCalled = new List<string>();

    void Start()
    {
        var allClasses = Assembly.GetExecutingAssembly().GetTypes();
        foreach (Type t in allClasses)
        {
            FindAndCallStaticLoadingMethods(t);
            
        }
    }

    private void FindAndCallStaticLoadingMethods(Type t)
    {
        var allMethods = t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (MethodInfo method in allMethods)
        {
            if (method.GetCustomAttributes(typeof(StaticAutoLoad), false).Length!=0 && !methodsCalled.Contains(method.Name))
            {
                Debug.Log("Invoking static loader " + t.Name + "." + method.Name);
                method.Invoke(null, null);
                methodsCalled.Add(method.Name);
            }
        }
    }
}

/// <summary>
/// Marks a method that should be called on scene load
/// Classes implementing this should st
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class StaticAutoLoad : Attribute { }