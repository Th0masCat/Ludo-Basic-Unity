using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    // Array of pawns
    [SerializeField]
    GameObject[] pawnGameObject;

    // Variables to keep track of the game state
    int turnCounter = 0;
    int[] currentIndex = new int[] { 0, 0, 0, 0 };
    int[] positionAfterRoll = new int[] { 0, 0, 0, 0 };
    public string wonGameText;
    public bool gameOver = false;
    bool turnGoing = false;


    // Text elements for displaying dice roll and current turn
    [SerializeField]
    TextMeshProUGUI diceText;

    [SerializeField]
    TextMeshProUGUI turnText;

    // Text game object to display when to press spacebar for player's turn
    [SerializeField]
    GameObject spacebarText;

    void Update()
    {
        // Checking if the space key is pressed and the game is not over or the turn is not ongoing
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && !turnGoing)
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

        }
    }

    // Method to start a player's turn
    void Turn(int index, GameObject currentPiece, GameObject[] tiles)
    {
        // Rolling the dice
        int diceRollNumber = DiceRoll();
        diceText.text = "Dice: " + diceRollNumber;
        Debug.Log(diceRollNumber);

        // Updating the player's position after the dice roll
        positionAfterRoll[index] += diceRollNumber;

        // Checking if the player has won the game
        if (positionAfterRoll[index] == tiles.Length - 1)
        {
            StartCoroutine(MoveForward(index, currentPiece, tiles));
            gameOver = true;

            wonGameText = "Player " + (index+1) + " won!";
        }

        // Moving the player's pawn forward if they haven't won the game
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

            // set the turnGoing bool to true to prevent the player from rolling the dice again before their turn is over
            turnGoing = true;

            //Spacebar text game object set active
            spacebarText.SetActive(false);

            //Wait for 0.5 seconds
            yield return new WaitForSeconds(0.5f);
        }

        spacebarText.SetActive(true);
        pawnGameObject[index].GetComponent<Animator>().SetBool("moving", false);
        turnGoing = false;

        turnText.text = "Current Turn: Player " + (turnCounter + 1);

        // Update the current index to the position after rolling the dice
        currentIndex[index] = positionAfterRoll[index];
    }

}
