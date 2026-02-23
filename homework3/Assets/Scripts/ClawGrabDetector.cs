using UnityEngine;

public class ClawGrabDetector : MonoBehaviour
{
    public ClawMachineController machine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prize"))
        {
            machine.SetPotentialPrize(other.gameObject);
        }
    }
}