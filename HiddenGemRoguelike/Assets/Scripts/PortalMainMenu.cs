using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMainMenu : MonoBehaviour
{
   
    public GameObject portalEntrance;
    public GameObject entrance;
 

    private void Start()
    {
        
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
     
        }
    }

    IEnumerator startPortal()
    {
        Debug.Log("startPortal");
        yield return new WaitForSeconds(1f);
        entrance.SetActive(true);
     
    } 
}

