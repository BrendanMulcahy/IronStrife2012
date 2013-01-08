using UnityEngine;
public class Windmill : MonoBehaviour
{
    private float rotationTime = 5f; //in seconds
    void Update()
    {
        this.transform.Rotate(Vector3.forward * Time.deltaTime, Space.Self);
    }
}