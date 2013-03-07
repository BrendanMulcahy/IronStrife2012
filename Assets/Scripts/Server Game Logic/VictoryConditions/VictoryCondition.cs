using System;
using UnityEngine;

public abstract class VictoryCondition : MonoBehaviour
{
    Action updateFunction = () => { };

    protected virtual void OnMasterGameLogicAdded()
    {
        updateFunction = ServerUpdate;
    }
    protected virtual void OnConnectedToServer()
    {
        updateFunction = ClientUpdate;
    }

    void Update()
    {
        updateFunction();
    }

    protected abstract void ClientUpdate();
    protected abstract void ServerUpdate();
    protected abstract void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo msg);
}