using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
using System.Collections.Generic;

/// <summary>
/// Script that constructs a player GameObject by adding the appropriate MonoBehaviour components.
/// Attach this to an empty GameObject and it should do the rest of the work
/// </summary>
public class PlayerBuilder : MonoBehaviour
{
    public string[] attributeNames;

    void Start()
    {
        Type[] attributes = new Type[attributeNames.Length];
        for (int g = 0; g < attributeNames.Length; g++)
        {
            attributes[g] = Type.GetType(attributeNames[g]+"Attribute");
            Debug.Log("Attribute " + g + " is " + attributes[g].Name);
        }

        AddComponentScripts(attributes);
    }

    void AddComponentScripts(Type[] attributes)
    {
        LinkedList<Type> scriptsToAdd = new LinkedList<Type>();
        var allMonoBehaviours = Assembly.GetExecutingAssembly().GetExportedTypes().Where(type => type.IsSubclassOf(typeof(MonoBehaviour)));
        foreach (Type monoBehaviourType in allMonoBehaviours)
        {
            foreach (Type attributeType in attributes)
            {
                if (monoBehaviourType.GetCustomAttributes(attributeType, false).Length != 0)
                {
                    scriptsToAdd.AddLast(monoBehaviourType);
                    continue;
                }
            }
        }
        
        Debug.Log("All player scripts :");
        foreach (Type t in scriptsToAdd)
        {
            Debug.Log("\t- " + t.FullName);
        }

        foreach (Type t in scriptsToAdd)
        {
            this.gameObject.AddComponent(t);
        }
    }
}