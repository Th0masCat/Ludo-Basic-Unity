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
    GameObject player;

    [SerializeField]
    GameObject playerPrefab;

    int[] startPositions = new int[] {0, 13, 26, 39};


    int currentIndex = 0;
    int positionAfterRoll = 0;

    void Start()
    {
        foreach(var i in startPositions)
        {
            Instantiate(playerPrefab, redTiles[i].transform.position, redTiles[i].transform.rotation);
        }
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int diceRollNumber = DiceRoll();
            Debug.Log(diceRollNumber);
            positionAfterRoll += diceRollNumber;
            if (positionAfterRoll > redTiles.Length - 1)
            {
                Debug.Log(positionAfterRoll);
                
            }

            MoveForward();
        }
    }

    int DiceRoll()
    {
        return Random.Range(1, 7);
    }

    void MoveForward()
    {
        for (int i = currentIndex; i <= positionAfterRoll; i++)
        {
            player.transform.position = redTiles[i].transform.position;
            Debug.Log(positionAfterRoll);
        }
        currentIndex = positionAfterRoll;
    }
    
}
