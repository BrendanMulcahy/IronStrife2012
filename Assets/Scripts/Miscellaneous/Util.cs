using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Static utility class containing many useful static methods.
/// </summary>
public static class Util
{
    // TODO : Move this to a Globals class
    public const float MaxExperienceRange = 20.0f;
    /// <summary>
    /// The team that the local player is on.
    /// </summary>
    public static int MyLocalPlayerTeam;

    /// <summary>
    /// Gets the main terrain object in the scene.
    /// </summary>
    public static GameObject MainTerrain
    {
        get
        {
            var mainTerrain = GameObject.Find("Terrain");
            return mainTerrain;
        }
    }

    /// <summary>
    /// Gets a string list of all the names of animations in a list of animation clips.
    /// </summary>
    /// <param name="animation">The animation object to parse</param>
    /// <returns>A list of the names of animations in the object</returns>
    public static List<string> GetAnimationList(Animation animation)
    {
        // make an Array that can grow
        var tmpList = new List<string>();

        // enumerate all states
        foreach (AnimationState state in animation)
        {
            // add name to tmpList
            tmpList.Add(state.name);
        }
        return tmpList;
    }

    /// <summary>
    /// Gets the CharacterStats component of a gameObject. Will return null if there is none attached.
    /// If the object is a player, PlayerStats is returned. NPCs return NPCStats. You can cast to the
    /// desired type if you know kind of object you are querying.
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static CharacterStats GetCharacterStats(this GameObject go)
    {
        CharacterStats cs = go.GetComponent<CharacterStats>();
        if (cs == null)
        {
            return null;
        }
        else
        {
            return cs;
        }
    }

    /// <summary>
    /// Gets the PlayerMotor component attached to the gameObject. Null if there is none.
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static PlayerMotor GetPlayerMotor(this GameObject go)
    {
        PlayerMotor cs = go.GetComponent<PlayerMotor>();
        if (cs == null)
        {
            return null;
        }
        else
        {
            return cs;
        }
    }

    /// <summary>
    /// Gets a GameObject's attached InventoryManager component. Null if there is none.
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static Inventory GetInventory(this GameObject go)
    {
        Inventory inv = go.GetComponent<Inventory>();
        if (inv == null)
        {
            return null;
        }
        else
        {
            return inv;
        }
    }

    /// <summary>
    /// Gets a GameObject's attached DamageReceiver component. Null if none is attached.
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static DamageReceiver GetDamageReceiver(this GameObject go)
    {
        DamageReceiver dr = go.GetComponent<DamageReceiver>();
        if (dr == null)
        {
            return null;
        }
        {
            return dr;
        }
    }

    /// <summary>
    /// Equips the given item on the gameObject. You should use the InventoryManager class to equip items
    /// onto players
    /// </summary>
    /// <param name="go"></param>
    /// <param name="item"></param>
    public static void Equip(this GameObject go, EquippableItem item)
    {
        item.Equip(go);
    }

    /// <summary>
    /// Stores and retrieves the client's local game player object.
    /// </summary>
    public static GameObject MyLocalPlayerObject { get; set; }

    /// <summary>
    /// Disables a person's controls based on whether they are a server or client.
    /// </summary>
    /// <param name="go"></param>
    public static void DisableControls(this GameObject go)
    {
            go.GetComponent<NetworkController>().Reset();
            go.GetComponent<NetworkController>().enabled = false;

    }

    /// <summary>
    /// Enables a person's controls.
    /// </summary>
    /// <param name="go"></param>
    public static void EnableControls(this GameObject go)
    {
            go.GetComponent<NetworkController>().enabled = true;
    }

    /// <summary>
    /// Toggles the scrolling of the player's camera distance
    /// </summary>
    public static void TogglePlayerScrollZoom()
    {
        Camera.main.GetComponent<RegularCamera>().ToggleScrollingEnabled();
    }

    /// <summary>
    /// Locates the closest valid spawn location for a given team and a given spawn location.
    /// </summary>
    /// <param name="requestedRespawnLocation"></param>
    /// <param name="teamNumber"></param>
    /// <returns></returns>
    internal static Vector3 FindClosestTeamRespawn(Vector3 requestedRespawnLocation, int teamNumber)
    {
        var spawner = GameObject.Find("GameConnect");
        return spawner.transform.position;
    }

