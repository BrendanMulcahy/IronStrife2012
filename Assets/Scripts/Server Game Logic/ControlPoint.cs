using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    void OnMasterGameLogicAdded()
    {
        MasterGameLogic.Main.AddControlPoint(this);
    }
}