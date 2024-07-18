using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{


    public GameObject[] Enemies;

    public void SpawnTrigger(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            float randomX = UnityEngine.Random.Range(-5f, 5f);
            float randomZ = UnityEngine.Random.Range(-5f, 5f);

            
            Vector3 spawnPos = new (randomX, 0, randomZ);
            Vector3 transformPos = transform.position + spawnPos;
            GameObject enemy = Instantiate(Enemies[0], transformPos, transform.rotation);
            
        }
    }
}
