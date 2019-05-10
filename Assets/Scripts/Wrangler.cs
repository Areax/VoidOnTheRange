using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrangler : GamePiece
{
    // Start is called before the first frame update
    /*void Start()
    {
        numMovesLeft = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }*/

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

    protected override void OnCantMove<T>(T component)
    {
        throw new System.NotImplementedException();
    }
}
