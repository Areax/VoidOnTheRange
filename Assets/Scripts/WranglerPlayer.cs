using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WranglerPlayer : Player
{
    public override bool IsThisPlayersTurn()
    {
        return !GameManager.instance.IsVoidPlayerTurn;
    }

    public override bool TryHighlightTiles(Tile tile)
    {
        Wrangler wrangler = (Wrangler)GameManager.instance.MovingObject;

        if (wrangler.CanMove())
        {
            taintedTiles.Add(tile);
            tile.GetComponent<Renderer>().material.SetColor("_Color", new Color(128, 0, 128));
            wrangler.NumMovesLeft--;
            return true;
        }

        return false;
    }
}
