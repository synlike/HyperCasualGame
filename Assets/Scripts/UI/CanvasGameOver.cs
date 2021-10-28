using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameOver : MonoBehaviour
{
    public GameObject panelWin;
    public GameObject panelLose;


    public void ResetAll()
    {
        panelWin.SetActive(false);
        panelLose.SetActive(false);
    }
}
