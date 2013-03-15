using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
using System.Collections.Generic;

/// <summary>
/// Script that constructs a player GameObject by adding the appropriate MonoBehaviour components.
/// Uses reflection to determine the appropriate components to add to the GameObject, based on whether
/// this is a client or a server view of the character. Also handles adding specific components for the owner
/// of the player character, such as input managers and GUI components.
/// </summary>
public static class PlayerBuilder
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

    private static GameObject clientCharacterPrefab;
    private static GameObject serverCharacterPrefab;

    [StaticAutoLoad]
    public static void Initialize()
    {
        _typeToAttributes = new Dictionary<Type, PlayerComponentAttribute>();
        var allPlayerComponents = Util.GetClassesWithAttribute<PlayerComponentAttribute>();
        foreach (Type t in allPlayerComponents)
        {
            _typeToAttributes[t] = t.GetCustomAttributes(typeof(PlayerComponentAttribute), false)[0] as PlayerComponentAttribute;
        }
        initialized = true;
    }

    /// <summary>
    /// Attaches the appropriate components to the player gameobject, depending on whether or not they are a server or client.
    /// Uses reflection to check the PlayerComponent attribute and add appropriate components.
    /// </summary>
    private static void BuildCharacter(string type, GameObject gameObject)
    {
        AddCommonComponents(gameObject);

        if (type == "server")
        {
            BuildServerCharacter(gameObject);
        }
        else if (type == "client")
        {
            BuildClientCharacter(gameObject);
        }
    }

    private static void AddCommonComponents(GameObject gameObject)
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
    internal static void SetOwnership(GameObject gameObject)
    {
        Util.MyLocalPlayerObject = gameObject;
        Util.MyLocalPlayerTeam = gameObject.GetCharacterStats().TeamNumber;

        if (Network.isServer)
            AddServerOwnerComponents(gameObject);
        else
            AddClientOwnerComponents(gameObject);

        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            child.SendMessage("OnSetOwnership", SendMessageOptions.DontRequireReceiver);
        }
        Camera.main.SendMessage("InitialSetTarget", gameObject.transform);

    }

    /// <summary>
    /// Adds server-specific components
    /// </summary>
    /// <param name="gameObject"></param>
    private static void BuildServerCharacter(GameObject gameObject)
    {
        foreach (Type t in typeToAttributes.Keys)
        {
            if (typeToAttributes[t].types.Contains(PlayerScriptType.ServerDisabled))
            {
                Behaviour g;
                if ((g = gameObject.GetComponent(t) as Behaviour) != null)
                    g.enabled = false;
                else
                    ((MonoBehaviour)gameObject.AddComponent(t)).enabled = false;
            }
            if (typeToAttributes[t].types.Contains(PlayerScriptType.ServerEnabled))
            {
                Behaviour g;
                if ((g = gameObject.GetComponent(t) as Behaviour) != null)
                    g.enabled = true;
                else
                    ((MonoBehaviour)gameObject.AddComponent(t)).enabled = true;
            }
        }
    }

    /// <summary>
    /// Adds client specific components
    /// </summary>
    /// <param name="gameObject"></param>
    private static void BuildClientCharacter(GameObject gameObject)
    {
        foreach (Type t in typeToAttributes.Keys)
        {
            if (typeToAttributes[t].types.Contains(PlayerScriptType.ClientDisabled))
            {
                Behaviour g;
                if ((g = gameObject.GetComponent(t) as Behaviour) != null)
                    g.enabled = false;
                else
                    ((MonoBehaviour)gameObject.AddComponent(t)).enabled = false;
            }
            if (typeToAttributes[t].types.Contains(PlayerScriptType.ClientEnabled))
            {
                Behaviour g;
                if ((g = gameObject.GetComponent(t) as Behaviour) != null)
                    g.enabled = true;
                else
                    ((MonoBehaviour)gameObject.AddComponent(t)).enabled = true;
            }
        }
    }

    /// <summary>
    /// Adds server-owner specific components
    /// </summary>
    /// <param name="gameObject"></param>
    private static void AddServerOwnerComponents(GameObject gameObject)
    {
        foreach (Type t in typeToAttributes.Keys)
        {
            if (typeToAttributes[t].types.Contains(PlayerScriptType.ServerOwnerEnabled))
            {
                Behaviour g;
                if ((g = gameObject.GetComponent(t) as Behaviour) != null)
                    g.enabled = true;
                else
                    (g = (MonoBehaviour)gameObject.AddComponent(t)).enabled = true;

            }

            if (typeToAttributes[t].types.Contains(PlayerScriptType.ServerOwnerDeleted))
            {
                MonoBehaviour c = gameObject.GetComponent(t) as MonoBehaviour;
                if (c)
                    UnityEngine.Object.Destroy(c);

            }
        }
    }

    /// <summary>
    /// Adds client-owner specific components
    /// </summary>
    /// <param name="gameObject"></param>
    private static void AddClientOwnerComponents(GameObject gameObject)
    {
        foreach (Type t in typeToAttributes.Keys)
        {
            if (typeToAttributes[t].types.Contains(PlayerScriptType.ClientOwnerEnabled))
            {
                MonoBehaviour c = gameObject.GetComponent(t) as MonoBehaviour;
                if (c)
                    c.enabled = true;
                else
                    ((MonoBehaviour)gameObject.AddComponent(t)).enabled = true;
            }

            if (typeToAttributes[t].types.Contains(PlayerScriptType.ClientOwnerDeleted))
            {
                MonoBehaviour c = gameObject.GetComponent(t) as MonoBehaviour;
                if (c)
                    UnityEngine.Object.Destroy(c);

            }

            if (typeToAttributes[t].types.Contains(PlayerScriptType.ClientOwnerDisabled))
            {
                MonoBehaviour c = gameObject.GetComponent(t) as MonoBehaviour;
                if (c)
                    c.enabled = false;

            }
        }
    }

    /// <summary>
    /// Generates a new Client character, assigns it NetworkViewIDs.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="interpolationViewID"></param>
    /// <param name="animationViewID"></param>
    /// <returns></returns>
    internal static GameObject GenerateClient(string username, NetworkViewID interpolationViewID, NetworkViewID animationViewID, PlayerRecord record)
    {
        var gameObject = GameObject.Instantiate(Resources.Load("Player/PlayerPrefabMelee01")) as GameObject;
        if (record != null) record.gameObject = gameObject;
        //NetworkView interpolationView = gameObject.GetComponents<NetworkView>()[0];
        //interpolationView.viewID = interpolationViewID;
        //interpolationView.observed = gameObject.AddComponent<GraduallyUpdateState>();

        //NetworkView animationView = gameObject.GetComponents<NetworkView>()[1];
        //animationView.viewID = animationViewID;
        //animationView.observed = gameObject.AddComponent<NetworkSyncAnimation>();

        NetworkView masterView = gameObject.GetComponents<NetworkView>()[0];
        masterView.viewID = interpolationViewID;
        masterView.observed = gameObject.AddComponent<MasterNetworkSerializer>();

        gameObject.name = username;
        BuildCharacter("client", gameObject);

        Debug.Log("Setting networkViewIDs to  " + username + ". transform ID = " + interpolationViewID + " , animation ID = " + animationViewID);

        return gameObject;
    }

    internal static GameObject GenerateServer(string username, NetworkViewID interpolationViewID, NetworkViewID animationViewID, PlayerRecord record)
    {

        var gameObject = GameObject.Instantiate(Resources.Load("Player/PlayerPrefabMelee01")) as GameObject;
        record.gameObject = gameObject;
        //NetworkView interpolationView = gameObject.GetComponents<NetworkView>()[0];
        //interpolationView.viewID = interpolationViewID;
        //interpolationView.observed = gameObject.AddComponent<ServerUpdateState>();

        //NetworkView animationView = gameObject.GetComponents<NetworkView>()[1];
        //animationView.viewID = animationViewID;
        //animationView.observed = gameObject.AddComponent<NetworkSyncAnimation>();

        NetworkView masterView = gameObject.GetComponents<NetworkView>()[0];
        masterView.viewID = interpolationViewID;
        masterView.observed = gameObject.AddComponent<MasterNetworkSerializer>();

        gameObject.name = username;
        BuildCharacter("server", gameObject);

        return gameObject;
    }
}