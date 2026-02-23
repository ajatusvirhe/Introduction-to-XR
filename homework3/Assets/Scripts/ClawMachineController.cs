using UnityEngine;

public class ClawMachineController : MonoBehaviour
{
    public Transform clawRig;

    public float moveSpeed = 0.05f;
    public float maxX = 0.00587f;
    public float minX = -0.00214f;

    private Vector3 startPosition;
    private float currentInput = 0f;

    public Animator clawAnimator;

    public float dropDistance = 2f;
    public float dropSpeed = 2f;

    public Transform dropPoint;

    private bool isDropping = false;
    private GameObject grabbedPrize;


    void Start()
    {
        startPosition = clawRig.localPosition;
    }

    public void MoveHorizontal(float input)
    {
        currentInput = input;  // store lever input (-1 to 1)
    }

    void Update()
    {
        if (Mathf.Abs(currentInput) > 0.05f)
        {
            Vector3 pos = clawRig.localPosition;

            pos.x += currentInput * moveSpeed * Time.deltaTime;

            pos.x = Mathf.Clamp(pos.x, minX, maxX);

            clawRig.localPosition = pos;
        }
    }
}