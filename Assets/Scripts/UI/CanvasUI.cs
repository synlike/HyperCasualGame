using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasUI : MonoBehaviour
{
    public GameObject canvasUIPanel;

    public TMP_Text coinsText;
    public Image fuelBarImage;
    public Animator fuelIconAnim;

    public GameObject playButton;
    public GameObject powerButton;
    public GameObject fuelBar;

    public Image powerupImage;

    public Sprite boostSprite;
    public Sprite shieldSprite;

    public void ActiveUI()
    {
        canvasUIPanel.SetActive(true);
        playButton.SetActive(false);
        powerButton.SetActive(true);
        fuelBar.SetActive(true);
    }

    public void ResetUI()
    {
        canvasUIPanel.SetActive(true);
        playButton.SetActive(true);
        powerButton.SetActive(false);
        fuelBar.SetActive(false);
    }

    public void HidePanel()
    {
        canvasUIPanel.SetActive(false);
    }

    public void DisplayBoost()
    {
        powerupImage.enabled = true;
        powerupImage.sprite = boostSprite;
    }
    public void DisplayShield()
    {
        powerupImage.enabled = true;
        powerupImage.sprite = shieldSprite;
    }
    public void DisplayNone()
    {
        powerupImage.enabled = false;
    }

}
