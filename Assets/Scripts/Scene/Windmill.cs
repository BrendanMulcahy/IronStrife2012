using UnityEngine;

public class Windmill : MonoBehaviour
{
    private float rotationTime = 10f; //in seconds
    private Vector3 boundsCenter;
    private Vector3 forward;

    void Awake()
    {
        boundsCenter = collider.bounds.center;
        forward = transform.forward;
    }

    void Update()
    {
        this.transform.RotateAround(boundsCenter, forward, 360f * (Time.deltaTime / rotationTime));
    }
}