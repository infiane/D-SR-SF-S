using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCaller : MonoBehaviour
{
    // doesn't need an explanation
    private AnimationController animController;

    // an attack that was before the current one
    private AttackType previousAttack;

    [SerializeField]
    [Tooltip("0 - top from stance to slice, 1 - top from slice to slice, 2 - top from thrust to slice\n" +
             "3 - middle from stance to slice, 4 - middle from slice to slice, 5 - middle from thrust to slice\n" +
             "6 - bottom from stance to slice, 7 - bottom from slice to slice, 8 - bottom from thrust to slice\n")]
    private float[] timeToActivateSliceTrigger;

    [SerializeField]
    [Tooltip("0 - top from stance to thrust, 1 - top from thrust to thrust, 2 - top from slice to thrust\n" +
             "3 - middle from stance to thrust, 4 - middle from thrust to thrust, 5 - middle from slice to thrust\n" +
             "6 - bottom from stance to thrust, 7 - bottom from thrust to thrust, 8 - bottom from slice to thrust\n")]
    private float[] timeToActivateThrustTrigger;

    // time in which amount the trigger will turn on
    private int triggerTimeIndex;
    [Space]

    // controlling either we have an option to combo
    private bool allowedToCombo;
    public bool allowedToAttack { get; private set; }
    
    [Space]
    [SerializeField]
    private TriggerBoxController sliceTrigger;
    [SerializeField]
    private TriggerBoxController thrustTrigger;

    #region PRIVATE-FUNCTIONS

    private void Awake()
    {
        animController = this.GetComponent<AnimationController>();
    }

    private void Start()
    {
        allowedToCombo = false;
        allowedToAttack = true;
        previousAttack = AttackType.NULL;
    }

    private int DefineTriggerTime(PlayerAnimationType animType, Stance playerStance)
    {
        switch (playerStance)
        {
            case Stance.TOP:
                switch(animType)
                {
                    case PlayerAnimationType.SLICE_FROM_ANY_STANCE:
                        return 0;
                    case PlayerAnimationType.THRUST_FROM_ANY_STANCE:
                        return 0;
                    case PlayerAnimationType.SLICE_SLICE_FROM_ANY_STANCE:
                        return 1;
                    case PlayerAnimationType.THRUST_THRUST_FROM_ANY_STANCE:
                        return 1;
                    case PlayerAnimationType.SLICE_THRUST_FROM_ANY_STANCE:
                        return 2;
                    case PlayerAnimationType.THRUST_SLICE_FROM_ANY_STANCE:
                        return 2;
                    default:
                        return -1;
                }
            case Stance.MIDDLE:
                switch (animType)
                {
                    case PlayerAnimationType.SLICE_FROM_ANY_STANCE:
                        return 3;
                    case PlayerAnimationType.THRUST_FROM_ANY_STANCE:
                        return 3;
                    case PlayerAnimationType.SLICE_SLICE_FROM_ANY_STANCE:
                        return 4;
                    case PlayerAnimationType.THRUST_THRUST_FROM_ANY_STANCE:
                        return 4;
                    case PlayerAnimationType.SLICE_THRUST_FROM_ANY_STANCE:
                        return 5;
                    case PlayerAnimationType.THRUST_SLICE_FROM_ANY_STANCE:
                        return 5;
                    default:
                        return -1;
                }
            case Stance.BOTTOM:
                switch (animType)
                {
                    case PlayerAnimationType.SLICE_FROM_ANY_STANCE:
                        return 6;
                    case PlayerAnimationType.THRUST_FROM_ANY_STANCE:
                        return 6;
                    case PlayerAnimationType.SLICE_SLICE_FROM_ANY_STANCE:
                        return 7;
                    case PlayerAnimationType.THRUST_THRUST_FROM_ANY_STANCE:
                        return 7;
                    case PlayerAnimationType.SLICE_THRUST_FROM_ANY_STANCE:
                        return 8;
                    case PlayerAnimationType.THRUST_SLICE_FROM_ANY_STANCE:
                        return 8;
                    default:
                        return -1;
                }
            default:
                return -1;
        }  
    }

    #endregion

    #region PUBLIIC-FUNCTIONS

    public void TurnOnComboTimeGap()
    {
        allowedToCombo = true;
        allowedToAttack = true;
    }

    public void TurnOffComboTimeGap()
    {
        allowedToCombo = false;
        allowedToAttack = true;
        animController.CallAnimationChange(PlayerAnimationType.FROM_ATTACK_TO_STANCE);
    }

    /*
        A function that makes a slice attack
    */
    public void Slice(Stance playerStance)
    {
        // if we fit in the gap for a combo and there was a previous attack
        if (allowedToCombo && previousAttack != AttackType.NULL)
        {
            allowedToCombo = false;
            StopAllCoroutines();

            if (previousAttack == AttackType.SLICE)
            {
                animController.CallAnimationChange(PlayerAnimationType.SLICE_SLICE_FROM_ANY_STANCE);

                switch (playerStance)
                {
                    case Stance.TOP:
                        triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.SLICE_SLICE_FROM_ANY_STANCE, Stance.TOP);
                        break;
                    case Stance.MIDDLE:
                        triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.SLICE_SLICE_FROM_ANY_STANCE, Stance.MIDDLE);
                        break;
                    case Stance.BOTTOM:
                        triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.SLICE_SLICE_FROM_ANY_STANCE, Stance.BOTTOM);
                        break;
                }
            }
            else if (previousAttack == AttackType.THRUST)
            {
                animController.CallAnimationChange(PlayerAnimationType.THRUST_SLICE_FROM_ANY_STANCE);

                switch (playerStance)
                {
                    case Stance.TOP:
                        triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.THRUST_SLICE_FROM_ANY_STANCE, Stance.TOP);
                        break;
                    case Stance.MIDDLE:
                        triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.THRUST_SLICE_FROM_ANY_STANCE, Stance.MIDDLE);
                        break;
                    case Stance.BOTTOM:
                        triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.THRUST_SLICE_FROM_ANY_STANCE, Stance.BOTTOM);
                        break;
                }
            }
        }
        else
        {
            animController.CallAnimationChange(PlayerAnimationType.SLICE_FROM_ANY_STANCE);

            switch (playerStance)
            {
                case Stance.TOP:
                    triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.SLICE_FROM_ANY_STANCE, Stance.TOP);
                    break;
                case Stance.MIDDLE:
                    triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.SLICE_FROM_ANY_STANCE, Stance.MIDDLE);
                    break;
                case Stance.BOTTOM:
                    triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.SLICE_FROM_ANY_STANCE, Stance.BOTTOM);
                    break;
            }
        }

        StartCoroutine(ActivateTriggerInTime(timeToActivateSliceTrigger[triggerTimeIndex], AttackType.SLICE));
        previousAttack = AttackType.SLICE;
        allowedToAttack = false;
        
    }

    /*
        A function that makes a thrust attack
    */
    public void Thrust(Stance playerStance)
    {
        // if we fit in the gap for a combo and there was a previous attack
        if (allowedToCombo && previousAttack != AttackType.NULL)
        {
            allowedToCombo = false;
            StopAllCoroutines();

            if (previousAttack == AttackType.SLICE)
            {
                animController.CallAnimationChange(PlayerAnimationType.SLICE_THRUST_FROM_ANY_STANCE);

                switch (playerStance)
                {
                    case Stance.TOP:
                        triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.SLICE_THRUST_FROM_ANY_STANCE, Stance.TOP);
                        break;
                    case Stance.MIDDLE:
                        triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.SLICE_THRUST_FROM_ANY_STANCE, Stance.MIDDLE);
                        break;
                    case Stance.BOTTOM:
                        triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.SLICE_THRUST_FROM_ANY_STANCE, Stance.BOTTOM);
                        break;
                }
            }
            else if (previousAttack == AttackType.THRUST)
            {
                animController.CallAnimationChange(PlayerAnimationType.THRUST_THRUST_FROM_ANY_STANCE);

                switch (playerStance)
                {
                    case Stance.TOP:
                        triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.THRUST_THRUST_FROM_ANY_STANCE, Stance.TOP);
                        break;
                    case Stance.MIDDLE:
                        triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.THRUST_THRUST_FROM_ANY_STANCE, Stance.MIDDLE);
                        break;
                    case Stance.BOTTOM:
                        triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.THRUST_THRUST_FROM_ANY_STANCE, Stance.BOTTOM);
                        break;
                }
            }
        }
        else
        {
            animController.CallAnimationChange(PlayerAnimationType.THRUST_FROM_ANY_STANCE);

            switch (playerStance)
            {
                case Stance.TOP:
                    triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.SLICE_FROM_ANY_STANCE, Stance.TOP);
                    break;
                case Stance.MIDDLE:
                    triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.SLICE_FROM_ANY_STANCE, Stance.MIDDLE);
                    break;
                case Stance.BOTTOM:
                    triggerTimeIndex = DefineTriggerTime(PlayerAnimationType.SLICE_FROM_ANY_STANCE, Stance.BOTTOM);
                    break;
            }
        }

        StartCoroutine(ActivateTriggerInTime(timeToActivateThrustTrigger[triggerTimeIndex], AttackType.THRUST));
        previousAttack = AttackType.THRUST;
        allowedToAttack = false;
    }

    #endregion

    #region COROUTINES

    private IEnumerator ActivateTriggerInTime(float time, AttackType attackType)
    {
        sliceTrigger.DeactivateTrigger();
        thrustTrigger.DeactivateTrigger();

        yield return new WaitForSeconds(time);

        if (attackType == AttackType.SLICE)
            sliceTrigger.ActivateTrigger();
        else if (attackType == AttackType.THRUST)
            thrustTrigger.ActivateTrigger();
    }

    #endregion
}
