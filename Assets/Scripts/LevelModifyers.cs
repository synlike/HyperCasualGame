using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModifyers : MonoBehaviour
{
    [SerializeField]
    private HelicoController helico;

    [SerializeField]
    private float speedMultiplier;

    private void Start()
    {
        helico.ForwardSpeed *= speedMultiplier;
    }
}
