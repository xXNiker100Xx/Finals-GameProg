using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteRotation : MonoBehaviour
{
    public Camera targetCamera; 
    public SpriteRenderer spriteRenderer;


    public Movement _movement;
    public ChacterControlManagement _controlManagement;
    Animator _anim;
    


    public Sprite frontSprite;
    public Sprite backSprite;
    public Sprite frontLeftSprite;
    public Sprite frontRightSprite;
    public Sprite backLeftSprite;
    public Sprite backRightSprite;

    public bool Front = false;
    public bool Back = false;
    public bool FrontLeft = false;
    public bool FrontRight = false;
    public bool BackLeft = false;
    public bool BackRight = false;
   


    public bool isActive = true;

    void Start()
    {

        _anim = GetComponent<Animator>();

        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (isActive)
        {
            UpdateSpriteBasedOnMouse();
        }        
    }

    void UpdateSpriteBasedOnMouse()
    {
        
        Vector3 mouseScreenPosition = Input.mousePosition;

       
        Ray ray = targetCamera.ScreenPointToRay(mouseScreenPosition);

       
        Plane plane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 worldMousePosition = ray.GetPoint(distance);

            Vector3 direction = worldMousePosition - transform.position;

            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            
            Front = Back = FrontLeft = FrontRight = BackLeft = BackRight = false;

            if (_controlManagement.canAttack)
            {
                onRotation(true);
            }
           
            if (angle >= 22.5f && angle < 67.5f)
            {
                FrontRight = true;
                if (FrontRight)
                {
                    SetFrontRightAngle(true);
                }else if (!FrontRight)
                {
                    SetFrontRightAngle(false);
                }
                
            }
            else if (angle >= 67.5f && angle < 112.5f)
            {
                Front = true;
                if (Front)
                {
                    SetFrontAngle(true);
                }
                else if (!Front)
                {
                    SetFrontAngle(false);
                }

            }
            else if (angle >= 112.5f && angle < 157.5f)
            {
                FrontLeft = true;
                if (FrontLeft)
                {
                    SetFrontLeftAngle(true);
                }
                else if (!FrontLeft)
                {
                    SetFrontLeftAngle(false);
                }
            }
            else if (angle >= -157.5f && angle < -112.5f)
            {
                BackLeft = true;
                if (BackLeft)
                {
                    SetBackLeftAngle(true);
                }
                else if (!BackLeft)
                {
                    SetBackLeftAngle(false);
                }
            }
            else if (angle >= -112.5f && angle < -67.5f)
            {
                Back = true;
                if (Back)
                {
                    SetBackAngle(true);
                }
                else if (!Back)
                {
                    SetBackAngle(false);
                }
            }
            else if (angle >= -67.5f && angle < -22.5f)
            {
                BackRight = true;
                if (BackRight)
                {
                    SetBackRightAngle(true);
                }
                else if (!BackRight)
                {
                    SetBackRightAngle(false);

                }
            }
        }
    }

    //Anim Manager
    private void SetFrontRightAngle(bool active)
    {
        if(active)
        {
            if (_controlManagement.canAttack && !_movement.moving)
            {
                _anim.Play("idleRF");
            }
            else if (_movement.moving && _controlManagement.canAttack)
            {
                _anim.Play("runFR");
            }
            else
            {
                _anim.Play("AtkRF");
               
            }
        }
    }

    private void SetFrontAngle(bool active)
    {
        if (active)
        {
            if (_controlManagement.canAttack && !_movement.moving)
            {
                _anim.Play("IdleF");
            }
            else if (_movement.moving && _controlManagement.canAttack)
            {
                _anim.Play("runF");
            }
            else
            {
                _anim.Play("AtkF");
                onRotation(false);
                
            }
        }
    }

    private void SetFrontLeftAngle(bool active)
    {
        if (active)
        {
            if (_controlManagement.canAttack && !_movement.moving)
            {
                _anim.Play("idleLF");

            }
            else if (_movement.moving && _controlManagement.canAttack)
            {
                _anim.Play("runFL");
            }
            else
            {
                _anim.Play("AtkLF");
                onRotation(false);
                
            }
        }
    }

    private void SetBackRightAngle(bool active)
    {
        if (active)
        {
            if (_controlManagement.canAttack && !_movement.moving)
            {
                _anim.Play("idleBR");

            }
            else if (_movement.moving && _controlManagement.canAttack)
            {
                _anim.Play("runBR");
            }
            else
            {
                _anim.Play("AtkRB");
                onRotation(false);
                
            }
        }
        
    }

    private void SetBackAngle(bool active)
    {
        if (active)
        {
            if (_controlManagement.canAttack && !_movement.moving)
            {
                _anim.Play("idleB");

            }
            else if (_movement.moving && _controlManagement.canAttack)
            {
                _anim.Play("runB");
            }
            else
            {
                _anim.Play("AtkB");
                onRotation(false);
                
            }
        } 
    }

    private void SetBackLeftAngle(bool active)
    {
        if (active)
        {
            if (_controlManagement.canAttack && !_movement.moving)
            {
                _anim.Play("idleBL");

            }
            else if (_movement.moving && _controlManagement.canAttack)
            {
                _anim.Play("runBL");
            }
            else
            {
                _anim.Play("AtkLB");
                onRotation(false);
            }
        }
    }

    public void onRotation(bool isRotate)
    {
        isActive = isRotate;
    }
}
