using UnityEngine;

public class PlayerObjectInteractor : MonoBehaviour
{
    const float interactionRayDistance = 25f;
    const float maxInteractionDistance = 5f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactionRayDistance))
            {
                var io = hit.collider.gameObject.GetComponent<InteractableObject>();
                if (io && Vector3.Distance(this.transform.position, collider.transform.position) < maxInteractionDistance)
                {
                    io.InteractWith(this.gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            int mask = ~LayerMask.NameToLayer("Terrain");
            var collisions = Physics.OverlapSphere(transform.position + transform.forward * 2f, 1.5f, mask);
            foreach (Collider collider in collisions)
            {
                var io = collider.gameObject.GetComponent<InteractableObject>();
                if (io && Vector3.Distance(this.transform.position, collider.transform.position) < maxInteractionDistance)
                {
                    io.InteractWith(this.gameObject);
                    break;
                }
            }
        }
    }
}
