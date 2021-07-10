using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //counting and printing score
    public Text coinsText;

    public static int minCoinforNextScene;
    void Start()
    {
        minCoinforNextScene = 10;
        
    }

    void Update()
    {
        coinsText.text = PlayerController.numberOfCoins.ToString();

        //check game is over or not
        if(PlayerController.isGameOver)
        {
            GameOver();
        }
        //open another level for pro gamer
        else if(PlayerController.isGoNext)
        {
            NextLevel();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("EndGame");
        PlayerController.isGameOver = false;
    }


    private void NextLevel()
    {
            PlayerController.numberOfCoins = 0;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        PlayerController.isGoNext = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        PlayerController.isGameOver = false;
    }

}
