using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void DoorOpenAnimation()
    {
        print("play");
        animator.Play("DoorOpenAnimation");
    }

    public void DoorCloseAnimation()
    {
        animator.Play("DoorCloseAnimation");
    }

    public void OnTriggerEnter(Collider other)
    {
        print("play");
        animator.Play("DoorOpenAnimation");
    }

    public void OnTriggerExit(Collider other)
    {
        print("played");
        animator.Play("DoorCloseAnimation");
    }
}
