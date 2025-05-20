using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;       // Player의 CameraHolder
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, -10f);
    [SerializeField] private float smoothSpeed = 10f;

    private void Reset()
    {
        target = GetComponentInParent<Transform>();
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        transform.LookAt(target);
    }
}