    /// <summary>
    /// Moves the transform to the main terrain's floor at the given world position.
    /// </summary>
    /// <param name="transform">The transform to move</param>
    /// <param name="worldPosition">The position to move to. Y coordinate is ignored, terrain floor is used.</param>
    /// <returns></returns>
    public static Vector3 MoveToWorldGroundPosition(this Transform transform, Vector3 worldPosition)
    {
        Vector3 position = worldPosition;
        position.y = Util.MainTerrain.GetComponent<Terrain>().SampleHeight(worldPosition);
        transform.position = position;
        return position;
    }

    /// <summary>
    /// Gives the ground position under the specified point, including objects and terrain
    /// Returns a value .05f above the ground, to avoid falling through.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Vector3 SampleFloorIncludingObjects(Vector3 position)
    {
        position += Vector3.up * 2;
        RaycastHit hit;
        var layerMask = 1 << 8;
        layerMask += (1 << 9);
        layerMask += (1 << 10);
        layerMask = ~layerMask;
        if (Physics.Raycast(position, Vector3.down, out hit, 500, layerMask))
        {
            return hit.point + new Vector3(0, .05f, 0);
        }
        else
        {
            return SampleFloorIncludingObjects(position + Vector3.up * 25f);

            //Debug.LogError("Sample floor failed at " + position);
            //return new Vector3();
        }
    }

