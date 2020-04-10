using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehaviour : MonoBehaviour
{
    public float damageAmount;
    public float speed;

    
    public GameObject spellParticle;
    public GameObject destroyParticle;
    private void Update()
    {
        float moveSpeed = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            Health healthComp = other.GetComponent<Health>();
            if (healthComp != null)
            {
                healthComp.DoDamage(damageAmount);
                Destroy(gameObject);
            }
        }
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
        spellParticle.SetActive(false);
    }
}
