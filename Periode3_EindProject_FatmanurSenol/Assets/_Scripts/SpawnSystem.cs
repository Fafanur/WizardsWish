using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public delegate void WaveHandler(SpawnData spawnData);
    public event WaveHandler waveEndedEvent;
    public event WaveHandler waveStartedEvent;
    public event WaveHandler waveProgessEvent;
    public GameObject enemyPrefab;
    public GameObject[] spawnPoints;

    private int spawnAmount;
    private int deathAmount;

    private Coroutine spawnCoroutine;
    private bool ongoingWave;

    private SpawnData curSpawnData;


    void SpawnSingleEnemy()
    {
        int spawnPointNumber = UnityEngine.Random.Range(0, spawnPoints.Length);
        Vector3 spawnPos = spawnPoints[spawnPointNumber].transform.position;

        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemyInstance.GetComponentInChildren<Health>().deathEvent += OnEnemyDeath;
    }

    private void OnEnemyDeath(Health healthComp)
    {
        healthComp.deathEvent -= OnEnemyDeath;
        deathAmount++;
        if (waveProgessEvent != null)
        {
            waveProgessEvent(curSpawnData);
        }
        if (deathAmount == spawnAmount)
        {
            EndWave();
        }
    }

    IEnumerator SpawnWave(SpawnData spawnData)
    {
        for(int i = 0; i < spawnData.enemyAmount; i++)
        {
            SpawnSingleEnemy();
            yield return new WaitForSeconds(spawnData.spawnDelay);
        }
        spawnCoroutine = null;
    }

    public void StartWave(SpawnData spawnData)
    {
        EndWave();
        ongoingWave = true;
        curSpawnData = spawnData;
        spawnAmount = spawnData.enemyAmount;
        deathAmount = 0;
        spawnCoroutine = StartCoroutine(SpawnWave(spawnData));
        if (waveStartedEvent != null)
        {
            waveStartedEvent(curSpawnData);
        }
    }

    public void EndWave()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        if (ongoingWave)
        {
            ongoingWave = false;
            if (waveEndedEvent != null) 
            {
                waveEndedEvent(curSpawnData);
            }
        }
    }
}
