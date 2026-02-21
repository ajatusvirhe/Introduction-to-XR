using UnityEngine;

public class CanFallDetector : MonoBehaviour
{
    private Vector3 startPosition;
    public float fallDistanceThreshold = 0.2f;
    public bool hasFallen = false;

    void Start()
    {
        startPosition = transform.position;
    }

    public void CheckIfFallen()
    {
        if (!hasFallen)
        {
            float distance = Vector3.Distance(transform.position, startPosition);
            Debug.Log(gameObject.name + " distance : " + distance);

            if (distance > fallDistanceThreshold)
            {
                hasFallen = true;
                Debug.Log(gameObject.name + " hasFallen");
            }
        }
    }
}
