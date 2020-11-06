using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation1 : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void DoorOpenAnimation1()
    {
        print("play1");
        animator.Play("DoorOpenAnimation1");
    }

    public void DoorCloseAnimation1()
    {
        animator.Play("DoorCloseAnimation1");
    }
}
