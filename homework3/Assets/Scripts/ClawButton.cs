using UnityEngine;

public class ClawButton : MonoBehaviour
{
    public ClawMachineController machine;

    private void OnTriggerEnter(Collider other)
    {
        Hand hand = other.GetComponentInParent<Hand>();
        if (hand != null)
        {
            machine.StartDropSequence();
        }
    }
}
