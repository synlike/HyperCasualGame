using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasUI : MonoBehaviour
{
    public TMP_Text coinsText;
    public Image fuelBarImage;
    public Animator fuelIconAnim;

    public GameObject playButton;
    public GameObject powerButton;
    public GameObject fuelBar;

    public void ActiveUI()
    {
        playButton.SetActive(false);
        powerButton.SetActive(true);
        fuelBar.SetActive(true);
    }

}
