using UnityEngine;
using System.Collections.Generic;

public class RelicDropArea : MonoBehaviour
{
    /// <summary>
    /// The control point that this dorp area is associated with. 
    /// Only the team that controls the Control Point may leave relics in this drop area.
    /// </summary>
    public ControlPoint controlPoint;

    private int particlesPerRelic = 50;
    private float sizePerRelic = .15f;
    private new ParticleSystem particleSystem;

    public List<Relic> relicsInArea = new List<Relic>();

    void Start()
    {
        if (!collider)
        {
            AddDefaultCollider();
        }
        if (!GetComponentInChildren<ParticleSystem>())
        {
            AddDefaultParticleSystem();
        }
        this.gameObject.layer = 21;

        controlPoint.Captured += controlPoint_Captured;
        particleSystem = this.gameObject.GetComponentInChildren<ParticleSystem>();
    }

    private void AddDefaultParticleSystem()
    {
        var prefab = Resources.Load("RelicDropEffect");
        var go = Instantiate(prefab) as GameObject;
        go.transform.SetParentAndCenter(this.transform);
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
            AddRelicToList(relic);
        }
    }

    /// <summary>
    /// Adds a relic to the list of relics within this area, and changes the particle system's effects.
    /// </summary>
    /// <param name="relic"></param>
    private void AddRelicToList(Relic relic)
    {
        relicsInArea.Add(relic);
        var startSpeed = particleSystem.startSpeed;
        for (int g = 1; g <= 4; g++)
        {
            particleSystem.startSpeed += startSpeed * g;
            particleSystem.Emit(particlesPerRelic*4);
            particleSystem.startSpeed -= startSpeed * g;
        }
        particleSystem.startSize += sizePerRelic;
        particleSystem.emissionRate += particlesPerRelic;
        particleSystem.Play();
    }

    void OnTriggerExit(Collider other)
    {
        var relic = other.GetComponent<Relic>();
        if (relic && relicsInArea.Contains(relic))
        {
            relic.OnExitedDropArea();
            RemoveRelicFromList(relic);
        }
    }

    private void RemoveRelicFromList(Relic relic)
    {
        relicsInArea.Remove(relic);
        particleSystem.startSize -= sizePerRelic;
        particleSystem.emissionRate -= particlesPerRelic;


    }

}