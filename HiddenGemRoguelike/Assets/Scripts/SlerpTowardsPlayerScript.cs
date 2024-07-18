using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpTowardsPlayerScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public bool isMoving = false;
    private Transform player;
    private Vector3 startPosition;
    private float startTime;
    private float journeyLength;

    public void StartMoving(Transform playerTransform)
    {
        player = playerTransform;
        startPosition = transform.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, player.position);
        StartCoroutine(StartToMove());
    }

    private void Update()
    {
        if (isMoving)
        {
            SlerpMoveTowardsPlayer();
        }
    }

    private void SlerpMoveTowardsPlayer()
    {
        float distCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distCovered / journeyLength;

        transform.position = Vector3.Slerp(startPosition, player.position, fractionOfJourney);

        if (Vector3.Distance(transform.position, player.position) < 0.1f)
        {
            isMoving = false;
        }
    }

    IEnumerator StartToMove()
    {
        yield return new WaitForSeconds(1f);
        isMoving = true;
    }
}
