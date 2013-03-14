using UnityEngine;
using System.Collections;
using System;

public class MessageTerminal : MonoBehaviour
{
    private static MessageTerminal _instance;
    public static MessageTerminal Main
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("MessageTerminal").GetComponent<MessageTerminal>();
            }
            return _instance;
        }
    }

    private static GameObject itemDropEffect;

    void Start()
    {
        itemDropEffect = Resources.Load("Items/WorldItems/DropEffect") as GameObject;
    }

    [RPC]
    void SpawnNPC(string type, Vector3 position, NetworkViewID animationID, NetworkViewID transformID)
    {
        Debug.Log("Spawning new NPC of type " + type + " at  ["+position.x + ", "+position.y + " +, "+position.z + "]");
        GameObject newNPC = GameObject.Instantiate(Resources.Load("NPCs/" + type)) as GameObject;
        newNPC.GetComponents<NetworkView>()[0].viewID = transformID;
        newNPC.GetComponents<NetworkView>()[1].viewID = animationID;
    }

    [RPC]
    void SpawnWorldItem(string itemName, NetworkViewID itemID, Vector3 position, NetworkViewID networkViewID)
    {

        var worldItemGO = Instantiate(WorldItem.GetWorldItemPrefab(itemName)) as GameObject;
        var wi = worldItemGO.GetComponent<WorldItem>();
        wi.itemName = itemName;
        wi.item = ItemFactory.GetFromViewID(itemID, itemName);
        wi.transform.position = position;
        (Instantiate(itemDropEffect) as Transform).SetParentAndCenter(worldItemGO.transform);

    }

    [RPC]
    void BroadcastGameState(int newGoodScore, int newEvilScore)
    {
        GameState.goodScore = newGoodScore;
        GameState.evilScore = newEvilScore;
    }

    [RPC]
    void GameStarted()
    {
        Debug.Log("The game has started!");
        PopupMessage.Display("The game has started!", 3.0f);
    }

    [RPC]
    void ClientDisplay(string message, float fadeTime)
    {
        PopupMessage.LocalDisplay(message, fadeTime);
    }

    [RPC]
    void CreateNetworkedSceneObject1(string typeName, NetworkViewID viewId)
    {
        Type t = Type.GetType(typeName);
        var attributes = t.GetCustomAttributes(typeof(DefaultSceneObjectAttribute), true);
        var p = attributes[0] as DefaultSceneObjectAttribute;

        GameObject newGo = GameObject.Find(p.prefabName);
        if (!newGo)
        {
            if (p.prefabName != null)
            {
                newGo = Instantiate(Resources.Load("DefaultSceneObjects/" + p.prefabName)) as GameObject;
                newGo.name = p.gameObjectName;
            }
        }
        newGo.networkView.viewID = viewId;
    }

    [RPC]
    void CreateNetworkedSceneObject2(string typeName, NetworkViewID viewId1, NetworkViewID viewId2)
    {
        Type t = Type.GetType(typeName);
        var attributes = t.GetCustomAttributes(typeof(DefaultSceneObjectAttribute), true);
        var p = attributes[0] as DefaultSceneObjectAttribute;

        GameObject newGo = GameObject.Find(p.prefabName);
        if (!newGo)
        {
            if (p.prefabName != null)
            {
                newGo = Instantiate(Resources.Load("DefaultSceneObjects/" + p.prefabName)) as GameObject;
                newGo.name = p.gameObjectName;
            }
        }
        var viewIds = newGo.GetComponents<NetworkView>();
        viewIds[0].viewID = viewId1;
        viewIds[1].viewID = viewId2;
    }

    [RPC]
    void CreateNetworkedSceneObject3(string typeName, NetworkViewID viewId1, NetworkViewID viewId2, NetworkViewID viewId3)
    {
        Type t = Type.GetType(typeName);
        var attributes = t.GetCustomAttributes(typeof(DefaultSceneObjectAttribute), true);
        var p = attributes[0] as DefaultSceneObjectAttribute;

        GameObject newGo = GameObject.Find(p.prefabName);
        if (!newGo)
        {
            if (p.prefabName != null)
            {
                newGo = Instantiate(Resources.Load("DefaultSceneObjects/" + p.prefabName)) as GameObject;
                newGo.name = p.gameObjectName;
            }
        }
        var viewIds = newGo.GetComponents<NetworkView>();
        viewIds[0].viewID = viewId1;
        viewIds[1].viewID = viewId2;
        viewIds[2].viewID = viewId3;
    }

    [RPC]
    void SetTimeScale(float newScale)
    {
        Time.timeScale = newScale;
    }

}
