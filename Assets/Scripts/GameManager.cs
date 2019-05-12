using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;
    [HideInInspector] public bool IsVoidPlayerTurn;
    [HideInInspector] public MovingObject MovingObject;

    public VoidPlayer voidPlayer;
    private WranglerPlayer wranglerPlayer;

    public bool PieceIsSelected()
    {
        return MovingObject != null;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        MovingObject = null;
        IsVoidPlayerTurn = true;
        DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
        voidPlayer = GetComponent<VoidPlayer>();
        wranglerPlayer = GetComponent<WranglerPlayer>();

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
        // void player has no more actions
        if(IsVoidPlayerTurn && !voidPlayer.HasActionsLeft())
        {
            IsVoidPlayerTurn = false;
            wranglerPlayer.ResetNumActions();
        }
        else if(!IsVoidPlayerTurn && !wranglerPlayer.HasActionsLeft())
        {
            IsVoidPlayerTurn = true;
            voidPlayer.ResetNumActions();
        }
    }

    public Player GetCurrentPlayer()
    {
        return IsVoidPlayerTurn ? (Player) voidPlayer : (Player) wranglerPlayer;
    }
}