    public static Vector3 SampleNavMesh(Vector3 targetPos)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPos, out hit, 1000f, ~0))
        {
            return hit.position;
        }
        else
        {
            Debug.LogError("No position was found near the NavMesh at " + targetPos);
            return new Vector3();
        }
    }

    internal static IEnumerator DestroyInSeconds(UnityEngine.Object toDestroy, float p)
    {
        yield return new WaitForSeconds(p);
        if (toDestroy != null)
            UnityEngine.Object.Destroy(toDestroy);

    }

    /// <summary>
    /// Fades out a given audio source over time
    /// </summary>
    /// <param name="audio"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    internal static IEnumerator FadeOutSoundInSeconds(AudioSource audio, float p)
    {
        float initialVolume = audio.volume;
        float remainingTime = p;
        float timeInterval = .1f;
        while (remainingTime >= 0)
        {
            yield return new WaitForSeconds(timeInterval);
            remainingTime -= timeInterval;
            audio.volume = initialVolume * (remainingTime / p);
        }
    }

    /// <summary>
    /// Fades in a given audio source over time.
    /// </summary>
    /// <param name="audio"></param>
    /// <param name="songToFade"></param>
    /// <param name="maxVolume"></param>
    /// <param name="fadeTime"></param>
    /// <returns></returns>
    internal static IEnumerator FadeInSoundInSeconds(AudioSource audio, AudioClip songToFade, float maxVolume, float fadeTime)
    {
        audio.clip = songToFade;
        yield return new WaitForSeconds(2.0f);
        float initialVolume = 0;
        float totalTime = 0;
        float timeInterval = .1f;
        while (totalTime < fadeTime)
        {
            yield return new WaitForSeconds(timeInterval);
            totalTime += timeInterval;
            audio.volume = initialVolume + maxVolume * (totalTime / fadeTime);
        }
        yield break;
    }

    /// <summary>
    /// Returns true if this GameObject is the player's GameObject.
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    internal static bool IsMyLocalPlayer(this GameObject go)
    {
        if (Util.MyLocalPlayerObject == go)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets this transform to be the child of the given transform, and centers it at that parent.
    /// </summary>
    /// <param name="t"></param>
    /// <param name="other"></param>
    internal static void SetParentAndCenter(this Transform t, Transform other)
    {
        t.parent = other;
        t.localPosition = new Vector3();
        t.localRotation = Quaternion.identity;

    }

    private static GUISkin skin;
    public static GUISkin ISEGUISkin { get { if (!skin) skin = Resources.Load("ISEGUISkin") as GUISkin; return skin; } }

    /// <summary>
    /// Sets the layer of this GameObject and all of its children.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="layerNumber"></param>
    public static void SetLayerRecursively(this GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    /// <summary>
    /// Destroys all objects in the given Object array after time t
    /// </summary>
    /// <param name="o"></param>
    public static void Destroy(UnityEngine.Object[] o, float time = 0f)
    {
        if (time != 0)
        {
            for (int g = 0; g < o.Length; g++)
            {
                UnityEngine.Object.Destroy(o[g], time);
            }
        }
        else
        {
            for (int g = 0; g < o.Length; g++)
            {
                UnityEngine.Object.Destroy(o[g]);
            }
        }
    }

    /// <summary>
    /// Disables a component after a given time
    /// </summary>
    /// <param name="comp">The component to disable</param>
    /// <param name="delay">The time to wait</param>
    /// <returns></returns>
    internal static IEnumerator DisableInSeconds(Behaviour comp, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (comp != null)
            comp.enabled = false;
    }

    /// <summary>
    /// Gets the NetworkViewID of the first NetworkView attached to this GameObject.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static NetworkViewID GetNetworkViewID(this GameObject gameObject)
    {
        if (!gameObject) Debug.Log("Null");
        var networkViewID = gameObject.GetComponent<NetworkView>();
        if (networkViewID)
            return networkViewID.viewID;
        else
            return NetworkViewID.unassigned;
    }

    /// <summary>
    /// Gets the GameObject that this NetworkViewID is attached to
    /// </summary>
    /// <param name="viewID"></param>
    /// <returns></returns>
    public static GameObject GetGameObject(this NetworkViewID viewID)
    {
        if (viewID == NetworkViewID.unassigned) return null;

        return NetworkView.Find(viewID).gameObject;
    }

    /// <summary>
    /// Gets all of the subclasses of a given type, using reflection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Type[] GetSubclasses<T>()
    {
        var allClasses = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(T))).ToArray();
        return allClasses;
    }

    /// <summary>
    /// Gets all classes with the given attribute, using reflection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Type[] GetClassesWithAttribute<T>()
    {
        var allClasses = Assembly.GetExecutingAssembly().GetExportedTypes().Where(t => t.GetCustomAttributes(typeof(T), false).Length > 0).ToArray();
        return allClasses;
    }

    private static string _username;
    /// <summary>
    /// Gets the local player's username
    /// </summary>
    public static string Username
    {
        get
        {
            if (_username == null)
            {
                _username = PlayerPrefs.GetString("username", "default_username");
#if UNITY_EDITOR
                _username += "_Editor";
#endif
            }
            return _username;
        }
    }

    /// <summary>
    /// Sends an RPC to a specific group over this NetworkView.
    /// </summary>
    /// <param name="networkView"></param>
    /// <param name="methodName"></param>
    /// <param name="group"></param>
    /// <param name="mode"></param>
    /// <param name="parameters"></param>
    public static void RPCToGroup(this NetworkView networkView, string methodName, int group, RPCMode mode, params object[] parameters)
    {
        var previousGroup = networkView.group;
        networkView.group = group;

        networkView.RPC(methodName, mode, parameters);
        networkView.group = previousGroup;
    }

    /// <summary>
    /// Sends an RPC to a specific group over this NetworkView to a specific NetworkPlayer
    /// </summary>
    /// <param name="networkView"></param>
    /// <param name="methodName"></param>
    /// <param name="group"></param>
    /// <param name="player"></param>
    /// <param name="parameters"></param>
    public static void RPCToGroup(this NetworkView networkView, string methodName, int group, NetworkPlayer player, params object[] parameters)
    {
        var previousGroup = networkView.group;
        networkView.group = group;

        networkView.RPC(methodName, player, parameters);
        networkView.group = previousGroup;
    }

    public static void RPCToServer(this NetworkView networkView, MonoBehaviour behaviour, string methodName, params object[] parameters)
    {
        if (Network.isServer)
        {
            var allMethods = behaviour.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (MethodInfo method in allMethods)
            {
                if (method.Name == methodName)
                {
                    method.Invoke(behaviour, parameters);
                    break;
                }
            }

        }
        else
        {
            networkView.RPC(methodName, RPCMode.Server, parameters);
        }
    }

    public static void RPCToServer(this NetworkView networkView, string methodName, params object[] parameters)
    {
        if (Network.isServer)
        {
            foreach (MonoBehaviour behaviour in networkView.gameObject.GetComponents<MonoBehaviour>())
            {

                var allMethods = behaviour.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (MethodInfo method in allMethods)
                {
                    if (method.Name == methodName)
                    {
                        method.Invoke(behaviour, parameters);
                        break;
                    }
                }
            }
        }
        else
        {
            networkView.RPC(methodName, RPCMode.Server, parameters);
        }
    }

    private static Font _OFLGoudyStMTT;
    public static Font OFLGoudyStMTT
    {
        get
        {
            if (_OFLGoudyStMTT == null)
                _OFLGoudyStMTT = Resources.Load("Fonts/OFLGoudyStMTT") as Font;
            return _OFLGoudyStMTT;
        }
    }

    public static IEnumerator TurnOffParticles(ParticleEmitter[] emitters, float delay)
    {
        yield return new WaitForSeconds(delay);
        TurnOffParticles(emitters);
    }

    public static IEnumerator TurnOffParticlesInChildren(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        TurnOffParticles(go.GetComponentsInChildren<ParticleEmitter>());
    }

    private static void TurnOffParticles(ParticleEmitter[] emitters)
    {
        foreach (ParticleEmitter pe in emitters)
        {
            pe.emit = false;
        }
    }

    public static void SerializeRegeneratingStat(this BitStream stream, RegeneratingStat stat)
    {
        int current;
        if (stream.isWriting)
        {
            current = stat.CurrentValue;
            stream.Serialize(ref current);

        }
        else
        {
            current = stat.CurrentValue;
            stream.Serialize(ref current);
            stat.SetCurrentValue(current);
        }
    }

    public static void SerializeBuffableStat(this BitStream stream, BuffableStat stat)
    {
        if (stream.isWriting)
        {
            var baseVal = stat.baseValue;
            stream.Serialize(ref baseVal);

            var mod = stat.ModifiedValue;
            stream.Serialize(ref mod);
        }
        else
        {
            var baseVal = stat.baseValue;
            var previousVal = baseVal;
            stream.Serialize(ref baseVal);
            stat.IncrementBaseValue(baseVal - previousVal);

            var mod = stat.ModifiedValue;
            var previous = mod;
            stream.Serialize(ref mod);
            stat.IncrementModifierValue(mod - previous);
        }
    }

    public static void SerializeMoveSpeed(this BitStream stream, MoveSpeedStat stat)
    {
        if (stream.isWriting)
        {
            var baseVal = stat.baseValue;
            stream.Serialize(ref baseVal);

            var mod = stat.ModifiedValue;
            stream.Serialize(ref mod);
        }
        else
        {
            var baseVal = stat.baseValue;
            var previousVal = baseVal;
            stream.Serialize(ref baseVal);
            stat.IncrementBaseValue(baseVal - previousVal);

            var mod = stat.ModifiedValue;
            var previous = mod;
            stream.Serialize(ref mod);
            stat.IncrementModifierValue(mod - previous);
        }
    }

    public static NetworkPlayer GetNetworkPlayer(this GameObject go)
    {
        if (Network.isClient) return new NetworkPlayer();

        else
        {
            return PlayerManager.Main.FindRecord(go).networkPlayer;
        }
    }

    /// <summary>
    /// Gets the team number of a given game object. Returns -1 if the object isn't associated with a team.
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static int GetTeamNumber(this GameObject go)
    {
        var stats = go.transform.root.gameObject.GetCharacterStats();
        if (stats)
        {
            return stats.TeamNumber;
        }
        else return -1;
    }

    /// <summary>
    /// Generates a random number between 0 and 1 and returns true if it is less than this float.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public static bool TryRandomRoll(this float f)
    {
        return (UnityEngine.Random.value < f);
    }

    /// <summary>
    /// Prints a string to the Console.
    /// </summary>
    /// <param name="s">The string to be printed</param>
    public static void ToConsole(this string s)
    {
        Debug.Log(s);
    }
}
