using UnityEngine;
using System.Collections.Generic;

public class RelicDropArea : MonoBehaviour
{
    /// <summary>
    /// The control point that this dorp area is associated with. 
    /// Only the team that controls the Control Point may leave relics in this drop area.
    /// </summary>
    public ControlPoint controlPoint;

    public List<Relic> relicsInArea = new List<Relic>();

    void Start()
    {
        if (!collider)
        {
            AddDefaultCollider();
        }
        this.gameObject.layer = 21;

        controlPoint.Captured += controlPoint_Captured;
    }

    void controlPoint_Captured(GameObject sender, ControlPointCapturedEventArgs e)
    {
        foreach (Relic r in relicsInArea)
        {
            r.ControllingTeam = e.newTeam;
        }
    }

    private void AddDefaultCollider()
    {
        var boxCollider = this.gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector3(10, 5, 10);
    }

    void OnTriggerEnter(Collider other)
    {
        var relic = other.GetComponent<Relic>();
        if (relic)
        {
            relic.OnEnteredDropArea(this);
            relicsInArea.Add(relic);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var relic = other.GetComponent<Relic>();
        if (relic && relicsInArea.Contains(relic))
        {
            relic.OnExitedDropArea();
            relicsInArea.Remove(relic);
        }
    }

}