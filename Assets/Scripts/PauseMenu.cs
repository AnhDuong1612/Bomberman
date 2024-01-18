using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public void Awake()
    {
        pausePanel = GameObject.FindGameObjectWithTag("PausePanel");
        if (pausePanel == null) Debug.Log("Pause panel null");
        else Debug.Log("Pause panel != null");
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

}
