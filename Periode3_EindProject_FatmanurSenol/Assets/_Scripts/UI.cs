using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public Health targetHealth;
    // Start is called before the first frame update
    void Start()
    {
        targetHealth.deathEvent += TargetDamaged;
    }

    private void TargetDamaged(float currentHealth)
    {
        Debug.Log("I HAVE BEEN DAMAGED: " + currentHealth) ;
    }

    private void OnDestroy()
    {
        targetHealth.deathEvent -= TargetDamaged;
    }
}
