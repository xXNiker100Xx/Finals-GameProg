using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public int MaxEnemyCount = 0;
    public int Maxwave = 0;

    [Header("Map")]
    public int mapChoices;

    [Header("UI Components")]
    public TextMeshProUGUI waveText;

    private int enemyCount;
    private int wave = -1;
    private bool isWaveActive = false;
    private int enemiesSpawned;

    public GameObject[] mapType;

    private EnemySpawnerScript[] enemySpawners;
    [SerializeField] private GameObject[] mobSpawners;
    private EnemyAttributes[] Enemies;
    [SerializeField] private PortalScript[] portals;
    private GameObject[] portalEnterance;
    private int currentMapIndex = -1;

    void Start()
    {
        FindEnemySpawnPoint();
        ChangeMap(mapChoices);
        StartWave();
    }

    void FindPortalsInMap(GameObject map)
    {
        portals = map.GetComponentsInChildren<PortalScript>();
        portalEnterance = new GameObject[portals.Length];
        for (int i = 0; i < portals.Length; i++)
        {
            portalEnterance[i] = portals[i].gameObject;
        }
    }

    void FindEnemySpawnPoint()
    {
        enemySpawners = FindObjectsOfType<EnemySpawnerScript>();
        mobSpawners = new GameObject[enemySpawners.Length];

        for (int i = 0; i < enemySpawners.Length; i++)
        {
            mobSpawners[i] = enemySpawners[i].gameObject;
        }
    }

    void ActivatePortals(bool state)
    {
        foreach (var portal in portals)
        {
            portal.portalGate(state);
        }
    }

    void StartWave()
    {
        wave++;
        enemiesSpawned = 0;
        waveText.text = "Wave " + wave;

        StartCoroutine(SpawnEnemies());
        isWaveActive = true;
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < MaxEnemyCount)
        {
            foreach (EnemySpawnerScript spawner in enemySpawners)
            {
                spawner.SpawnTrigger(1);
                enemiesSpawned++;
                Debug.Log("Enemies spawned: " + enemiesSpawned);
                yield return new WaitForSeconds(1.0f);
            }
        }
    }

    void Update()
    {
        FindEnemySpawnPoint();
        Enemies = FindObjectsOfType<EnemyAttributes>();
        enemyCount = Enemies.Length;

        if (isWaveActive && enemyCount <= 0 && enemiesSpawned >= MaxEnemyCount)
        {
            isWaveActive = false;
            MaxEnemyCount += 1;
            if (wave < Maxwave)
            {
                StartWave();
            }
            else
            {
                ActivatePortals(true);
                waveText.text = "Wave Complete!";
            }
        }

        if (currentMapIndex != mapChoices)
        {
            ChangeMap(mapChoices);
        }
    }

    public void ChangeMap(int newMapIndex)
    {
       
        if (currentMapIndex >= 0 && currentMapIndex < mapType.Length)
        {
            mapType[currentMapIndex].SetActive(false);
            ActivatePortals(false);
        }
      
        currentMapIndex = newMapIndex;
        mapType[currentMapIndex].SetActive(true);
        
        FindPortalsInMap(mapType[currentMapIndex]);

        wave = 0;
        Maxwave += 1;
        MaxEnemyCount += 1;
        StartWave();
    }
}
