using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    GameMechanics gameMechanics;

    [SerializeField]
    TextMeshProUGUI screenText;

    [SerializeField]
    GameObject endGameScreen;

    [SerializeField]
    GameObject gameUI;

    private void Start()
    {
        gameMechanics = GetComponent<GameMechanics>();
    }

    private void Update()
    {
        if (gameMechanics.gameOver)
        {
            endGameScreen.SetActive(true);
            gameUI.SetActive(false);
            if (gameMechanics.wonGame)
            {
                screenText.text = "You won";
            }
            else
            {
                screenText.text = "You Lost";
            } 
        }

    }

    public void ReplayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitButton()
    {
        SceneManager.LoadScene(0);
    }
}
