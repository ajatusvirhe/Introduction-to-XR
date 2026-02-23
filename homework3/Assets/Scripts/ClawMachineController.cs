using System.Collections;
using System.Drawing;
using UnityEngine;

public class ClawMachineController : MonoBehaviour
{
    public Transform clawRig;

    public float moveSpeed = 0.05f;
    public float maxX = 0.00587f;
    public float minX = -0.00214f;

    private Vector3 startPosition;
    //private Transform startTransform;
    private float currentInput = 0f;

    public Animator clawAnimator;

    public float dropDistance;
    public float dropSpeed;

    public Transform dropPoint;
    public Transform teleportPrize;
    public Transform teleportTowards;

    private bool isDropping = false;
    private GameObject grabbedPrize;


    void Start()
    {
        startPosition = clawRig.localPosition;
        //startTransform = clawRig.transform;
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
            GetComponent<AudioSource>().Play();
            StartCoroutine(DropSequence());
    }

    private IEnumerator DropSequence()
    {
        isDropping = true;

        Vector3 startPos = clawRig.localPosition;
        //Vector3 downPos = startPos - new Vector3(0, dropDistance, 0);
        Vector3 downPos = startPos - new Vector3(0, 0, dropDistance); //dropdistancem0.02 here y is z?


        // Open claw
        clawAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(0.7f);


        // Move down
        while (Vector3.Distance(clawRig.localPosition, downPos) > 0.01f)
        {
            clawRig.localPosition = Vector3.MoveTowards(
                clawRig.localPosition,
                downPos,
                (dropSpeed * Time.deltaTime)/2
            );

            yield return null;
        }

        // Close claw
        clawAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(0.7f);

        // Attach prize if detected
        if (grabbedPrize != null)
        {
            Debug.Log("Grabbed a prize!");
            grabbedPrize.transform.SetParent(clawRig);
            grabbedPrize.transform.localPosition = Vector3.zero;
        }

        // Move up
        while (Vector3.Distance(clawRig.localPosition, startPos) > 0.001f)
        {
            clawRig.localPosition = Vector3.MoveTowards(
                clawRig.localPosition,
                startPos,
                (dropSpeed * Time.deltaTime)/2
            );
            Debug.Log("Moved up at " + Time.time);
            yield return null;
        }

        yield return new WaitForSeconds(0.8f);
        // Move to drop point
        while (Vector3.Distance(clawRig.position, dropPoint.position) > 0.001f)
        {
            clawRig.position = Vector3.MoveTowards(
                clawRig.position,
                dropPoint.position,
                dropSpeed * Time.deltaTime
            );
            yield return null;
        }
        Debug.Log("Moved to drop point at " + Time.time);

        // Release prize
        if (grabbedPrize != null)
        {
            Debug.Log("Releasing prize at " + Time.time);
            grabbedPrize.transform.SetParent(null);
            while (Vector3.Distance(grabbedPrize.transform.position, teleportTowards.position) > 0.01f)
            {
                grabbedPrize.transform.position = Vector3.MoveTowards(
                    grabbedPrize.transform.position,
                    teleportTowards.position,
                    dropSpeed*6 * Time.deltaTime
                );
                yield return null;
            }

            grabbedPrize.transform.position = teleportPrize.position;
            grabbedPrize.GetComponent<Rigidbody>().useGravity = true;
            Collider prizecollider = grabbedPrize.GetComponent<Collider>();
            if (prizecollider != null)
            {
                prizecollider.enabled = true; // enables the collider only after teleporting
                prizecollider.providesContacts = true;
                prizecollider.isTrigger = false;
            }
            else
            {
                prizecollider = grabbedPrize.GetComponentInChildren<Collider>();
                prizecollider.enabled = true;
                prizecollider.providesContacts = true;
                prizecollider.isTrigger = false;
            }
            grabbedPrize = null;
        }

        yield return new WaitForSeconds(0.8f);

        isDropping = false;
        clawRig.localPosition = startPosition;
    }

}