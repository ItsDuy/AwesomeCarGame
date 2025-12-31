using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public Transform target;
    public float dampTime = 3f;
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            // Only damp X and Z, keep Y fixed
            Vector3 dampedPosition = new Vector3(
                Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref velocity.x, dampTime),
                offset.y + target.position.y,
                Mathf.SmoothDamp(transform.position.z, targetPosition.z, ref velocity.z, dampTime)
            );
            transform.position = dampedPosition;
        }
    }
}
