using UnityEngine;

public class MagnifyingCameraFollow : MonoBehaviour
    // MAde with chatGPT
{
    public Transform lensCenter;        // the lens center
    public Transform headCamera;  // XR Main Camera

    // Update is called once per frame
    void Update()
    {
        // Follow lens position
        transform.position = lensCenter.position;

        // Get head forward direction
        Vector3 forward = headCamera.forward;

        // Look straight where the player looks
        //transform.rotation = headCamera.rotation;
        // Keep camera upright (no roll)
        transform.position += forward * 0.01f;
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        
    }
}
