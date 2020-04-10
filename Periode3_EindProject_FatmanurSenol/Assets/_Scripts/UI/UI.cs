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

    public GameObject deathScreen;
    public GameObject winScreen;

    public AudioSource audioSource;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    private Vector3 healthX;
    void Start()
    {
        targetHealth.deathEvent += Death;
        targetHealth.damagedEvent += TargetDamage;
        targetHealth.healingEvent += TargetHealed;

        gameManager.winEvent += Win;
        spawnSystem.waveStartedEvent += WaveStarted;
        
        healthX = healthBar.transform.localScale;
    }
    private void WaveStarted(SpawnData spawnData)
    {
        waveIndicator.text = (gameManager.curWave + 1).ToString() + "/" + (gameManager.waves.Length).ToString();
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
        audioSource.PlayOneShot(loseSFX);
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    private void Win()
    {
        audioSource.PlayOneShot(winSFX);
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        targetHealth.deathEvent -= Death;
        targetHealth.damagedEvent -= TargetDamage;
        targetHealth.healingEvent -= TargetHealed;
        spawnSystem.waveStartedEvent -= WaveStarted;
        gameManager.winEvent -= Win;
        damageIndicator.DOKill();
        Time.timeScale = 1f;

    }
}
