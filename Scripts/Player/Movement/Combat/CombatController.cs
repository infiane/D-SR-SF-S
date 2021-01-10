using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    // a boolean that shows whether the player is in combat mode or nah
    public bool playerInCombatMode { get; private set; }

    private void Start()
    {
        playerInCombatMode = false;
    }

    /*
        
    */
    public void EnableStance()
    {
        playerInCombatMode = true;
    }

    /*
         
    */
    public void DisableStance()
    {
        playerInCombatMode = false;
    }
}
