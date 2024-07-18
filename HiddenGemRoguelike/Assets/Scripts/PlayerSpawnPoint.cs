using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    [SerializeField]GameObject player;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Awake()
    {
        StartCoroutine(changeSpawn());
    }

    public void spawnPoint()
    {
        player.transform.position = transform.position;
    }

    IEnumerator changeSpawn()
    {
        yield return new WaitForSeconds(0.5f);
        spawnPoint();
    }
}
