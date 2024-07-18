using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PortalScript : MonoBehaviour
{
    private int interactionCount = 0;
    public GameObject portalEntrance;
    public GameObject entrance;
    public GameManager manager;
    SphereCollider _sphereCollider;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.enabled = false;
        entrance.SetActive(false);
    }

  
    public void portalGate(bool _active)
    {
        portalEntrance.SetActive(_active);

        if (_active)
        {
            StartCoroutine(startPortal());
        }
        else
        {
            StopCoroutine(startPortal());
            entrance.SetActive(false);
            _sphereCollider.enabled = false;
        }
    }

    IEnumerator startPortal()
    {
        Debug.Log("startPortal");
        yield return new WaitForSeconds(1f);
        entrance.SetActive(true);
        _sphereCollider.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player In Field");
            if (Input.GetKey(KeyCode.E))
            {
                interactionCount++;
                manager.ChangeMap(interactionCount);
                manager.mapChoices++;
            }
        }
    }
}
