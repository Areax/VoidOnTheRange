using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;
    [HideInInspector] public bool gamePieceIsSelected;
    [HideInInspector] public bool voidPlayerTurn;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        gamePieceIsSelected = false;
        voidPlayerTurn = true;
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    public void GameOver()
    {
        enabled = false;
    }

    void InitGame()
    {
        boardScript.SetupScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
