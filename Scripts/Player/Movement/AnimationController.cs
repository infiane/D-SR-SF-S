using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationType { TRANSITION_IDLE_INTO_COMBATSTANCE,
                                  TRANSITION_COMBATSTANCE_INTO_IDLE }

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
            case PlayerAnimationType.TRANSITION_IDLE_INTO_COMBATSTANCE:
                animator.SetTrigger("TransIntoCombat");
                break;
            case PlayerAnimationType.TRANSITION_COMBATSTANCE_INTO_IDLE:
                animator.SetTrigger("CombatIntoTrans");
                break;
        }
    }
}
