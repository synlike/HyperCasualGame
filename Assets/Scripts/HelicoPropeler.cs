using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicoPropeler : MonoBehaviour
{
    public float rpm = 120.0f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, 6f * rpm * Time.deltaTime, 0);
    }
}
