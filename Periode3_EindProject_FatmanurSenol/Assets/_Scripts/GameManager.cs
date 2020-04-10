using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void StatusHandler();
    public StatusHandler winEvent;
    public SpawnSystem spawnSystem;
    public SpawnData[] waves;

    public int curWave = 0;
    public AudioSource audioSource;
    public AudioClip nextWaveSFX;

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
            audioSource.PlayOneShot(nextWaveSFX, 1f);
        }
        else 
        {
            if (winEvent != null)
            {
                winEvent();
            }
        }
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        spawnSystem.StartWave(waves[curWave]);
    }

    private void OnDestroy()
    {
        spawnSystem.waveEndedEvent -= OnWaveEndedEvent;
    }
}
