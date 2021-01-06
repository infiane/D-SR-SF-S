using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private AnimationController animController;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float combatMovementSpeed;

    // false - looking right, true - looking left
    private bool direction = false;

    public bool AllowedToMove { get { return allowedToMove; } }
    private bool allowedToMove = true;

    private void Awake()
    {
        animController = this.GetComponent<AnimationController>();
    }

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

        // changing the character's position
        Vector2 curPos = transform.position;

        // changing the character's movement speed depending on whether he's in the combat mode or not
        float speedMultiplier = animController.PlayerInCombatMode ? combatMovementSpeed : movementSpeed;
        Vector2 newPos = new Vector2(curPos.x + horizontal * Time.deltaTime * speedMultiplier, curPos.y);

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
