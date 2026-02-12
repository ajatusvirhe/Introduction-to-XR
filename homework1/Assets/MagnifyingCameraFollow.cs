using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Rendering;

public class MagnifyingCameraFollow : MonoBehaviour
    // MAde with chatGPT partly
{
    public Transform lensCenter;  // the lens center
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

        //compensate for the magnifying glass rotating, so image does not rotate with it but stays 'static'
        // compared to just transform.rotation = headCamera.rotation.
        // could not find another way to keep images upward direction actually up, because it transform along with the magnifýing glass
        // this produces other kinds of problems
        //transform.rotation = /*Quaternion.Inverse(lastRotation)*/ lastRotation * headCamera.rotation; 

        // Look straight where the player looks
        transform.rotation = headCamera.rotation;
        // Keep camera upright (no roll)
        //transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        lastRotation = lensCenter.rotation;
    }
}
