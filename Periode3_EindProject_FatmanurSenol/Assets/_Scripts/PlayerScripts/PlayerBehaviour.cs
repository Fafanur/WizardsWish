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

    private Rigidbody _rb;
    private Vector3 _movement;
    private float nextFire;
    private Health myHealth;

    private void Awake()
    {
        myHealth = GetComponent<Health>();
        _rb = GetComponent<Rigidbody>();

        myHealth.healingEvent += OnHealing;

    }
    void Update()
    {
        _movement = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        if (Physics.Raycast(transform.position, -transform.up, raycastdis))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rb.velocity = Vector3.up * jumpForce;
            }
        }

        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            CastSpell();
        }
    }
    void FixedUpdate()
    {
        MoveCharacter(_movement);
    }

    private void OnHealing(Health healthComp)
    {
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
        Vector3 spawnPos = spellSpawnLocation.transform.position;
        Vector3 rot = Camera.main.transform.rotation.eulerAngles;
        Instantiate(spell, spawnPos, Quaternion.Euler(rot));
    }
}
