using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoxController : MonoBehaviour
{
    private bool activated;

    private void Start()
    {
        DeactivateTrigger();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
        {
            // TODO: if i hit an enemy hitbox then i'll call enemy's DefenseCaller and ours CombatController, indicating that an attack was succesful
        }
    }

    public void ActivateTrigger()
    {
        activated = true;
    }

    public void DeactivateTrigger()
    {
        activated = false;
    }
}
