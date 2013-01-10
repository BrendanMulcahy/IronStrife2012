using System.Collections;
using UnityEngine;

public class RegularCamera : MonoBehaviour
{

    // This camera is similar to the one used in Jak & Dexter

    Transform target;
    float distance = 4.0f;
    float height = 3.0f;

    private Vector3 headOffset = Vector3.zero;
    private Vector3 centerOffset = Vector3.zero;
    private ThirdPersonController controller;

    private bool isChatOpen = false;

    float mindistance = 3.0f;
    float maxdistance = 10.0f;

    float xSpeed = 250.0f;
    float ySpeed = 120.0f;

    float yMinLimit = -90f;
    float yMaxLimit = 90f;

    float x = 0.0f;
    float y = 0.0f;

    private CameraMode cameraMode = CameraMode.Regular;
    public CameraMode CameraMode { get { return cameraMode; } set { if (cameraMode != value) { isTransitioning = true; transitionTime = startingTransitionTime; } cameraMode = value; } }
    private float aimXSpeed = 250.0f;
    private float aimYSpeed = 120.0f;
    public Vector3 aimOffset;
    public bool isTransitioning = false;
    private float transitionTime = 0f;
    private const float startingTransitionTime = .005f;
    private GameObject LockedTarget { get { return controller.LockedTarget; } }

    private bool firstPersonModeEnabled = false;

    private void Awake()
    {
        aimOffset = new Vector3(.3f, .5f, -1f);
        DidChangeTarget();
    }

    private void DidChangeTarget()
    {
        if (target)
        {
            CharacterController characterController = target.collider as CharacterController;
            if (characterController)
            {
                centerOffset = characterController.bounds.center - target.position;
                headOffset = centerOffset;
                headOffset.y = characterController.bounds.max.y - target.position.y;
            }

            if (target)
            {
                controller = target.GetComponent<ThirdPersonController>();
            }
        }
    }

    private void Update()
    {

    }

    private void LateUpdate()
    {
        if (target)
        {
            if (firstPersonModeEnabled)
            {
                FirstPersonUpdate();
            }
            else
            {
                switch (cameraMode)
                {
                    case CameraMode.Regular:
                        RegularModeUpdate();
                        break;
                    case CameraMode.Aim:
                        AimModeUpdate();
                        break;
                    case CameraMode.Locked:
                        LockedModeUpdate();
                        break;
                }
                FixCollision();
            }
        }
    }

    public void ToggleFirstPersonCamera()
    {
        this.firstPersonModeEnabled = !this.firstPersonModeEnabled;
        if (firstPersonModeEnabled)
        {
            target = target.transform.Find("MainC/LightWeight_Rigged_:HeadCG");
        }
        else
        {
            target = target.transform.root;
        }
    }

    private void FirstPersonUpdate()
    {
        this.transform.position = target.transform.position;
        this.transform.rotation = target.transform.rotation;
    }

    private void RegularModeUpdate()
    {
        if (Input.GetButton("Fire2"))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y += Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        }

        y = ClampAngle(y, yMinLimit, yMaxLimit);
        var wheeldelta = Input.GetAxis("Mouse ScrollWheel");
        if (!isChatOpen && !DebugGUI.visible)
            distance -= wheeldelta * 4;
        distance = Mathf.Clamp(distance, mindistance, maxdistance);

        var rotation = Quaternion.Euler(-y, x, 0);
        var desiredPosition = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
        desiredPosition.y += height;

        transform.rotation = rotation;
        SmoothlyUpdatePosition(desiredPosition);
    }

    private void AimModeUpdate()
    {

        x += Input.GetAxis("Mouse X") * aimXSpeed * 0.02f; ;
        y += Input.GetAxis("Mouse Y") * aimYSpeed * 0.02f;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        distance = Mathf.Clamp(distance, mindistance, maxdistance);

        var rotation = Quaternion.Euler(-y, x, 0);
        var desiredPosition = target.position; desiredPosition.y += 2;
        desiredPosition += target.forward * aimOffset.z;
        desiredPosition += target.right * aimOffset.x;
        desiredPosition += target.up * aimOffset.y;



        transform.rotation = rotation;
        SmoothlyUpdatePosition(desiredPosition);
    }

    private void LockedModeUpdate()
    {
        Vector3 playerPosition = target.transform.position;
        Vector3 lockedPosition = LockedTarget.transform.position;
        Vector3 directionVector = lockedPosition - playerPosition;

        var desiredPosition = playerPosition - directionVector.normalized * distance + Vector3.up * height;
        directionVector = lockedPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(directionVector);
        SmoothlyUpdatePosition(desiredPosition);

        if (!DebugGUI.visible && !isChatOpen)
        {
            Debug.Log("Debug GUI is not visible.");
            var wheeldelta = Input.GetAxis("Mouse ScrollWheel");
            distance -= wheeldelta * 4;

            distance = Mathf.Clamp(distance, mindistance, maxdistance);
        }
    }

    private void SmoothlyUpdatePosition(Vector3 desiredPosition)
    {

        float mult = .06f;
        if (isTransitioning)
        {
            transitionTime *= 1.05f;
            var difference = desiredPosition - transform.position; 
            if (difference.magnitude <= .1f)
            {
                isTransitioning = false;
            }
            var interpolationTime = difference.magnitude * (mult + transitionTime); 
            transform.position = Vector3.Lerp(transform.position, desiredPosition, interpolationTime);
        }
        else
        {
            float maxMoveDelta = (distance / maxdistance) * 4f;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, maxMoveDelta);

        }
    }

    private void FixCollision()
    {
        var layerMask = 1 << 11;
        RaycastHit hit;
        Vector3 rayTarget = target.transform.position + Vector3.up * 2;
        Vector3 rayDirection = this.transform.position - rayTarget;
        if (Physics.Raycast(rayTarget, rayDirection, out hit, rayDirection.magnitude, layerMask))
        {
            this.transform.position = hit.point - rayDirection * .1f;
        }
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    private void SetTarget(Transform t)
    {
        target = t;
        DidChangeTarget();
    }

    public void SetTransform(Vector3 lookDirection)
    {
        y = lookDirection.normalized.y * 90;
        if (lookDirection.x > 0)
        {
            x = Mathf.Acos(Vector3.Dot(lookDirection.normalized, new Vector3(0, 0, 1))) * 180 / Mathf.PI;
        }
        else
        {
            x = Mathf.Acos(Vector3.Dot(lookDirection.normalized, new Vector3(0, 0, 1))) * 180 / Mathf.PI;
            x += (180 - x) * 2;
        }
    }

    private void ChatOpen()
    {
        isChatOpen = true;
    }

    private void ChatClose()
    {
        isChatOpen = false;
    }
}

    public enum CameraMode
    {
        Regular = 1,
        Aim = 2,
        Locked = 3,
    }
