using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Vector3 rotationax = Vector3.up; // Y axis
    public float rotationSpeed = 10f; // degrees per second

    void Update()
    {
        transform.Rotate(rotationax * rotationSpeed * Time.deltaTime);
    }
}
