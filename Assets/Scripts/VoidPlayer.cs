using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidPlayer : Player
{
    public override bool IsThisPlayersTurn()
    {
        return GameManager.instance.IsVoidPlayerTurn;
    }

    public override void HighlightTiles(Tile tile)
    {
        Voidling voidling = (Voidling)GameManager.instance.MovingObject;

        if (voidling.VoidlingForm == Voidling.Form.Shadow && voidling.CanMove())
        {
            tile.GetComponent<Renderer>().material.SetColor("_Color", new Color(128, 0, 128));
            taintedTiles.Add(tile);
            voidling.NumMovesLeft--;
        }
    }
}
