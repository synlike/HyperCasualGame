using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingButton : MonoBehaviour
{

    private void Start()
    {
        GameManager.Instance.CanvasUI.HidePanel();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Replay()
    {
        GameManager.Instance.CanvasUI.ResetUI();
        GameManager.Instance.ResetAll();
        SceneManager.LoadScene(1);
    }
}
