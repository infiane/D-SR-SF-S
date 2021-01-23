using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stance { TOP, MIDDLE, BOTTOM };
public enum AttackType { SLICE, THRUST };

public class CombatController : MonoBehaviour
{
    // player stance
    private Stance currentPlayerStance;

    // a script that is responsible for calling attack calls on player
    private AttackCaller attackCaller;
    // a script that is responsible for calling defense calls on player
    private DefenseCaller defenseCaller;

    // animation controller, obv
    private AnimationController animController;

    // a boolean that shows whether the player is in combat mode or nah
    public bool playerInCombatMode { get; private set; }

    private void Awake()
    {
        attackCaller = this.GetComponent<AttackCaller>();
        defenseCaller = this.GetComponent<DefenseCaller>();
        animController = this.GetComponent<AnimationController>();
    }

    private void Start()
    {
        playerInCombatMode = false;
        currentPlayerStance = Stance.TOP;
    }

    /*
       TODO: describe this mf
    */
    public void EnableStance()
    {
        playerInCombatMode = true;
        animController.CallAnimationChange(PlayerAnimationType.TRANSITION_IDLE_INTO_STANCETOP);
    }

    /*
       TODO: describe this mf
    */
    public void DisableStance()
    {
        playerInCombatMode = false;
        animController.CallAnimationChange(PlayerAnimationType.TRANSITION_STANCETOP_INTO_IDLE);
    }

    /*
        TODO: describe this mf
    */
    public void ChangeStanceMode(Stance stanceMode)
    {
        currentPlayerStance = stanceMode;
        switch (stanceMode)
        {
            case Stance.TOP:
                // TODO: change to top animation
                break;
            case Stance.MIDDLE:
                // TODO: change to middle animation
                break;
            case Stance.BOTTOM:
                // TODO: change to bottom animation
                break;
        }
    }

    public void CallAnAttack(AttackType attack)
    {
        switch (attack)
        {
            case AttackType.SLICE:
                // TODO: call a slice in AttackCaller
                attackCaller.Slice(currentPlayerStance);
                break;
            case AttackType.THRUST:
                // TODO: call a thrust in AttackCaller
                attackCaller.Thrust(currentPlayerStance);
                break;
        }
    }
}
