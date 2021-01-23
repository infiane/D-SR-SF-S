using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region TESTING-VALUES
    [SerializeField]
    private float angleAddition = 180;

    #endregion

    // player mechanics controllers
    private AnimationController animController;
    private CombatController combController;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float combatMovementSpeed;

    // false - looking right, true - looking left
    private bool playerLookingLeft = false;

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
        playerLookingLeft = false;
    }

    private void Update()
    {
        if (allowedToMove)
        {
            InputChange();
        }
    }

    #region PRIVATE-FUNCTIONS

    private void InputChange()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("Mouse Position in world space - " + mousePos);

        Vector2 playerPos = transform.position;
        Vector2 fromPlayerToPos = mousePos - playerPos;
        float angle = Vector2.SignedAngle(Vector2.right, fromPlayerToPos.normalized);
        //Debug.Log("Angle - " + angle);

        playerLookingLeft = angle <= 90 && angle >= -90 ? false : true;

        bool combStanceOn = combController.playerInCombatMode;

        #region stance-input
        // Transitioning into combat stance
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (combStanceOn)
                DisableStanceCall();
            else
                EnableStanceCall();
        }
        #endregion

        #region battle-input

        // if player is in combat stance
        if (combStanceOn)
        {
            if (playerLookingLeft)
            {
                if (angle < 150 && angle >= 90)
                    combController.ChangeStanceMode(Stance.TOP);
                else if (angle <= 180 && angle >= 150 || angle >= -180 && angle <= -150)
                    combController.ChangeStanceMode(Stance.MIDDLE);
                else if (angle <= -90 && angle > -150)
                    combController.ChangeStanceMode(Stance.BOTTOM);
            }
            else
            {
                if (angle <= 90 && angle > 30)
                    combController.ChangeStanceMode(Stance.TOP);
                else if (angle <= 30 && angle >= -30)
                    combController.ChangeStanceMode(Stance.MIDDLE);
                else if (angle < -30 && angle >= -90)
                    combController.ChangeStanceMode(Stance.BOTTOM);
            }

            if (Input.GetMouseButtonDown(0))
            {
                combController.CallAnAttack(AttackType.SLICE);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                combController.CallAnAttack(AttackType.THRUST);
            }
        }

        #endregion

        #region moving-input

        float horizontal = Input.GetAxis("Horizontal");
        float scaleK = Mathf.Abs(transform.localScale.x);

        transform.localScale = playerLookingLeft ? scaleK * (Vector2.left + Vector2.up) : scaleK * (Vector2.right + Vector2.up);
        

        // changing the character's position
        Vector2 curPos = transform.position;

        // changing the character's movement speed depending on whether he's in the combat mode or not
        float speedMultiplier = combController.playerInCombatMode ? combatMovementSpeed : movementSpeed;
        Vector2 newPos = new Vector2(curPos.x + horizontal * Time.deltaTime * speedMultiplier, curPos.y);

        transform.position = newPos;

        #endregion
    }

    /*
        Calls for a change in animation and combatcontroller to enable battle stance
    */
    private void EnableStanceCall()
    {
        combController.EnableStance();
    }

    /*
        Calls for a change in animation and combatcontroller to disable battle stance
    */
    private void DisableStanceCall()
    {
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
