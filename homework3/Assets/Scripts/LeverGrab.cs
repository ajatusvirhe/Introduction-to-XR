using UnityEngine;
using UnityEngine.InputSystem;

public class LeverGrab : MonoBehaviour
{
    //public InputActionReference leftGrip;
    //public InputActionReference rightGrip;
    public InputActionProperty leftGrip;
    public InputActionProperty rightGrip;

    public float maxRotation = 30f;      // degrees left/right
    public float rotationSpeed = 150f;
    //public Transform claw;

    private bool handInTrigger = false;
    private Transform grabbingHand;
    private float handStartX;
    private float leverStartRotation;
    private Transform handInside;

    public ClawMachineController clawController;

    public float shiftThreshold = 0.1f;
    private Vector3 startLocalPosition;

    void Start()
    {
        //startLocalPosition = transform.localPosition;

        leftGrip.action.Enable();
        rightGrip.action.Enable();
    }

    void Update()
    {
        // Start grabbing
        if (handInTrigger && grabbingHand == null)
        {
            if (leftGrip.action.IsPressed() || rightGrip.action.IsPressed())
            {
                grabbingHand = handInside;
                handStartX = grabbingHand.position.x;
                leverStartRotation = transform.localEulerAngles.z;
            }
        }

        // While grabbing
        if (grabbingHand != null)
        {
            bool gripHeld =
                leftGrip.action.IsPressed() ||
                rightGrip.action.IsPressed();

            if (!gripHeld)
            {
                grabbingHand = null;
                return;
            }

            float deltaX = grabbingHand.position.x - handStartX;

            float targetRotation =
                leverStartRotation - deltaX * rotationSpeed;

            targetRotation = Mathf.Clamp(
                NormalizeAngle(targetRotation),
                -maxRotation,
                maxRotation
            );

            transform.localRotation =
                Quaternion.Euler(0, 0, targetRotation);

            // Send normalized value (-1 to 1) to claw
            float normalized = targetRotation / maxRotation;
            clawController.MoveHorizontal(normalized);
        }
    }

    //private Transform handInside;

    private void OnTriggerEnter(Collider other)
    {
        Hand hand = other.GetComponentInParent<Hand>();
        if (hand != null)
        {
            handInTrigger = true;
            handInside = hand.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Hand hand = other.GetComponentInParent<Hand>();
        if (hand != null)
        {
            handInTrigger = false;
        }
    }

    float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
