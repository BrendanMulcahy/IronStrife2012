using System;
using UnityEngine;
using System.Collections.Generic;

public static class NetworkObjectManager
{
    private static Dictionary<NetworkViewID, GameObject> networkObjects = new Dictionary<NetworkViewID, GameObject>();
    private static int Count { get { return networkObjects.Count; } }

    public static void Add(NetworkViewID newId, GameObject newObject)
    {
        networkObjects.Add(newId, newObject);
    }

    public static GameObject Get(NetworkViewID id)
    {
        return networkObjects[id];
    }

    public static void Remove(NetworkViewID idToRemove)
    {
        networkObjects.Remove(idToRemove);
    }

    public static void Clear()
    {
        networkObjects.Clear();
    }
}
