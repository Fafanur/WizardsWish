using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Health targetHealth;
    public GameObject healthBar;
    public Image damageIndicator;
    public Text waveIndicator;
    public SpawnSystem spawnSystem;

    public GameManager gameManager;
    private Vector3 healthX;
    void Start()
    {
        targetHealth.deathEvent += Death;
        targetHealth.damagedEvent += TargetDamage;
        targetHealth.healingEvent += TargetHealed;

        spawnSystem.waveStartedEvent += WaveStarted;
        spawnSystem.waveProgessEvent += WaveProgression;
        
        healthX = healthBar.transform.localScale;
    }
    private void WaveStarted(SpawnData spawnData)
    {
        waveIndicator.text = (gameManager.curWave + 1).ToString() + "/" + (gameManager.waves.Length).ToString();
    }

    private void WaveProgression(SpawnData spawnData)
    {
        
    }

    private void UpdateHealthBar()
    {
        healthX = new Vector3((targetHealth.curHealth / targetHealth.maxHealth), 1f, 1f);
        healthBar.transform.localScale = healthX;
    }

    private void TargetDamage(Health healthComp)
    {
        UpdateHealthBar();
        damageIndicator.DOFade(0.4f, 0.1f);
        damageIndicator.DOFade(0f, 0.1f).SetDelay(0.1f);
        
    }

    private void TargetHealed(Health healthComp)
    {
        UpdateHealthBar();
    }

    private void Death(Health healthComp)
    {
        
        Debug.Log("I HAVE BEEN DAMAGED: " + healthComp.curHealth) ;
    }

    private void OnDestroy()
    {
        targetHealth.deathEvent -= Death;
        targetHealth.damagedEvent -= TargetDamage;
        targetHealth.healingEvent -= TargetHealed;

    }
}
