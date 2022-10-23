using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Here is where the function gets ran to check the trigger once per frame
    void Update()
    {
        // Since this is a dynamic value we use float for the type, for a button we might use <bool> 
        // Trigger listener
        float triggerVal = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerVal);

        // Grip listener
        float gripVal = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripVal);
    }
}
