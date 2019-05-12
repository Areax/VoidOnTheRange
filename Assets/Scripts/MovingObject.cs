using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = .3f;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    protected bool selected;
    protected int numMovesLeft;

    public int NumMovesLeft { get { return numMovesLeft; } set { numMovesLeft = value; } }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;

        ResetMovement();
    }

    private void OnMouseOver()
    {
        if (!selected && Input.GetMouseButtonDown(0) && CanMove())
        {
            GameManager.instance.MovingObject = this;
            GameManager.instance.GetCurrentPlayer().AddTaintedTile(this.transform.parent);
            selected = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public abstract void ResetMovement();
    public abstract bool CanMove();
}
