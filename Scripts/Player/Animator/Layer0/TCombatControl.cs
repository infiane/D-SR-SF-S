using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCombatControl : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerMovement>().BlockMovement();
    }
}
