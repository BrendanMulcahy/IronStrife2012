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
    private static bool initialized = false;
    private static Dictionary<Type, PlayerComponentAttribute> _typeToAttributes = new Dictionary<Type, PlayerComponentAttribute>();
    private static Dictionary<Type, PlayerComponentAttribute> typeToAttributes
    {
        get
        {
            if (!initialized) Initialize();
            return _typeToAttributes;
        }
    }

    [StaticAutoLoad]
    private static void Initialize()
    {
        _typeToAttributes = new Dictionary<Type, PlayerComponentAttribute>();
        var allPlayerComponents = Util.GetClassesWithAttribute<PlayerComponentAttribute>();
        foreach (Type t in allPlayerComponents)
        {
            _typeToAttributes[t] = t.GetCustomAttributes(typeof(PlayerComponentAttribute), false)[0] as PlayerComponentAttribute;
        }
        initialized = true;
    }

    void OnNetworkInstantiate(NetworkMessageInfo info)
    {
        if (Network.isClient)
            BuildCharacter();
    }

    /// <summary>
    /// Attaches the appropriate components to the player gameobject, depending on whether or not they are a server or client.
    /// </summary>
    public void BuildCharacter()
    {
        AddCommonComponents();

        if (Network.isServer)
        {
            BuildServerCharacter();
        }
        else if (Network.isClient)
        {
            BuildClientCharacter();
        }
    }

    private void AddCommonComponents()
    {
        foreach (Type t in typeToAttributes.Keys)
        {
            if (typeToAttributes[t].types.Contains(PlayerScriptType.AllEnabled))
            {
                ((MonoBehaviour)gameObject.AddComponent(t)).enabled = true;
            }

            if (typeToAttributes[t].types.Contains(PlayerScriptType.AllDisabled))
            {
                ((MonoBehaviour)gameObject.AddComponent(t)).enabled = false;
            }
        }
    }

    /// <summary>
    /// Notifies the player that the gameobject this is called on is their player.
    /// Calls the OnSetOwnership method in all attached components.
    /// Also attaches the camera to this gameObject.
    /// </summary>
    [RPC]
    internal void SetOwnership()
    {
        Util.MyLocalPlayerObject = this.gameObject;

        if (Network.isServer)
            AddServerOwnerComponents();
        else
            AddClientOwnerComponents();

        gameObject.SendMessage("OnSetOwnership", SendMessageOptions.DontRequireReceiver);
        Camera.main.SendMessage("SetTarget", transform);

    }

    private void BuildServerCharacter()
    {
        foreach (Type t in typeToAttributes.Keys)
        {
            if (typeToAttributes[t].types.Contains(PlayerScriptType.ServerDisabled))
            {
                ((MonoBehaviour)gameObject.AddComponent(t)).enabled = false;
            }
            if (typeToAttributes[t].types.Contains(PlayerScriptType.ServerEnabled))
            {
                ((MonoBehaviour)gameObject.AddComponent(t)).enabled = true;
            }
        }
    }

    private void BuildClientCharacter()
    {
        foreach (Type t in typeToAttributes.Keys)
        {
            if (typeToAttributes[t].types.Contains(PlayerScriptType.ClientDisabled))
            {
                ((MonoBehaviour)gameObject.AddComponent(t)).enabled = false;
            }
            if (typeToAttributes[t].types.Contains(PlayerScriptType.ClientEnabled))
            {
                ((MonoBehaviour)gameObject.AddComponent(t)).enabled = true;
            }
        }
    }

    private void AddServerOwnerComponents()
    {
        foreach (Type t in typeToAttributes.Keys)
        {
            if (typeToAttributes[t].types.Contains(PlayerScriptType.ServerOwnerEnabled))
            {
                MonoBehaviour c = GetComponent(t) as MonoBehaviour;
                if (c)
                    c.enabled = true;
                else
                    ((MonoBehaviour)gameObject.AddComponent(t)).enabled = true;
            }
        }
    }

    private void AddClientOwnerComponents()
    {
        foreach (Type t in typeToAttributes.Keys)
        {
            if (typeToAttributes[t].types.Contains(PlayerScriptType.ClientOwnerEnabled))
            {
                MonoBehaviour c = GetComponent(t) as MonoBehaviour;
                if (c)
                    c.enabled = true;
                else
                    ((MonoBehaviour)gameObject.AddComponent(t)).enabled = true;
            }

            if (typeToAttributes[t].types.Contains(PlayerScriptType.ClientOwnerDeleted))
            {
                MonoBehaviour c = GetComponent(t) as MonoBehaviour;
                if (c)
                    Destroy(c);

            }
        }
    }

    public void GetAndAssignNewNetworkView(Component target, NetworkStateSynchronization stateSync = NetworkStateSynchronization.Off)
    {
        if (Network.isClient) throw new UnauthorizedAccessException("Clients cannot assign new network views.");

        else
        {
            var newNetView = gameObject.AddComponent<NetworkView>();
            newNetView.observed = target;
            newNetView.stateSynchronization = stateSync;
            newNetView.viewID = Network.AllocateViewID();

            networkView.RPC("AssignNewNetworkView", RPCMode.OthersBuffered, newNetView.viewID, (int)stateSync, target.GetType().Name);
        }
    }

    /// <summary>
    /// Assigns a new NetworkView to observe the given target.
    /// </summary>
    /// <param name="viewID">The NetworkViewID to use</param>
    /// <param name="stateSync">The method of state sync to use</param>
    /// <param name="targetTypeName">The name of the target type to observe. There must be only one attached</param>
    [RPC]
    void AssignNewNetworkView(NetworkViewID viewID, int stateSync, string targetTypeName)
    {
        var newNetView = this.gameObject.AddComponent<NetworkView>();
        if (this.gameObject.GetComponents(Type.GetType(targetTypeName)).Length > 1)
        {
            Debug.LogError("There should only be one " + targetTypeName + " attached to " + gameObject + "!");
            return;
        }
        newNetView.observed = this.gameObject.GetComponent(targetTypeName);
        newNetView.stateSynchronization = (NetworkStateSynchronization)stateSync;
        newNetView.viewID = viewID;

    }


}