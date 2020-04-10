using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    public float raycastdis;
    public float fireRate;
    //testing spells
    public GameObject spell;
    public Transform spellSpawnLocation;
    public GameObject healingEffect;

    public AudioSource sfxSource;
    public AudioClip healingSFX;
    public AudioClip[] attackSFXs;

    private Rigidbody _rb;
    private Vector3 _movement;
    private Health myHealth;
    private float counter;

    private void Awake()
    {
        myHealth = GetComponent<Health>();
        _rb = GetComponent<Rigidbody>();

        myHealth.healingEvent += OnHealing;

    }
    void Update()
    {
        counter += Time.deltaTime;
        _movement = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        if (Physics.Raycast(transform.position, -transform.up, raycastdis))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rb.velocity = Vector3.up * jumpForce;
            }
        }

        if(Cursor.lockState == CursorLockMode.Locked)
        {
            if (Input.GetMouseButton(0) && counter > fireRate)
            {
                counter = 0f;
                CastSpell();
            }
        }
        
    }
    void FixedUpdate()
    {
        MoveCharacter(_movement);
    }

    private void OnHealing(Health healthComp)
    {
        sfxSource.PlayOneShot(healingSFX, 0.7f);
        CancelInvoke(nameof(EndHealing));
        healingEffect.SetActive(true);
        Invoke(nameof(EndHealing), 3f);
    }

    private void EndHealing()
    {
        healingEffect.SetActive(false);
    }
    public void MoveCharacter(Vector3 direction)
    {
        _rb.MovePosition(transform.position + (direction.normalized * movementSpeed * Time.deltaTime));
    }

    public void CastSpell()
    {
        sfxSource.PlayOneShot(attackSFXs[Random.Range(0, attackSFXs.Length)]);
        Vector3 spawnPos = spellSpawnLocation.transform.position;
        Vector3 rot = Camera.main.transform.rotation.eulerAngles;
        Instantiate(spell, spawnPos, Quaternion.Euler(rot));
    }
}
