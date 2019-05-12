using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WranglerPlayer : Player
{
    public override bool IsThisPlayersTurn()
    {
        return !GameManager.instance.IsVoidPlayerTurn;
    }

    public override void HighlightTiles(Tile tile)
    {
        Wrangler wrangler = (Wrangler)GameManager.instance.MovingObject;

        if (wrangler.CanMove())
        {
            tile.GetComponent<Renderer>().material.SetColor("_Color", new Color(128, 0, 128));
            taintedTiles.Add(tile);
            wrangler.NumMovesLeft--;
        }
    }
}
