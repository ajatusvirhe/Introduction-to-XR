using UnityEngine;
using UnityEngine.InputSystem;

public class StringManualGrab : MonoBehaviour
{
    private Vector3 startLocalPosition;

    public float maxPullDistance = 0.4f;
    public float pullThreshold = 0.3f;

    private Transform grabbingHand;
    private float handStartY;

    //public InputActionReference leftGrip;
    //public InputActionReference rightGrip;
    public InputActionProperty leftGrip;
    public InputActionProperty rightGrip;

    private bool handInTrigger = false;
    private Transform handInside;

    public bool hasBeenPulled = false;
    public PullBoothManager boothManager;

    void Start()
    {
        startLocalPosition = transform.localPosition;

        leftGrip.action.Enable();
        rightGrip.action.Enable();
    }

    void Update()
    {
        /*if (hasBeenPulled)
        {
            //Release();
            return;
        }*/
            

        // Start grab
        if (handInTrigger && grabbingHand == null)
        {
            if (leftGrip.action.IsPressed() || rightGrip.action.IsPressed())
            {
                grabbingHand = handInside;
                handStartY = grabbingHand.position.y;
            }
        }

        // While grabbing
        if (grabbingHand != null)
        {
            bool gripStillHeld =
                leftGrip.action.IsPressed() ||
                rightGrip.action.IsPressed();

            if (!gripStillHeld)
            {
                Release();
                return;
            }

            float handDelta = handStartY - grabbingHand.position.y;
            float clampedPull = Mathf.Clamp(handDelta, 0f, maxPullDistance);

            transform.localPosition =
                startLocalPosition - new Vector3(0, clampedPull, 0);

            if (clampedPull >= pullThreshold)
            {
                Debug.Log("calling booth manager frm string");
                hasBeenPulled = true;
                boothManager.StringSelected(this);
                Release();
                return;
            }
        }
    }

    void Release()
    {
        grabbingHand = null;
        transform.localPosition = startLocalPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Hand"))
        if (other.GetComponentInParent<Hand>() != null)
        {
            handInTrigger = true;
            handInside = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            handInTrigger = false;
            handInside = null;
        }
    }
}
