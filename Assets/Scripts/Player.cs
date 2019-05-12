using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected int numActions = 0;
    protected List<Tile> taintedTiles;
    protected bool validMovement;

    void Awake()
    {
        taintedTiles = new List<Tile>();
        ResetNumActions();
        validMovement = true;
    }

    void Update()
    {
        // if piece is selected
        if (GameManager.instance.PieceIsSelected() && IsThisPlayersTurn())
        {
            MovingObject movingObject = GameManager.instance.MovingObject;
            Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast(cursorRay.origin, cursorRay.direction, Mathf.Infinity);

            if (hit2D)
            {
                GameObject tileObject = hit2D.collider.gameObject;
                // if the object is not a tile, maybe still add tile under object to list (it's the parent)
                // need to check if tile has child always, and set MoveablePath value to false if not
                // this way you can't MOVE through objects, but you can still highlight a false path through them.!

                // if mouse is over object and object's parent is tile, set tileObject to object's parent
                if (hit2D.collider.tag != "tile" && tileObject.transform.parent != null && tileObject.transform.parent.GetComponent<Tile>() != null)
                {
                    tileObject = tileObject.transform.parent.gameObject;
                }

                Tile tile = tileObject.GetComponent<Tile>();
                // if the selected piece can still move, is a nearby tile
                // the current tile is not colored, it is this player's turn
                if (movingObject.CanMove() && IsNearbyTile(tile) && tile != null && !taintedTiles.Contains(tile) && IsThisPlayersTurn())
                {
                    if(TryHighlightTiles(tile.GetComponent<Tile>()))
                    {
                        if (tileObject.transform.childCount != 0 && movingObject.transform.parent != tileObject.transform)
                        {
                            validMovement = false;
                        }
                    }
                }

                // on left mouse up and CAN move to current tile
                if (Input.GetMouseButtonUp(0))
                {
                    // current tile does not have any children
                    if (validMovement)
                    {
                        // if object can move && isHighlighted, set position of object to current tile
                        // if object cannot move anymore, Just move object to last highlighted square
                        Transform tileTransform;
                        if (movingObject.CanMove() && taintedTiles.Contains(tile))
                        {
                            tileTransform = tileObject.transform;
                        }
                        else
                        {
                            tileTransform = taintedTiles[taintedTiles.Count - 1].transform;
                        }

                        movingObject.transform.position = new Vector2(tileTransform.transform.position.x, tileTransform.transform.position.y);
                        movingObject.transform.SetParent(tileTransform);
                        numActions--;
                    }

                    // reset moves for object, reset tiles' color, subtract num actions for player
                    ResetMovement();
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    ResetMovement();
                }
            }
            else
            {
                // Didn't hit anything. Just move object to last highlighted square
                if (Input.GetMouseButtonUp(0) && taintedTiles.Count > 0 && validMovement)
                {
                    Vector2 tilePos = taintedTiles[taintedTiles.Count - 1].transform.position;

                    if (validMovement)
                    {
                        movingObject.transform.position = new Vector2(tilePos.x, tilePos.y);
                        movingObject.transform.SetParent(taintedTiles[taintedTiles.Count - 1].transform);
                        numActions--;
                    }
                    ResetMovement();
                }
                else if(Input.GetMouseButton(1) || Input.GetMouseButtonUp(0))
                {
                    ResetMovement();
                }
            }
        }
    }

    public bool HasActionsLeft()
    {
        return numActions > 0 ? true : false;
    }

    public void ResetNumActions()
    {
        numActions = 3;
    }

    public void AddTaintedTile(Transform tileTransform)
    {
        taintedTiles.Add(tileTransform.GetComponent<Tile>());
    }

    private void UnhighlightTiles()
    {
        taintedTiles.ForEach(x => x.ResetColor());
        taintedTiles.Clear();
    }

    private void ResetMovement()
    {
        GameManager.instance.MovingObject.ResetMovement();
        GameManager.instance.MovingObject = null;
        UnhighlightTiles();
        validMovement = true;
    }

    private bool IsNearbyTile(Tile destTile)
    {
        Tile curTile = taintedTiles[taintedTiles.Count - 1];

        // current tile is an odd column
        if(curTile.x % 2 == 1 && 
            ((curTile.X - 1 == destTile.X && curTile.Y == destTile.Y) ||
            (curTile.X == destTile.X && curTile.Y + 1 == destTile.Y) ||
            (curTile.X + 1 == destTile.X && curTile.Y == destTile.Y) ||
            (curTile.X + 1 == destTile.X && curTile.Y - 1 == destTile.Y) ||
            (curTile.X == destTile.X && curTile.Y - 1 == destTile.Y) ||
            (curTile.X - 1 == destTile.X && curTile.Y - 1 == destTile.Y)))
        {
            return true;
        }
        // current tile is an even column
        else if(curTile.x % 2 == 0 &&
            ((curTile.X - 1 == destTile.X && curTile.Y + 1 == destTile.Y) ||
            (curTile.X == destTile.X && curTile.Y + 1 == destTile.Y) ||
            (curTile.X + 1 == destTile.X && curTile.Y + 1 == destTile.Y) ||
            (curTile.X + 1 == destTile.X && curTile.Y  == destTile.Y) ||
            (curTile.X == destTile.X && curTile.Y - 1 == destTile.Y) ||
            (curTile.X - 1 == destTile.X && curTile.Y == destTile.Y)))
        {
            return true;
        }

        return false;
    }

    public abstract bool IsThisPlayersTurn();
    // highlighting == "invisible" movement, subtracting move from object
    public abstract bool TryHighlightTiles(Tile tile);
}
