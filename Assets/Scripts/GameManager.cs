using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private bool isPlaying = false;
    public bool IsPlaying { get { return isPlaying; } }


    private int currentLevel = 1;
    public int CurrentLevel { get { return currentLevel; } }

    private int playerCoins = 0;
    public int PlayerCoins { get { return playerCoins; } set { playerCoins = value; } }

    private float currentFuel = 100;
    public float CurrentFuel { get { return currentFuel; } set { currentFuel = value; } }

    [SerializeField] 
    private CanvasUI canvasUI;

    [SerializeField]
    private float fuelDecreaseRate = 0.5f;

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

        SimpleCollectibleScript.coinPickedDelegate += AddCoin;
        SimpleCollectibleScript.fuelPickedDelegate += AddFuel;

        UpdateCoins();
        UpdateFuelBar();
    }

    public void Play()
    {
        canvasUI.ActiveUI();
        isPlaying = true;
        StartCoroutine(FuelDecreaseCoroutine());
    }

    public void GameOver()
    {
        isPlaying = false;
        // DO THE REST OF GAME OVER
    }

    void Update()
    {
        
    }

    void AddCoin()
    {
        playerCoins++;
        UpdateCoins();
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
    }

    private void UpdateFuelBar()
    {
        canvasUI.fuelBarImage.fillAmount = currentFuel / 100;
        canvasUI.fuelIconAnim.SetFloat("CurrentFuel", currentFuel);
    }

    IEnumerator FuelDecreaseCoroutine()
    {
        while(isPlaying)
        {
            currentFuel -= fuelDecreaseRate;
            UpdateFuelBar();
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
