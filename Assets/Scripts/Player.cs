using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected int numActions = 0;
    protected List<Tile> taintedTiles;

    void Awake()
    {
        taintedTiles = new List<Tile>();
        SetNumActions();
    }

    void Update()
    {
        // if piece is selected
        if (GameManager.instance.PieceIsSelected() && IsThisPlayersTurn())
        {
            Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast(cursorRay.origin, cursorRay.direction, Mathf.Infinity);
            
            MovingObject movingObject = GameManager.instance.MovingObject;
            bool IsMouseOnTile = hit2D && hit2D.collider.tag == "tile";
            // if the object is not a tile, maybe still add tile under object to list (it's the parent)
            // need to check if tile has child always, and set MoveablePath value to false if not
            // this way you can't MOVE through objects, but you can still highlight a false path through them.!

            // if mouse is over tile
            if (IsMouseOnTile)
            {
                // if the selected piece can still move
                if (movingObject.CanMove() && !movingObject.transform.IsChildOf(hit2D.transform))
                {
                    TryHighlightTiles(hit2D.collider.gameObject.GetComponent<Tile>());
                }
            }

            // on left mouse up and CAN move to current tile
            if (Input.GetMouseButtonUp(0))
            {
                // current tile does not have any children
                if (IsMouseOnTile && hit2D.transform.childCount == 0)
                {
                    // if object can move, set position of object to current tile
                    // if object cannot move anymore, grab tile from last of list
                    Vector2 tilePos;
                    if(movingObject.CanMove())
                    {
                        tilePos = hit2D.collider.transform.position;
                    }
                    else
                    {
                        tilePos = taintedTiles[taintedTiles.Count - 1].transform.position;
                    }

                    movingObject.transform.position = new Vector2(tilePos.x, tilePos.y);
                    movingObject.transform.parent = hit2D.transform;
                }

                // reset moves for object, reset tiles' color, subtract num actions for player
                GameManager.instance.MovingObject.ResetMovement();
                GameManager.instance.MovingObject = null;
                UnhighlightTiles();
                numActions--;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                GameManager.instance.MovingObject.ResetMovement();
                GameManager.instance.MovingObject = null;
                UnhighlightTiles();
            }
        }
    }

    public void TryHighlightTiles(Tile tile)
    {
        // the current tile is not colored, it is this player's turn
        if (!taintedTiles.Contains(tile) && IsThisPlayersTurn())
        {
            HighlightTiles(tile);
        }
    }

    public bool HasActionsLeft()
    {
        return numActions > 0 ? true : false;
    }

    protected void SetNumActions()
    {
        numActions = 3;
    }

    private void UnhighlightTiles()
    {
        taintedTiles.ForEach(x => x.ResetColor());
        taintedTiles.Clear();
    }

    public abstract bool IsThisPlayersTurn();
    // highlighting == "invisible" movement, subtracting move from object
    public abstract void HighlightTiles(Tile tile);
}
