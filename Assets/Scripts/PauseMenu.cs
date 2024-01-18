using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    private AudioManager audioManager;
    public void Awake()
    {
        pausePanel = GameObject.FindGameObjectWithTag("PausePanel");
        if (pausePanel == null) Debug.Log("Pause panel null");
        else Debug.Log("Pause panel != null");

        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        audioManager.PlaySFX(audioManager.button);
    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        audioManager.PlaySFX(audioManager.button);

    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        audioManager.PlaySFX(audioManager.button);
    }
    public void Option()
    {
        audioManager.PlaySFX(audioManager.button);
    }
    public void cancelOption()
    {
        audioManager.PlaySFX(audioManager.button);
    }

}
