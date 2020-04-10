using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnData 
{
    public int enemyAmount;
    [HideInInspector]
    public int waveAmount;
    public float spawnDelay;
}
