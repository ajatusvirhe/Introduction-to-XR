using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
    //animation variables
    Animator animator;
    private float trigTarget;
    private float gripTarget;
    private float currentGrip;
    private float currentTrigger;
    public float animationspeed;

    //for physics
    public GameObject followobject;
    private float followspeed = 40f;
    private float rotatespeed = 100f;
    public Vector3 posoffset;
    public Vector3 rotoffset;
    private Transform followtarget;
    private Rigidbody body;

    void Start()
    {
        animator = GetComponent<Animator>();

        followtarget = followobject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;

        // teleport jands
        body.position = followtarget.position;
        body.rotation = followtarget.rotation;
    }

    void Update()
    {
        AnimateHand();
        PhysicsMove();
    }

    private void PhysicsMove()
    {
        //position
        var positionWithOffset = followtarget.position + posoffset;
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        body.linearVelocity = (positionWithOffset - transform.position).normalized * followspeed * distance; // body.velocity?
        //rotation
        var rotaWithOffset = followtarget.rotation * Quaternion.Euler(rotoffset);
        var quart = rotaWithOffset * Quaternion.Inverse(body.rotation);
        quart.ToAngleAxis(out float angle, out Vector3 axis);
        body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotatespeed);
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        trigTarget = v;
    }

    void AnimateHand()
    {
        if(currentGrip != gripTarget)
        {
            currentGrip = Mathf.MoveTowards(currentGrip, gripTarget, Time.deltaTime * animationspeed);
            animator.SetFloat("grip",currentGrip);
        }
        if(currentTrigger != trigTarget)
        {
            currentTrigger = Mathf.MoveTowards(currentTrigger, trigTarget, Time.deltaTime * animationspeed);
            animator.SetFloat("trigger",currentTrigger);
        }

    }
}
