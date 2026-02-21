using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StringPull : MonoBehaviour
{
    private Vector3 startLocalPosition;
    public float pullThreshold = 0.3f;

    public bool hasBeenPulled = false;

    public PullBoothManager boothManager;

    void Start()
    {
        startLocalPosition = transform.localPosition;
    }

    void Update()
    {
        if (hasBeenPulled) return;

        float pulledDistance = startLocalPosition.y - transform.localPosition.y;

        if (pulledDistance > pullThreshold)
        {
            hasBeenPulled = true;
            //boothManager.StringSelected(this);
            transform.localPosition = startLocalPosition; // move back 
        }
    }
}
