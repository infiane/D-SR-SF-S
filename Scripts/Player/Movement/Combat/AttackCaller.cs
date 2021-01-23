using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCaller : MonoBehaviour
{
    // doesn't need an explanation
    private AnimationController animController;

    [SerializeField]
    [Header("0 - top, 1 - middle, 2 - bottom")]
    private float[] timeToActivateSliceTrigger;
    [SerializeField]
    private float[] timeToActivateThrustTrigger;
    [Space]

    [Header("3 - TThrust, 4 - MThrust, 5 - BThrust")]
    [Header("0 - TSlice, 1 - MSlice, 2 - BSlice")]
    [Header("Time given to make a combo after an attack")]
    
    
    [SerializeField]
    private float[] comboTimeGap;

    // controlling either we have an option to combo
    private bool allowedToCombo;
    
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
    }

    #endregion

    #region PUBLIIC-FUNCTIONS

    public void Slice(Stance playerStance)
    {
        if (allowedToCombo)
            StopAllCoroutines();

        StartCoroutine(ActivateTriggerInTime(timeToActivateSliceTrigger[(int)playerStance], AttackType.SLICE));
        StartCoroutine(GiveTimeToCombo(comboTimeGap[(int)playerStance]));

        animController.CallAnimationChange(PlayerAnimationType.SLICE_FROM_ANY_STANCE);

        allowedToCombo = true;
    }

    public void Thrust(Stance playerStance)
    {
        if (allowedToCombo)
            StopAllCoroutines();

        StartCoroutine(ActivateTriggerInTime(timeToActivateThrustTrigger[(int)playerStance], AttackType.THRUST));
        StartCoroutine(GiveTimeToCombo(comboTimeGap[3 + (int)playerStance]));

        animController.CallAnimationChange(PlayerAnimationType.THRUST_FROM_ANY_STANCE);

        allowedToCombo = true;
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

    // after an attack has ended a player has a little time gap to continue it to make a combo. if he doesnt continue, we are going back to stance
    private IEnumerator GiveTimeToCombo(float time)
    {
        yield return new WaitForSeconds(time);
        allowedToCombo = false;
        animController.CallAnimationChange(PlayerAnimationType.FROM_ATTACK_TO_STANCE);
    }

    #endregion
}
