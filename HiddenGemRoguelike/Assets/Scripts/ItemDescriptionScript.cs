using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescriptionScript : MonoBehaviour
{
    public GameObject itemName;
    public GameObject itemDescription;
    private void OnTriggerStay(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            itemDescription.SetActive(true);
            itemName.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            itemDescription.SetActive(false);
            itemName.SetActive(true);
        }
    }
}
