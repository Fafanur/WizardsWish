using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 offset;
    public Vector3 offsetRot;
    public float hoverSpeed;
    public float hoverDistance;

    public float moveSpeed;
    public float rotationSpeed;

    public Vector3 autoRotateSpeed;
    void FixedUpdate()
    {
        if (targetObject != null)
        {
            Vector3 targetPos = transform.position + offset.x * transform.right + (offset.y + (Mathf.Sin(Time.time * hoverSpeed) * hoverDistance)) * transform.up + offset.z * transform.forward;
            Vector3 targetRot = transform.rotation.eulerAngles + offsetRot + Time.time * autoRotateSpeed;
            targetObject.transform.position = Vector3.Lerp(targetObject.transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
            targetObject.transform.rotation = Quaternion.Lerp(targetObject.transform.rotation, Quaternion.Euler(targetRot), rotationSpeed * Time.fixedDeltaTime ); 
        }
    }
}
