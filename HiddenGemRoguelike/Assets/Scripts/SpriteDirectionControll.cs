using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDirectionControll : MonoBehaviour
{
    public Transform targetCamera;

    private void Awake()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        if (targetCamera != null)
        {          
            Vector3 currentRotation = transform.eulerAngles;

            transform.rotation = Quaternion.Euler(targetCamera.eulerAngles.x, currentRotation.y, currentRotation.z);
        }
    }
}
