using UnityEngine;

public class DistanceFromLocation : TransitionRequirement
{
    private Vector3 location;
    private Transform transform;
    float maxDistance;

    public DistanceFromLocation(Vector3 location, Transform transform, float distance)
    {
        this.location = location;
        this.transform = transform;
        this.maxDistance = distance;
    }

    public override bool IsSatisfied()
    {
        return (Vector3.Distance(location, transform.position) > maxDistance);
    }
}

public class ProximityToLocation : TransitionRequirement
{
    private Vector3 location;
    private Transform transform;
    float maxDistance;

    public ProximityToLocation(Vector3 location, Transform transform, float distance)
    {
        this.location = location;
        this.transform = transform;
        this.maxDistance = distance;
    }

    public override bool IsSatisfied()
    {
        return (Vector3.Distance(location, transform.position) < maxDistance);
    }
}