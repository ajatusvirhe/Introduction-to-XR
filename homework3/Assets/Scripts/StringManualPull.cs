using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StringManualPull : MonoBehaviour
{
    private XRGrabInteractable grab;

    private Vector3 startLocalPosition;
    private float minPullDistance = 0f;
    public float maxPullDistance = 0.4f;
    public float pullThreshold = 0.3f;

    private Transform grabbingHand;
    private float handStartY;

    public bool hasBeenPulled = false;
    public PullBoothManager boothManager;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        startLocalPosition = transform.localPosition;

        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        grabbingHand = args.interactorObject.transform;
        Debug.Log("Grabbinghand: " + grabbingHand.name);
        handStartY = grabbingHand.position.y;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        grabbingHand = null;

        // Snap back to start
        transform.localPosition = startLocalPosition;
    }

    void Update()
    {
        if (grabbingHand == null || hasBeenPulled)
            return;

        float handDelta = handStartY - grabbingHand.position.y;

        float clampedPull = Mathf.Clamp(handDelta, minPullDistance, maxPullDistance);

        transform.localPosition = startLocalPosition - new Vector3(0, clampedPull, 0);

        if (clampedPull >= pullThreshold)
        {
            hasBeenPulled = true;
            boothManager.StringSelected(this);
        }
    }
}
