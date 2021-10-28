using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera cam;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothSpeed = 0.1f;

    [SerializeField]
    private Vector3 offset;

    public bool lerpFOV, unlerpFOV;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if(lerpFOV)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 63, 0.1f);
        }

        if(unlerpFOV)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60, 0.1f);
        }
    }

    void LateUpdate()
    {
        Vector3 goToPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, goToPos, smoothSpeed);

        transform.position = smoothPos;
    }
}
