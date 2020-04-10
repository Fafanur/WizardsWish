using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float healAmount;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Health healthComp = other.GetComponent<Health>();
            if (healthComp != null && healthComp.curHealth < healthComp.maxHealth)
            {
                healthComp.DoHeal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
