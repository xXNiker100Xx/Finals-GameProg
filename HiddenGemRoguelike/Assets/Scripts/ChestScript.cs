using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    Animator animator;
    public GameObject item;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player In Field");
            if (Input.GetKey(KeyCode.E))
            {
                animator.Play("ChessOpen");
            }
        }
    }

    public void dropitem()
    {
        item.SetActive(true);
    }
}
