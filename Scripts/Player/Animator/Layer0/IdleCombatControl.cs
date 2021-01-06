using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCombatControl : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerMovement>().BlockMovement();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerMovement>().AllowMovement();
    }
}
