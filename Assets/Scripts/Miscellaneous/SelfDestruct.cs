using UnityEngine;
public class SelfDestruct : MonoBehaviour
{
    public float time = 10.0f;
    void Start()
    {
        Destroy(this.gameObject, time);
    }
}