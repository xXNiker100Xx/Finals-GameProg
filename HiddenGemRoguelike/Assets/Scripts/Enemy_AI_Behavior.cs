using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI_Behavior : MonoBehaviour
{
    public NavMeshAgent agent;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {

            Vector3 player = collision.transform.position;
            agent.SetDestination(player);
            Debug.Log("PlayerInField!");
        }
    }
}
