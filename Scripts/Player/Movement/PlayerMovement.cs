using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool AllowedToMove
    {
        get { return allowedToMove; }
    }

    [SerializeField]
    private float movementSpeed;

    // false - looking right, true - looking left
    private bool direction = false;

    private bool allowedToMove = true;

    private void Start()
    {
        allowedToMove = true;
        direction = false;
    }

    private void FixedUpdate()
    {
        if (allowedToMove)
        {
            Move();
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        // changing the direction the character looks
        if      (horizontal > 0.1f) direction = false;
        else if (horizontal < -0.1f) direction = true;

        float scaleK = Mathf.Abs(transform.localScale.x);

        transform.localScale = direction ? scaleK * (Vector2.left + Vector2.up) : scaleK * (Vector2.right + Vector2.up);

        // chaning the character's position
        Vector2 curPos = transform.position;
        Vector2 newPos = new Vector2(curPos.x + horizontal * Time.deltaTime * movementSpeed, curPos.y);

        transform.position = newPos;
    }

    public void BlockMovement()
    {
        allowedToMove = false;
    }

    public void AllowMovement()
    {
        allowedToMove = true;
    }
}
