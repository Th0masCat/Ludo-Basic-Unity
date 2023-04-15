// This script manages the in-game UI and end game screen

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // Reference to GameMechanics script to check if game is over
    GameMechanics gameMechanics;

    // Reference to TextMeshProUGUI object to display end game message
    [SerializeField]
    TextMeshProUGUI screenText;

    // Reference to the end game screen object to enable/disable it
    [SerializeField]
    GameObject endGameScreen;

    // Reference to the game UI object to enable/disable it
    [SerializeField]
    GameObject gameUI;

    public bool paused = false;

    private void Start()
    {
        // Get the GameMechanics component from the GameObject this script is attached to
        gameMechanics = GetComponent<GameMechanics>();
    }

    private void Update()
    {
        //Pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        //Enable game UI and disable pause UI
        if (!paused)
        {
            endGameScreen.SetActive(false);
            gameUI.SetActive(true);
        }

        // If the game is over
        if (gameMechanics.gameOver || paused)
        {
            // Enable the end game screen and disable the game UI
            endGameScreen.SetActive(true);
            gameUI.SetActive(false);

            if (paused)
            {
                screenText.text = "Paused";
            }
            // If the player has won the game
            else if (gameMechanics.wonGame)
            {
                screenText.text = "You won";
            }
            else
            {
                screenText.text = "You Lost";
            } 
        }

    }

    
}
