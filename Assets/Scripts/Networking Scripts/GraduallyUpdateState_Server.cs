using UnityEngine;

[PlayerComponent(PlayerScriptType.ServerEnabled)]
public class GraduallyUpdateState_Server : MonoBehaviour
{

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        // Always send transform (depending on reliability of the network view)
        if (stream.isWriting)
        {
            Vector3 pos = transform.position;
            Quaternion rot = transform.rotation;
            stream.Serialize(ref pos);
            stream.Serialize(ref rot);
        }
    }
}