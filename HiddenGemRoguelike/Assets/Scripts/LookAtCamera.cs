using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform objectTransform;
    public float rotation = -90;

    public bool rotationFrozen = false;

    private void Start()
    {
        objectTransform = GetComponent<Transform>();

        Camera camera = FindAnyObjectByType<Camera>();
        if (camera != null)
        {
            cameraTransform = camera.transform;
        }
    }

    void FixedUpdate()
    {
        if (!rotationFrozen)
        {
            lookAtCamera();
        }
    }

    void lookAtCamera()
    {
        Vector3 relativePos = cameraTransform.position - objectTransform.position;
        objectTransform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        objectTransform.Rotate(Vector3.up, rotation);
    }

    // Method to freeze the rotation externally (if needed)
    public void FreezeRotation()
    {
        rotationFrozen = true;
    }
}