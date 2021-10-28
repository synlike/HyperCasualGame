using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum Powerups { None, Boost, Shield};

    private Powerups currentPowerUp = Powerups.None;
    public Powerups CurrentPowerUp { get { return currentPowerUp; } }

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private bool isPlaying = false;
    public bool IsPlaying { get { return isPlaying; } }


    private int currentLevel = 1;
    public int CurrentLevel { get { return currentLevel; } }

    private int playerCoins = 0;
    public int PlayerCoins { get { return playerCoins; } set { playerCoins = value; } }

    private int coinsPickedCurrentLevel = 0;

    private float currentFuel = 100;
    public float CurrentFuel { get { return currentFuel; } set { currentFuel = value; } }

    private int currentScene = 1;

    private int maxLevel = 5;

    [SerializeField] 
    private CanvasUI canvasUI;
    public CanvasUI CanvasUI { get { return canvasUI; } }

    [SerializeField]
    private CanvasGameOver canvasGameOver;
    [SerializeField]
    private CanvasPowerUp canvasPowerUp;

    [SerializeField]
    private float fuelDecreaseRate = 0.5f;
    [SerializeField]
    private int shieldCost = 3;
    [SerializeField]
    private int boostCost = 2;


    public delegate void OnPowerupDelegate();
    public static OnPowerupDelegate powerupDelegate;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    void Start()
    {
        Application.targetFrameRate = 30;
        Init();
    }

    public void Init()
    {
        SimpleCollectibleScript.coinPickedDelegate -= AddCoin;
        SimpleCollectibleScript.fuelPickedDelegate -= AddFuel;
        SimpleCollectibleScript.coinPickedDelegate += AddCoin;
        SimpleCollectibleScript.fuelPickedDelegate += AddFuel;

        UpdateCoins();
        UpdateFuelBar();
        UpdatePowerUpBar();

        Debug.Log("CURRENT LVL = " + currentLevel);

        if (currentLevel != 1)
        {
            canvasUI.HidePanel();
            canvasPowerUp.panelPowerUp.SetActive(true);
        }
    }

    public void ResetAll()
    {
        playerCoins = 0;
        currentLevel = 1;
        currentScene = 1;
        Init();
    }

    public void Play()
    {
        currentFuel = 100;
        coinsPickedCurrentLevel = 0;

        canvasUI.ActiveUI();
        canvasUI.fuelIconAnim.SetFloat("CurrentFuel", currentFuel);
        isPlaying = true;
        StartCoroutine(FuelDecreaseCoroutine());
    }

    public void Retry()
    {
        canvasGameOver.ResetAll();
        canvasUI.ResetUI();
        ResetCoinsLevel();
        SceneManager.LoadScene(currentScene);
        Init();
    }

    public void Quit()
    {
        ResetCoinsLevel();
        Application.Quit();
    }

    public void NextLevel()
    {
        canvasGameOver.ResetAll();
        canvasUI.ResetUI();
        currentScene++;
        currentLevel++;

        if (currentLevel <= maxLevel)
        {
            SceneManager.LoadScene(currentScene);
            Init();
        }
        else
        {
            // STOP THE GAME
            SceneManager.LoadScene("Ending");
        }
    }

    public void Win()
    {
        currentPowerUp = Powerups.None;
        canvasUI.HidePanel();
        UpdatePowerUpBar();
        StopAllCoroutines();
        isPlaying = false;
        canvasGameOver.panelWin.SetActive(true);
    }

    public void Lose()
    {
        currentPowerUp = Powerups.None;
        canvasUI.HidePanel();
        UpdatePowerUpBar();
        StopAllCoroutines();
        isPlaying = false;
        canvasGameOver.panelLose.SetActive(true);
    }

    void Update()
    {
        if(isPlaying && currentFuel == 0)
        {
            Lose();
        }
    }

    void AddCoin()
    {
        playerCoins++;
        coinsPickedCurrentLevel++;
        UpdateCoins();
    }

    void ResetCoinsLevel()
    {
        playerCoins -= coinsPickedCurrentLevel;
        coinsPickedCurrentLevel = 0;
    }

    void AddFuel(float value)
    {
        currentFuel += value;
        currentFuel = Mathf.Clamp(currentFuel, 0f, 100f);
        UpdateFuelBar();
    }

    private void UpdateCoins()
    {
        canvasUI.coinsText.text = playerCoins.ToString();
        canvasPowerUp.currentCoins.text = playerCoins.ToString();
    }

    private void UpdateFuelBar()
    {
        canvasUI.fuelBarImage.fillAmount = currentFuel / 100;
    }

    IEnumerator FuelDecreaseCoroutine()
    {
        while(isPlaying)
        {
            currentFuel -= fuelDecreaseRate;
            currentFuel = Mathf.Clamp(currentFuel, 0f, 100f);
            UpdateFuelBar();
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    // PowerUps Buttons Functions

    public void ActivatePowerup()
    {
        canvasUI.DisplayNone();
        powerupDelegate();
    }

    public void AddShield()
    {
        if(playerCoins >= shieldCost)
        {
            playerCoins -= shieldCost;
            currentPowerUp = Powerups.Shield;
            UpdateCoins();
            Ignore();
        }
    }

    public void AddBoost()
    {
        if (playerCoins >= boostCost)
        {
            playerCoins -= boostCost;
            currentPowerUp = Powerups.Boost;
            UpdateCoins();
            Ignore();
        }
    }

    public void Ignore()
    {
        canvasPowerUp.panelPowerUp.SetActive(false);
        canvasUI.ResetUI();

        UpdatePowerUpBar();
    }

    private void UpdatePowerUpBar()
    {
        if(currentPowerUp == Powerups.None)
        {
            canvasUI.DisplayNone();
        }
        else if (currentPowerUp == Powerups.Boost)
        {
            canvasUI.DisplayBoost();
        }
        else if (currentPowerUp == Powerups.Shield)
        {
            canvasUI.DisplayShield();
        }
    }
}
