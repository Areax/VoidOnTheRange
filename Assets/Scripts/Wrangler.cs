using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrangler : GamePiece
{
    void Lasso()
    {
        // on up click
        // if cow is 4 squares away, lasso em in
    }

    void Shoot()
    {
        // on up click
        // if embodied voidling, random dice
        // if 5/6, gg
    }

    void TransformVoidling()
    {

    }

    public void Move()
    {
        // subtract movement
    }

    public override bool CanMove()
    {
        if (!GameManager.instance.IsVoidPlayerTurn && numMovesLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void ResetMovement()
    {
        numMovesLeft = 3;
        selected = false;
    }
}
