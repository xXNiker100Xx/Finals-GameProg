using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHitBox : MonoBehaviour
{
    public EnemyAttributes _enemyAttributes;
    public Transform player; 
    public Transform pivot;
    public GameObject hitBox;
    public NavMeshAgent agent;

    public float rotationSpeed = 5f;
    public float coolDown = 5f;
    public bool isTrgger = true;
    public bool readyAttack = true;

    private void Start()
    {

        player = FindFirstObjectByType<PlayerAttributes>().transform;

        _enemyAttributes = GetComponentInParent<EnemyAttributes>();

        coolDown = _enemyAttributes.coolDown1;
    }

    void Update()
    {
        if (isTrgger)
        {
            Vector3 direction = player.position - pivot.position;
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            pivot.rotation = Quaternion.Slerp(pivot.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }    
    }

    public void EnemeyAttack(bool _Active)
    {
        isTrgger = _Active;
    }

    IEnumerator isAttacking()
    {
        agent.speed = 0f;
        hitBox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitBox.SetActive(false);
        agent.speed = 2f;
    }

    IEnumerator cooldDown()
    {
        isTrgger = false;
        yield return new WaitForSeconds(coolDown - 1f);
        isTrgger = true;
        yield return new WaitForSeconds(coolDown);
        readyAttack = true;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (readyAttack)
            {
                StartCoroutine(isAttacking());
                StartCoroutine(cooldDown());
                readyAttack = false;


            }
        }
    }



}
