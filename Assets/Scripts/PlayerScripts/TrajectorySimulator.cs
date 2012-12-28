using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class TrajectorySimulator : MonoBehaviour {

    LineRenderer lineRenderer;
    LinkedList<Vector3> positions;
    ThirdPersonController controller;
    RegularCamera regularCamera;
    InventoryManager inventory;
    bool IsAimCameraOn { get { return regularCamera.CameraMode == CameraMode.Aim; } }
    float ArrowFireVelocity { get { return controller.ArrowFireVelocity; } }
    const int maxIterations = 200;
    GameObject weaponGameObject;
    float gravity = 3.0f;
    public int index;

	// Use this for initialization
	void Start () 
    {
        positions = new LinkedList<Vector3>();
        controller = GetComponent<ThirdPersonController>();
        regularCamera = Camera.main.GetComponent<RegularCamera>();
        lineRenderer = GetComponent<LineRenderer>();
        inventory = GetComponent<InventoryManager>();
        weaponGameObject = inventory.currentWeaponGameobject;
	}

    void FixedUpdate()
    {
        if (IsAimCameraOn)
        {
            index = 0;
            positions = new LinkedList<Vector3>();
            var hit = new RaycastHit();
            var position = weaponGameObject.transform.position;
            var velocity = ArrowFireVelocity * controller.ForwardDirection * .01f;
            var lastPosition = position;
            positions.AddFirst(position);
            for (int g = 0; g < maxIterations; g++)
            {
                velocity.y -= gravity * .0001f;
                position += velocity;

                positions.AddLast(position);

                if (Physics.Raycast(lastPosition, position, out hit, velocity.magnitude+.1f))
                {
                    //Debug.Log("Collided with " + hit.collider.gameObject.name);
                    break;
                }
            }

            var node = positions.First;
            lineRenderer.SetVertexCount(maxIterations);
            while (node.Next != null)
            {
                if (index == maxIterations) break;
                lineRenderer.SetPosition(index++, node.Value);
                node = node.Next;
            }
        }
        else
        {
            lineRenderer.SetVertexCount(0);
        }
    }

    internal void SetWeaponGameObject(GameObject GO)
    {
        Debug.Log("Setting new weapon object");
        weaponGameObject = GO;
    }
}
