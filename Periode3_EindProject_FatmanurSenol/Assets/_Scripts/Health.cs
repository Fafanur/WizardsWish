using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void HealthHandler(float currentHealth);

    public event HealthHandler damagedEvent;
    public event HealthHandler healingEvent;
    public event HealthHandler deathEvent;

    public float maxHealth;
    public float curHealth { get; private set;}

    private void Awake()
    {
        curHealth = maxHealth;
    }
    
    
    public void DoDamage(float damage)
    {
        if (curHealth == 0)
        {
            return;
        }
        curHealth = Mathf.Clamp(curHealth - damage, 0f, maxHealth);
        if (damagedEvent != null)
        {
            damagedEvent(curHealth);
        }
        if (curHealth == 0f)
        {
            Death();
        }
    }

    public void DoHeal(float healing)
    {
        if (curHealth == 0 || curHealth == maxHealth)
        {
            return;
        }
        curHealth = Mathf.Clamp(curHealth + healing, 0f, maxHealth);
        if (healingEvent != null)
        {
            healingEvent(curHealth);
        }
    }

    private void Death()
    {
        if (deathEvent != null)
        {
            deathEvent(curHealth);
        }
        
    }
}
