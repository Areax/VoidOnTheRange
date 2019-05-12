using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidPlayer : Player
{
    public override bool IsThisPlayersTurn()
    {
        return GameManager.instance.IsVoidPlayerTurn;
    }

    public override bool TryHighlightTiles(Tile tile)
    {
        Voidling voidling = (Voidling)GameManager.instance.MovingObject;

        if (voidling.VoidlingForm == Voidling.Form.Shadow && voidling.CanMove())
        {
            taintedTiles.Add(tile);
            tile.GetComponent<Renderer>().material.SetColor("_Color", new Color(128, 0, 128));
            voidling.NumMovesLeft--;
            return true;
        }

        return false;
    }
}
