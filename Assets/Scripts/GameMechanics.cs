using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class GameMechanics : MonoBehaviour
{

    [SerializeField]
    GameObject[] redTiles;

    [SerializeField]
    GameObject[] yellowTiles;

    [SerializeField]
    GameObject[] greenTiles;

    [SerializeField]
    GameObject[] blueTiles;

    [SerializeField]
    GameObject[] playerPrefab;



    int x = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (x)
            {
                case 0:
                    Turn(0, playerPrefab[0], redTiles);
                    break;
                case 1:
                    Turn(1, playerPrefab[1], blueTiles);
                    break;
                case 2:
                    Turn(2, playerPrefab[2], yellowTiles);
                    break;
                case 3:
                    Turn(3, playerPrefab[3], greenTiles);
                    break;
                default:
                    break;
            }
            x++;
            if(x == 4)
            {
                x = 0;
            }

        }
    }

    int[] currentIndex = new int[] {0, 0, 0, 0};
    int[] positionAfterRoll = new int[] { 0, 0, 0, 0 };



    void Turn(int index, GameObject currentPiece, GameObject[] tiles)
    {
        int diceRollNumber = DiceRoll();

        positionAfterRoll[index] += diceRollNumber;
        
        if (positionAfterRoll[index] < tiles.Length - 1)
        {
            MoveForward(index, currentPiece, tiles);
            Debug.Log(positionAfterRoll[index]);
        }

    }

    int DiceRoll()
    {
        return Random.Range(1, 7);
    }

    void MoveForward(int index,GameObject currentPiece, GameObject[] tiles)
    {
        for (int i = currentIndex[index]; i <= positionAfterRoll[index]; i++)
        {
            currentPiece.transform.position = tiles[i].transform.position;
            Debug.Log(positionAfterRoll[index]);
        }
        currentIndex[index] = positionAfterRoll[index];
    }

}
