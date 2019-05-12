using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Color originalColor;
    private int x = 0;
    private int y = 0;

    public void SetCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int GetX() { return x; }
    public int GetY() { return y; }

    public void ResetColor()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }

    void Awake()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        /*if (GameManager.instance.gamePieceIsSelected == false && taintedTile)
        {
            GetComponent<Renderer>().material.color = originalColor;
            taintedTile = false;
        }*/
    }

    private void OnMouseOver()
    {
        // if object is currently selected, change color of tile.
        // set GameManager to true?
        // if void make it purple
        // otherwise make it yellow
        //GameManager.instance.TryHighlightTiles(this);
    }
}
