using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = .3f;
    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    private bool selected;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        /*if(hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }*/

        return false;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.instance.gamePieceIsSelected = selected = true;
        }
    }

    protected virtual void AttemptMove <T> (int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
            return;

        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
            OnCantMove(hitComponent);
    }

    protected abstract void OnCantMove <T> (T component)
        where T : Component;

    // Update is called once per frame
    void Update()
    {
        if(selected && Input.GetMouseButtonUp(0))
        {
            Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(cursorRay.origin, cursorRay.direction, Mathf.Infinity);
            //Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (hit && hit.collider.tag == "tile")
            {
                Vector2 tilePos = hit.collider.transform.position;
                transform.position = new Vector2(tilePos.x, tilePos.y);
                // set parent to tile object?
            }

            GameManager.instance.gamePieceIsSelected = selected = false;
        }
    }
}
