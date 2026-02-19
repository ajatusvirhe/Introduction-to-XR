using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    public InputActionProperty gripAction;
    public InputActionProperty triggerAction;
    //public InputActionReference actionGrip;
    //public InputActionReference actionTrig;
    public Hand hand;

    void OnEnable()
    {
        gripAction.action.Enable();
        triggerAction.action.Enable();
    }

    void OnDisable()
    {
        gripAction.action.Disable();
        triggerAction.action.Disable();
    }
    void Update()
    {
        hand.SetGrip(gripAction.action.ReadValue<float>());
        hand.SetTrigger(triggerAction.action.ReadValue<float>());
    }
}
