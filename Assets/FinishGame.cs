using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishGame : MonoBehaviour
{
    [SerializeField] private Text TextPlayer;

    // Start is called before the first frame update
    void Start()
    {
        TextPlayer.text = ScoreManager.playerWin;

    }
    public void Home()
    {
        Debug.Log("Qua ve home");
        ScoreManager.ResetScore();
        SceneManager.LoadScene("MainMenu");
    }   
    // Update is called once per frame
    void Update()
    {
        
    }
}
