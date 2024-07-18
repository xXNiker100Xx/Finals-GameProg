using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    public float hitForce = 100f;
    public Transform HitBox;
    public GameObject boxTrigger;
    public LayerMask groundLayer;
    private new AudioSource audio;
    public bool onTrigger;
    public bool canTrigger = true;
    public bool isActive = true;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (isActive)
        {
            FollowMouse();
        }

        if (canTrigger)
        {
            attackTrigger();
        }
    }

    public void FollowMouse()
    {
    
        Vector3 mouseScreenPosition = Input.mousePosition;

        
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
          
            Vector3 mouseWorldPosition = hit.point;
        
            Vector3 direction = mouseWorldPosition - HitBox.position;
            direction.y = 0;

            HitBox.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void attackTrigger()
    {
        if (onTrigger)
        {
            boxTrigger.SetActive(true);
        }
        else if (!onTrigger)
        {
            boxTrigger.SetActive(false);
        }
    }

    public void hitTrigger(bool hit)
    {
        if (hit)
        {
            StartCoroutine(startHit());
        }
    }

    public IEnumerator startHit()
    {
        audio.Play();
        onTrigger = true;

        HitBox.GetComponentInParent<Rigidbody>().AddForce(HitBox.forward * hitForce);

        yield return new WaitForSeconds(0.1f);
        onTrigger = false;
    }

    public void onHitBox(bool _Active)
    {
        isActive = _Active;
    }
}
