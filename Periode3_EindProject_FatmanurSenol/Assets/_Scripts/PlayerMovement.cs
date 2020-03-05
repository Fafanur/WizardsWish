using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    private Rigidbody _rb;
    private Vector3 _movement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        _movement = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
    }
    void FixedUpdate()
    {
        MoveCharacter(_movement);
    }
    public void MoveCharacter(Vector3 direction)
    {
        _rb.MovePosition(transform.position + (direction.normalized * movementSpeed * Time.deltaTime));
    }
}
