using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voidling : MovingObject
{
    int numMovesLeft;

    enum Form
    {
        Shadow,
        Embodied,
        Portal
    }

    Form form;

    // Start is called before the first frame update
    void Start()
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PortalTravel()
    {

    }

    void ThrowCharacter(MovingObject piece)
    {

    }

    protected override void OnCantMove<T>(T component)
    {
        throw new System.NotImplementedException();
    }
}
