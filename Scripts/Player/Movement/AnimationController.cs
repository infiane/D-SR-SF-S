using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationType { TRANSITION_IDLE_INTO_STANCETOP,
                                  TRANSITION_STANCETOP_INTO_IDLE,
                                  SLICE_FROM_ANY_STANCE,
                                  THRUST_FROM_ANY_STANCE,
                                  SLICE_SLICE_FROM_ANY_STANCE,
                                  SLICE_THRUST_FROM_ANY_STANCE,
                                  THRUST_SLICE_FROM_ANY_STANCE,
                                  THRUST_THRUST_FROM_ANY_STANCE,
                                  FROM_ATTACK_TO_STANCE}

public class AnimationController : MonoBehaviour
{

    private Animator animator;
    private PlayerMovement playerMov;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        playerMov = this.GetComponent<PlayerMovement>();
    }

    /*
        Gets the type of animation it is required to play and plays it
    */
    public void CallAnimationChange(PlayerAnimationType animType)
    {
        switch(animType)
        {
            case PlayerAnimationType.TRANSITION_IDLE_INTO_STANCETOP:
                animator.SetTrigger("TransIntoCombat");
                break;
            case PlayerAnimationType.TRANSITION_STANCETOP_INTO_IDLE:
                animator.SetTrigger("CombatIntoTrans");
                break;
            case PlayerAnimationType.SLICE_FROM_ANY_STANCE:
                animator.SetTrigger("Slice");
                break;
            case PlayerAnimationType.THRUST_FROM_ANY_STANCE:
                animator.SetTrigger("Thrust");
                break;
            case PlayerAnimationType.FROM_ATTACK_TO_STANCE:
                animator.SetTrigger("BackToStance");
                break;
            case PlayerAnimationType.SLICE_SLICE_FROM_ANY_STANCE:
                animator.SetTrigger("Slice");
                break;
            case PlayerAnimationType.THRUST_SLICE_FROM_ANY_STANCE:
                animator.SetTrigger("Slice");
                break;
            case PlayerAnimationType.THRUST_THRUST_FROM_ANY_STANCE:
                animator.SetTrigger("Thrust");
                break;
            case PlayerAnimationType.SLICE_THRUST_FROM_ANY_STANCE:
                animator.SetTrigger("Thrust");
                break;
        }
    }
}
