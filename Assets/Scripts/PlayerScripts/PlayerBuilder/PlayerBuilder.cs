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
    Dictionary<Type, PlayerComponentAttribute> typeToAttributes = new Dictionary<Type, PlayerComponentAttribute>();

    public void BuildCharacter()
    {
        var allPlayerComponents = Util.GetClassesWithAttribute<PlayerComponentAttribute>();
        foreach (Type t in allPlayerComponents)
        {
            typeToAttributes[t] = t.GetCustomAttributes(typeof(PlayerComponentAttribute), false)[0] as PlayerComponentAttribute;
        }

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


}