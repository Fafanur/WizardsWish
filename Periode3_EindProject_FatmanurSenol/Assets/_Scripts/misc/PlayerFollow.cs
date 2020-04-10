using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform playerTarget;
    public Vector3 offset;
    public float smoothSpeed;
 
    void FixedUpdate()
    {
        Vector3 finalPos = playerTarget.position + offset;
        finalPos = Vector3.Lerp(transform.position, finalPos, Time.fixedDeltaTime * smoothSpeed);
        transform.position = finalPos;
    }
}
