using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // player mechanics controllers
    private AnimationController animController;
    private CombatController combController;

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
        combController = this.GetComponent<CombatController>();
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
            InputChange();
        }
    }

    #region PRIVATE-FUNCTIONS

    private void InputChange()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (combController.playerInCombatMode)
                DisableStanceCall();
            else
                EnableStanceCall();
        }
        

        float horizontal = Input.GetAxis("Horizontal");

        // changing the direction the character looks
        if (horizontal > 0.1f) direction = false;
        else if (horizontal < -0.1f) direction = true;

        float scaleK = Mathf.Abs(transform.localScale.x);

        transform.localScale = direction ? scaleK * (Vector2.left + Vector2.up) : scaleK * (Vector2.right + Vector2.up);

        // changing the character's position
        Vector2 curPos = transform.position;

        // changing the character's movement speed depending on whether he's in the combat mode or not
        float speedMultiplier = combController.playerInCombatMode ? combatMovementSpeed : movementSpeed;
        Vector2 newPos = new Vector2(curPos.x + horizontal * Time.deltaTime * speedMultiplier, curPos.y);

        transform.position = newPos;
    }

    /*
        Calls for a change in animation and combatcontroller to enable battle stance
    */
    private void EnableStanceCall()
    {
        animController.CallAnimationChange(PlayerAnimationType.TRANSITION_IDLE_INTO_COMBATSTANCE);
        combController.EnableStance();
    }

    /*
        Calls for a change in animation and combatcontroller to disable battle stance
    */
    private void DisableStanceCall()
    {
        animController.CallAnimationChange(PlayerAnimationType.TRANSITION_COMBATSTANCE_INTO_IDLE);
        combController.DisableStance();
    }

    #endregion

    #region PUBLIC-FUNCTIONS

    public void BlockMovement()
    {
        allowedToMove = false;
    }

    public void AllowMovement()
    {
        allowedToMove = true;
    }

    #endregion
}
