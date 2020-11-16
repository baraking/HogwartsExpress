using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAnimation : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] public bool operDoors;

    void Start()
    {
        animator = GetComponent<Animator>();
        operDoors = false;
    }

    public void DrawStepAnimation()
    {
        operDoors = true;
        animator.Play("DrawStep");
    }

    public void WithdrawStepAnimation()
    {
        operDoors = false;
        animator.Play("WithdrawStep");
    }
}
