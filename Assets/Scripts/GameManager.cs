using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    // Screen Infos
    public float screenWidth;
    public float screenHeight;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);


            screenWidth = (float)Screen.width / 2.0f;
            screenHeight = (float)Screen.height / 2.0f;

            Debug.Log("WIDTH = " + screenWidth + " | HEIGHT = " + screenHeight);
        }
    }


    void Start()
    {
        //Application.targetFrameRate = 60;
    }


    void Update()
    {
        
    }
}
