using System.Collections;
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

    public float dropDistance=0.01f;
    public float dropSpeed=0.01f;

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

    public void SetPotentialPrize(GameObject prize)
    {
        if (grabbedPrize == null)
            grabbedPrize = prize;
    }

    public void StartDropSequence()
    {
        if (!isDropping)
            StartCoroutine(DropSequence());
    }

    private IEnumerator DropSequence()
    {
        isDropping = true;

        Vector3 startPos = clawRig.localPosition;
        //Vector3 downPos = startPos - new Vector3(0, dropDistance, 0);
        Vector3 downPos = startPos - new Vector3(0, 0, dropDistance); //dropdistancem0.02 here y is z?

        // Move down
        while (Vector3.Distance(clawRig.localPosition, downPos) > 0.01f)
        {
            clawRig.localPosition = Vector3.MoveTowards(
                clawRig.localPosition,
                downPos,
                dropSpeed * Time.deltaTime
            );

            yield return null;
        }

        // Open claw
        clawAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(0.8f);

        // Close claw
        clawAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);

        // Attach prize if detected
        if (grabbedPrize != null)
        {
            Debug.Log("Grabbed a prize!");
            grabbedPrize.transform.SetParent(clawRig);
            grabbedPrize.transform.localPosition = Vector3.zero;
        }

        // Move up
        while (Vector3.Distance(clawRig.localPosition, startPos) > 0.01f)
        {
            clawRig.localPosition = Vector3.MoveTowards(
                clawRig.localPosition,
                startPos,
                dropSpeed * Time.deltaTime
            );

            yield return null;
        }

        yield return new WaitForSeconds(0.8f);
        // Move to drop point
        while (Vector3.Distance(clawRig.position, dropPoint.position) > 0.01f)
        {
            clawRig.position = Vector3.MoveTowards(
                clawRig.position,
                dropPoint.position,
                dropSpeed * Time.deltaTime
            );

            yield return null;
        }

        // Release prize
        if (grabbedPrize != null)
        {
            grabbedPrize.transform.SetParent(null);
            grabbedPrize = null;
        }

        yield return new WaitForSeconds(0.5f);

        isDropping = false;
    }

}