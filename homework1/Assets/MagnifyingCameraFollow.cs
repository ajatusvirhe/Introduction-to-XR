using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Rendering;

public class MagnifyingCameraFollow : MonoBehaviour
    // MAde with chatGPT
{
    public Transform lensCenter;        // the lens center
    public Transform headCamera;  // XR Main Camera
    Quaternion lastRotation;

    private void Start()
    {
        lastRotation = lensCenter.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        // Follow lens position
        transform.position = lensCenter.position;

        // Get head forward direction
        //Vector3 forward = headCamera.forward;

        transform.rotation = /*Quaternion.Inverse(lastRotation)*/ lastRotation * headCamera.rotation; //transform.rot??
        // Look straight where the player looks
        //transform.rotation = headCamera.rotation;
        // Keep camera upright (no roll)
        //transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        lastRotation = lensCenter.rotation;
    }
}
