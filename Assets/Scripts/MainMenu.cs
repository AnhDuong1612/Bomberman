using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        audioManager.PlaySFX(audioManager.button);
    }

    public void QuitGame()
    {
        audioManager.PlaySFX(audioManager.button);
        Application.Quit();
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
