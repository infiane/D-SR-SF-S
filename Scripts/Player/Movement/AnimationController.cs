﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMov;

    // A boolean that shows whether the character entered the combat mode or nah
    private bool playerInCombatMode;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        playerMov = this.GetComponent<PlayerMovement>();
        playerInCombatMode = false;
    }

    private void FixedUpdate()
    {
        if (playerMov.AllowedToMove)
        {
            AnimationChange();
        }
    }

    /*
        Gets input from the player and triggers animations from it
    */
    private void AnimationChange()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (playerInCombatMode)
            {

            }
            animator.SetTrigger(playerInCombatMode ? "CombatIntoTrans" : "TransIntoCombat");
            playerInCombatMode = !playerInCombatMode;
        }
    }
}
