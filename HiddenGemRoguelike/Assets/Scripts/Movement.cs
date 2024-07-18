using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    public Transform cameraTransform;
    public ParticleSystem dashVFX;
    public float dashes = 0;
    public float speed;
    public bool moving = false;
    public bool canMove = true;
    public bool canDash = true;
    public AudioSource audioSource;
    public AudioClip clip;
    public AudioClip DashSFX;
    private bool isPlayingFootsteps = false;
    PlayerAttributes _playerAttributes;
    void Start()
    {
        GameObject mainCamPos = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = mainCamPos.transform;
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.identity;
        dashVFX = GetComponentInChildren<ParticleSystem>();
        _playerAttributes = GetComponent<PlayerAttributes>();
        dashes = _playerAttributes.dashCount;
        
    }

    void Update()
    {
        if (canMove)
        {
            HandleMovementInput();
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        if (!canMove)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    void HandleMovementInput()
    {
        Vector3 movementDirection = Vector3.zero;
        moving = false;
        if (Input.GetKey(KeyCode.W))
        {
            movementDirection += cameraTransform.forward;
            moving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementDirection -= cameraTransform.forward;
            moving = true;  
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection -= cameraTransform.right;
            moving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection += cameraTransform.right;
            moving = true;
        }

        movementDirection.y = 0;

        if (movementDirection.magnitude > 0)
        {

            movementDirection.Normalize();
            rb.velocity = movementDirection * speed;
           
            isWalkingSFX(true);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (canDash)
                {
                    dashes -= 1;

                    audioSource.PlayOneShot(DashSFX);
                    dashVFX.Play();
                    rb.AddForce(movementDirection * speed * 25, ForceMode.Impulse);
                    
                   
                }
                if (dashes == 0)
                {
                    dashes = _playerAttributes.dashCount;
                    StartCoroutine(dashCD());
                    canDash = false;
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            isWalkingSFX(false);
        }
    }
    IEnumerator dashCD()
    {
        yield return new WaitForSeconds(2f);
        dashVFX.Stop();
        canDash = true;
    }

    public void isWalkingSFX(bool play)
    {
        if (play)
        {
            if (!isPlayingFootsteps)
            {
                StartCoroutine(PlayFootstepSound());
            }
        }
        else
        {
            StopCoroutine(PlayFootstepSound());
            isPlayingFootsteps = false;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    IEnumerator PlayFootstepSound()
    {
        float baseFootstepInterval = 0.4f;
        isPlayingFootsteps = true;
        while (isPlayingFootsteps)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(clip);
            }
            float interval = baseFootstepInterval;
            yield return new WaitForSeconds(interval);
        }
    }

    public void disableMovement(bool trigger)
    {
        canMove = trigger;
    }
}
