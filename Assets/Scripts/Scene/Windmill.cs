using UnityEngine;

public class Windmill : MonoBehaviour
{
    private float rotationTime = 10f; //in seconds
    void Update()
    {
        this.transform.RotateAround(collider.bounds.center, transform.forward, 360f * (Time.deltaTime / rotationTime));
    }
}