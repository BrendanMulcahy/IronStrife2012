using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public class MasterNetworkSerializer : MonoBehaviour {

    class SerializerMethod
    {
        public MethodInfo method;
        public MonoBehaviour behaviour;
    }
    private LinkedList<SerializerMethod> serializerMethods = new LinkedList<SerializerMethod>();
	// Use this for initialization
	void Start () {
        var allMonoBehaviours = this.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour b in allMonoBehaviours)
        {
            if (b == this) continue;
            foreach (MethodInfo method in b.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (method.Name == "OnSerializeNetworkView" && method.GetCustomAttributes(typeof(DontAutoSerializeAttribute), false).Length == 0)
                {
                    serializerMethods.AddLast(new SerializerMethod() { method = method, behaviour = b });
                }
            }
        }

        serializerMethods.OrderBy(h => h.behaviour.name).ToArray();
	}

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        object[] parameters = { stream, info };
        foreach (SerializerMethod sm in serializerMethods)
        {
            sm.method.Invoke(sm.behaviour, parameters);
        }
    }
}
