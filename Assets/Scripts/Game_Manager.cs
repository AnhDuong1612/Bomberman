using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public GameObject[] palyers;
    public void CheckWin()
    {
        int alive = 0;
        foreach(GameObject player in palyers)
        {
            if (player.activeSelf)
            {
                alive++;
            }
        }
        if (alive <= 1)
        {
            Invoke(nameof(NewRound), 3f);
        }
    }
    public void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
