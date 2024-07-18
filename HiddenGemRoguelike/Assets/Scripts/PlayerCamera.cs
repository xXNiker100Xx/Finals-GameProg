using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public float fixedZoomDistance = 5f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 fixedRotation = new Vector3(30f, 0f, 0f);

    private void Start()
    {
        GameObject player = FindAnyObjectByType<PlayerAttributes>().gameObject;
        target = player.transform;
        transform.rotation = Quaternion.Euler(fixedRotation);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset.normalized * fixedZoomDistance;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

            transform.position = smoothedPosition;
            transform.LookAt(target.position);
            transform.rotation = Quaternion.Euler(fixedRotation);
        }
    }

}
