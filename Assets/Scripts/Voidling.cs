using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voidling : GamePiece
{
    public enum Form
    {
        Shadow,
        Embodied,
        Portal
    }

    private Form form;

    public Form VoidlingForm { get; set; }

    // Start is called before the first frame update
    protected override void Start()
    {
        form = Form.Shadow;

        switch(form)
        {
            case Form.Shadow:
                numMovesLeft = 5;
                break;
            case Form.Embodied:
                numMovesLeft = 4;
                break;
            case Form.Portal:
                numMovesLeft = 0;
                break;
        }

        base.Start();
    }

    void PortalTravel()
    {

    }

    void ThrowCharacter(MovingObject piece)
    {

    }

    public override bool CanMove()
    {
        if (GameManager.instance.IsVoidPlayerTurn && numMovesLeft > 0)
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
        numMovesLeft = 5;
        selected = false;
    }
}
