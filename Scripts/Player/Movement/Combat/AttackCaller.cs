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

    [SerializeField]
    private TriggerBoxController sliceTrigger;
    [SerializeField]
    private TriggerBoxController thrustTrigger;

    private void Awake()
    {
        animController = this.GetComponent<AnimationController>();
    }

    public void Slice(Stance playerStance)
    {
        StartCoroutine(ActivateTriggerInTime(timeToActivateSliceTrigger[(int)playerStance], AttackType.SLICE));
        animController.CallAnimationChange(PlayerAnimationType.SLICE_FROM_ANY_STANCE);
    }

    public void Thrust(Stance playerStance)
    {
        StartCoroutine(ActivateTriggerInTime(timeToActivateThrustTrigger[(int)playerStance], AttackType.THRUST));
        animController.CallAnimationChange(PlayerAnimationType.THRUST_FROM_ANY_STANCE);
    }

    IEnumerator ActivateTriggerInTime(float time, AttackType attackType)
    {
        sliceTrigger.DeactivateTrigger();
        thrustTrigger.DeactivateTrigger();

        yield return new WaitForSeconds(time);

        if (attackType == AttackType.SLICE)
            sliceTrigger.ActivateTrigger();
        else if (attackType == AttackType.THRUST)
            thrustTrigger.ActivateTrigger();
    }
}
