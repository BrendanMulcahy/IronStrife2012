using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.Linq;

/// <summary>
/// Static utility class containing many useful static methods.
/// </summary>
public static class Util
{
    public const float MaxExperienceRange = 20.0f;
    public static int MyLocalPlayerTeam;
    public static void Spawn(String objectName, Vector3 position = new Vector3())
    {
        if (Network.isServer)
        {
            (Network.Instantiate(Resources.Load(objectName), Vector3.zero, Quaternion.identity, 0) as Transform).MoveToWorldGroundPosition(position);
        }
    }

    private static GameObject MainTerrain
    {
        get
        {
            var mainTerrain = GameObject.Find("ValleyOfTheKnight_Terrain");
            return mainTerrain;
        }
    }

    /// <summary>
    /// Gets a string list of all the names of animations in a list of animation clips.
    /// </summary>
    /// <param name="animation">The animation object to parse</param>
    /// <returns>A list of the names of animations in the object</returns>
    public static ArrayList GetAnimationList(Animation animation)
    {
        // make an Array that can grow
        var tmpList = new ArrayList();

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
        if (Network.isServer)
        {
            go.GetComponent<ServerController>().enabled = false;
            go.GetComponent<ServerController>().Reset();
        }
        else
        {
            go.GetComponent<NetworkController>().Reset();
            go.GetComponent<NetworkController>().enabled = false;

        }

    }

    /// <summary>
    /// Enables a person's controls.
    /// </summary>
    /// <param name="go"></param>
    public static void EnableControls(this GameObject go)
    {
        if (Network.isServer)
            go.GetComponent<ServerController>().enabled = true;
        else
            go.GetComponent<NetworkController>().enabled = true;
    }

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
        var spawner = GameObject.Find("PrefabSpawner");
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
            return hit.point + new Vector3(0, .05f,0);
        }
        else return new Vector3();
    }

    internal static IEnumerator DestroyInSeconds(UnityEngine.Object toDestroy, float p)
    {
        yield return new WaitForSeconds(p);
        if (toDestroy!=null)
         UnityEngine.Object.Destroy(toDestroy);

    }

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

    internal static bool IsMyLocalPlayer(this GameObject go)
    {
        if (Util.MyLocalPlayerObject == go)
        {
            return true;
        }
        return false;
    }

    internal static void SetParentAndCenter(this Transform t, Transform other)
    {
        t.parent = other.transform;
        t.localPosition = new Vector3();
        t.localRotation = Quaternion.identity;

    }

    private static GUISkin skin;
    public static GUISkin ISEGUISkin { get { if (!skin) skin = Resources.Load("ISEGUISkin") as GUISkin; return skin; } }

    public static void SetLayerRecursively(this GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    public static void Destroy(UnityEngine.Object o)
    {
        UnityEngine.Object.Destroy(o);
    }

    public static void Destroy(UnityEngine.Object[] o)
    {
        for (int g = 0; g < o.Length; g++)
        {
            UnityEngine.Object.Destroy(o[g]);
        }
    }

    
    internal static IEnumerator DisableInSeconds(Behaviour comp, float p)
    {
        yield return new WaitForSeconds(p);
        if (comp != null)
            comp.enabled = false;
    }

    public static NetworkViewID GetNetworkViewID(this GameObject go)
    {
        if (!go) Debug.Log("Null");
        var networkViewID = go.GetComponent<NetworkView>();
        if (networkViewID) 
            return networkViewID.viewID;
        else 
            return NetworkViewID.unassigned;
    }

    public static GameObject GetGameObject(this NetworkViewID viewID)
    {
        if (viewID == NetworkViewID.unassigned) return null;

        return NetworkView.Find(viewID).gameObject;
    }

    public static Type[] GetSubclasses<T>()
    {
        var allClasses = Assembly.GetExecutingAssembly().GetExportedTypes().Where(t => t.IsSubclassOf(typeof(T))).ToArray();
        return allClasses;
    }

    public static Type[] GetClassesWithAttribute<T>()
    {
        var allClasses = Assembly.GetExecutingAssembly().GetExportedTypes().Where(t => t.GetCustomAttributes(typeof(T), false).Length > 0).ToArray();
        return allClasses;
    }

    private static string _username;
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
}
