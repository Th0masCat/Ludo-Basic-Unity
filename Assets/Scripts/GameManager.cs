using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //private bool gameStarted = false;
    //private bool gameOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }


    //public void GameOver()
    //{
    //    gameOver = true;
    //    SceneManager.LoadScene("GameOverScene");
    //}

    //public void RestartGame()
    //{
    //    gameStarted = false;
    //    gameOver = false;
    //    SceneManager.LoadScene("StartScreenScene");
    //}

}
