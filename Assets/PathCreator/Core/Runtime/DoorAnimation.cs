using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] public bool operDoors;

    void Start()
    {
        animator = GetComponent<Animator>();
        operDoors = false;
    }

    public void DoorOpenAnimation()
    {
        operDoors = true;
        animator.Play("DoorOpenAnimation");
    }

    public void DoorCloseAnimation()
    {
        operDoors = false;
        animator.Play("DoorCloseAnimation");
    }
}
