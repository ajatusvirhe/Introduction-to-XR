using System;
using UnityEngine;

public class OldHand : MonoBehaviour
{
    Animator animator;
    private float trigTarget;
    private float gripTarget;
    private float currentGrip;
    private float currentTrigger;
    public float speed;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
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
        if (currentGrip != gripTarget)
        {
            currentGrip = Mathf.MoveTowards(currentGrip, gripTarget, Time.deltaTime * speed);
            animator.SetFloat("grip", currentGrip);
        }
        if (currentTrigger != trigTarget)
        {
            currentTrigger = Mathf.MoveTowards(currentTrigger, trigTarget, Time.deltaTime * speed);
            animator.SetFloat("trigger", currentTrigger);
        }

    }
}