using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothSpeed = 0.1f;

    [SerializeField]
    private Vector3 offset;


    void LateUpdate()
    {
        Vector3 goToPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, goToPos, smoothSpeed);

        transform.position = smoothPos;


    }
}
