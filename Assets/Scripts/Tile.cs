using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool taintedTile;
    private Color originalColor;

    void Awake()
    {
        taintedTile = false;
        originalColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        if (GameManager.instance.gamePieceIsSelected == false && taintedTile)
        {
            GetComponent<Renderer>().material.color = originalColor;
            taintedTile = false;
        }
    }

    private void OnMouseOver()
    {
        // if object is currently selected, change color of tile.
        // set GameManager to true?
        // if void make it purple
        // otherwise make it yellow
        if(GameManager.instance.gamePieceIsSelected == true && !taintedTile)
        {
            if (GameManager.instance.voidPlayerTurn)
            {
                GetComponent<Renderer>().material.SetColor("_Color", new Color(128, 0, 128));
                taintedTile = true;
            }
        }
    }
}
