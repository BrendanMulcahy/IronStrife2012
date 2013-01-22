using System;
using UnityEngine;

/// <summary>
/// Attribute that designates a component to be added to a player character
/// Used by the PlayerBuilder class to dynamically build player characters on the client and server
/// </summary>
[AttributeUsage(AttributeTargets.Class,AllowMultiple=false)]
public class PlayerComponentAttribute : Attribute
{
    public PlayerScriptType[] types;
    public PlayerComponentAttribute(params PlayerScriptType[] types)
    {
        this.types = types;
    }
}

public enum PlayerScriptType
{
    /// <summary>
    /// Present on all
    /// </summary>
    AllEnabled,

    /// <summary>
    /// Present on all, but disabled
    /// </summary>
    AllDisabled,

    /// <summary>
    /// Enabled on the server
    /// </summary>
    ServerEnabled,

    /// <summary>
    /// Disabled on the server
    /// </summary>
    ServerDisabled,

    /// <summary>
    /// Enabled on a client
    /// </summary>
    ClientEnabled,

    /// <summary>
    /// Disabled on a client
    /// </summary>
    ClientDisabled,

    /// <summary>
    /// A component that only resides on the owner of the player character who is also the server
    /// </summary>
    ServerOwnerEnabled,

    /// <summary>
    /// A component that only resides on the owner of the player who is a client
    /// </summary>
    ClientOwnerEnabled,

    /// <summary>
    /// Must be deleted from only the owning client
    /// </summary>
    ClientOwnerDeleted,

    /// <summary>
    /// Disabled from the owning client
    /// </summary>
    ClientOwnerDisabled,
    ServerOwnerDeleted,
}