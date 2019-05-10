using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    public static int columns = 14;
    public static int rows = 11;
    public float radius = 1.5f;
    public GameObject tile;
    public GameObject wrangler;
    public GameObject cow;
    public GameObject voidling;

    private Transform boardHolder;
    private GameObject[][] gridPositions = new GameObject[columns][];

    int width = 20;
    int height = 20;

    float yOffset = 2.616f;
    float xOffset = 2.276f;


    void InitializeList()
    {
        //gridPositions.Clear();

        // Wranglers
        Instantiate(wrangler, gridPositions[2][3].transform.position, Quaternion.identity);
        Instantiate(wrangler, gridPositions[10][2].transform.position, Quaternion.identity);
        // Cows
        Instantiate(cow, gridPositions[4][2].transform.position, Quaternion.identity);
        Instantiate(cow, gridPositions[5][0].transform.position, Quaternion.identity);
        Instantiate(cow, gridPositions[5][4].transform.position, Quaternion.identity);
        Instantiate(cow, gridPositions[6][2].transform.position, Quaternion.identity);
        Instantiate(cow, gridPositions[7][1].transform.position, Quaternion.identity);
        Instantiate(cow, gridPositions[8][3].transform.position, Quaternion.identity);
        Instantiate(cow, gridPositions[11][5].transform.position, Quaternion.identity);
        Instantiate(cow, gridPositions[12][7].transform.position, Quaternion.identity);

        // Void Spawn
        Instantiate(voidling, gridPositions[0][9].transform.position, Quaternion.identity);
        Instantiate(voidling, gridPositions[1][8].transform.position, Quaternion.identity);
        Instantiate(voidling, gridPositions[2][9].transform.position, Quaternion.identity);
        Instantiate(voidling, gridPositions[2][10].transform.position, Quaternion.identity);
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Tile").transform;

        int mid = (columns + rows) / 4;

        for (int x = 0; x < columns; x++)
        {
            gridPositions[x] = new GameObject[rows];

            for (int y = 0; y < rows; y++)
            {
                float yPos = y * yOffset;

                if(x % 2 == 1)
                {
                    yPos -= yOffset / 2f;
                }

                GameObject instance = Instantiate(tile, new Vector3(x * xOffset, yPos, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
                gridPositions[x][y] = instance;
            }
        }
    }

    public void SetupScene()
    {
        BoardSetup();
        InitializeList();
    }
}
