﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void StatusHandler();
    public StatusHandler winEvent;
    public SpawnSystem spawnSystem;
    public SpawnData[] waves;

    public int curWave = 0;

    private void Awake()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].waveAmount = i;
        }
        spawnSystem.waveEndedEvent += OnWaveEndedEvent;
    }

    private void OnWaveEndedEvent(SpawnData spawnData)
    {
        curWave++;
        if (curWave < waves.Length)
        {
            spawnSystem.StartWave(waves[curWave]);
        }
        else 
        {
            if (winEvent != null)
            {
                winEvent();
            }
        }
    }

    void Start()
    {
        spawnSystem.StartWave(waves[curWave]);
    }

    private void OnDestroy()
    {
        spawnSystem.waveEndedEvent -= OnWaveEndedEvent;
    }
}
