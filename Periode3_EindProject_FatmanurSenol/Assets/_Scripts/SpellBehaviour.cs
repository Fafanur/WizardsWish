using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehaviour : MonoBehaviour
{
    public float damageAmount;
    public float speed;

    
    public GameObject spellParticle;
    public GameObject destroyParticle;
    public AudioSource audioSource;
    public AudioClip explosionSFX;
    private void Update()
    {
        float moveSpeed = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        audioSource.PlayOneShot(explosionSFX);
        if (other.gameObject.tag != "Player")
        {
            Health healthComp = other.GetComponent<Health>();
            if (healthComp != null)
            {
                healthComp.DoDamage(damageAmount);
                Invoke(nameof(DestroyMe), 0.5f);
            }
        }
        Instantiate(destroyParticle, transform.position, Quaternion.identity, other.transform);
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}
