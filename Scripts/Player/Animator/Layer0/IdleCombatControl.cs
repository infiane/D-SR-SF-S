using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCombatControl : StateMachineBehaviour
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerMovement>().BlockMovement();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerMovement>().AllowMovement();
    }
}
