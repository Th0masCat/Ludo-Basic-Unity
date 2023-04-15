using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMechanics : MonoBehaviour
{
    // Define arrays of tiles for each color, and an array of player game objects
    [SerializeField]
    GameObject[] redTiles;
        
    [SerializeField]
    GameObject[] yellowTiles;

    [SerializeField]
    GameObject[] greenTiles;

    [SerializeField]
    GameObject[] blueTiles;

    [SerializeField]
    GameObject[] pawnGameObject;

    // Declare and initialize turn counter, current index array, and position after roll array
    int turnCounter = 0;
    int[] currentIndex = new int[] { 0, 0, 0, 0 };
    int[] positionAfterRoll = new int[] { 0, 0, 0, 0 };

    public bool wonGame = false;
    public bool gameOver = false;


    [SerializeField]
    TextMeshProUGUI diceText;

    [SerializeField]
    TextMeshProUGUI turnText;


    void Update()
    {
        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            
            // Use switch case to determine whose turn it is based on turn counter
            switch (turnCounter)
            {
                case 0:
                    Turn(0, pawnGameObject[0], redTiles);
                    break;
                case 1:
                    Turn(1, pawnGameObject[1], blueTiles);
                    break;
                case 2:
                    Turn(2, pawnGameObject[2], yellowTiles);
                    break;
                case 3:
                    Turn(3, pawnGameObject[3], greenTiles);
                    break;
                default:
                    break;
            }

            // Increment turn counter and reset if it reaches 4
            turnCounter++;
            if(turnCounter == 4)
            {
                turnCounter = 0;
            }

            turnText.text = "Current Turn: Player " + (turnCounter + 1);
        }
    }

    // Define Turn method to handle player turns
    void Turn(int index, GameObject currentPiece, GameObject[] tiles)
    {
        // Roll dice and log the result
        int diceRollNumber = DiceRoll();
        diceText.text = "Dice: " + diceRollNumber;
        Debug.Log(diceRollNumber);

        // Add dice roll to position after roll array for the current player
        positionAfterRoll[index] += diceRollNumber;

        // Check if player has reached the end of the board and set wonGame=true if index=0(your player)
        if (positionAfterRoll[index] == tiles.Length - 1)
        {
            StartCoroutine(MoveForward(index, currentPiece, tiles));
            gameOver = true;

            if(index == 0)
            {
                wonGame = true;
                Debug.Log("You won");
            }

            Debug.Log("Player " + (index + 1) + " won");
        }

        // Move player forward if they have not reached the end of the board
        if (positionAfterRoll[index] < tiles.Length)
        {
            Debug.Log(positionAfterRoll[index]);
            StartCoroutine(MoveForward(index, currentPiece, tiles));
        }
        // Reset position after roll to current index if player overshoots end of board
        else
        {
            positionAfterRoll[index] = currentIndex[index];
        }

    }

    // Define DiceRoll method to return a random integer between 1 and 6
    int DiceRoll()
    {
        return Random.Range(1, 7);
    }

    // Define MoveForward method to move player piece to the next tile
    IEnumerator MoveForward(int index,GameObject currentPiece, GameObject[] tiles)
    {
        for (int i = currentIndex[index]; i <= positionAfterRoll[index]; i++)
        {
            currentPiece.transform.position = tiles[i].transform.position;
            yield return new WaitForSeconds(0);
        }
        currentIndex[index] = positionAfterRoll[index];
    }

}
