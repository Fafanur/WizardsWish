using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    public float raycastdis;

    private Rigidbody _rb;
    private Vector3 _movement;
    public bool isGrounded;

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
        Jump();
        
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rb.velocity = Vector3.up * jumpForce;
            }
        }
    }
    public void MoveCharacter(Vector3 direction)
    {
        _rb.MovePosition(transform.position + (direction.normalized * movementSpeed * Time.deltaTime));
    }

    public void Jump()
    {
        Vector3 dwn = -transform.up;
        Debug.DrawRay(transform.position, dwn * raycastdis, Color.white);
        if (Physics.Raycast(transform.position, dwn, raycastdis))
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }
    }
}
