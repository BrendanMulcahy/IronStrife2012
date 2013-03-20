using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public ControlPoint controlPoint;
    public int controllingTeam;
    public bool isStartingSpawn = false;

    void OnMasterGameLogicAdded()
    {
        if (controlPoint)
        {
            controllingTeam = controlPoint.controllingTeam;
            controlPoint.Captured += controlPoint_Captured;
        }
        PlayerManager.Main.RegisterRespawnPoint(this);
    }

    void controlPoint_Captured(GameObject sender, ControlPointCapturedEventArgs e)
    {
        controllingTeam = e.newTeam;
    }
}