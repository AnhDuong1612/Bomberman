using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public GameObject[] players;
    public static string playerWin = "";
    [SerializeField] public Text player1Score;
    [SerializeField] public Text player2Score;



    public static int score1 = 0;
    public static int score2 = 0;

    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        player1Score = GameObject.FindGameObjectWithTag("Player1Score").GetComponent<Text>();
        player2Score = GameObject.FindGameObjectWithTag("Player2Score").GetComponent<Text>();

        if (player1Score == null) Debug.Log("player1Score bi null");
        else Debug.Log("Player1Score != null");

        if (player2Score == null) Debug.Log("player2Score bi null");
        else Debug.Log("Player2Score != null");
    }
    private void Start()
    {
        player1Score.text = score1.ToString();
        player2Score.text = score2.ToString();
    }
    public void AddScore()
    {
        bool player1Active = players[0].activeSelf;
        bool player2Active = players[1].activeSelf;

        if (player1Active)
        {
            score1++;
            player1Score.text = score1.ToString();
        }
        if (player2Active)
        {
            score2++;
            player2Score.text = score2.ToString();
        }

        Debug.Log(score1 + " - " + score2);


        if (score1 == 2)
        {
            playerWin = "Player1";
            SceneManager.LoadScene("Finish Game");
        }
        else if (score2 == 2)
        {
            playerWin = "Player 2";
            SceneManager.LoadScene("Finish Game");
        }
    }
    public void Update()
    {
    }
    public void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public static void ResetScore()
    {
        score1 = 0;
        score2 = 0;
    }
}
