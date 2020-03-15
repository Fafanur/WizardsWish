using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float trapDamage;
    public void OnTriggerEnter(Collider other)
    {
        Health healthComp = other.GetComponent<Health>();
        if (healthComp != null)
        {
            healthComp.DoDamage(trapDamage);
        }
    }
}
