using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleControl : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerMovement>().AllowMovement();
    }
}
