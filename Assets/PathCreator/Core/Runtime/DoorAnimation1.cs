using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation1 : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] public bool operDoors;

    void Start()
    {
        animator = GetComponent<Animator>();
        operDoors = false;
    }

    public void DoorOpenAnimation1()
    {
        operDoors = true;
        animator.Play("DoorOpenAnimation1");
    }

    public void DoorCloseAnimation1()
    {
        operDoors = false;
        animator.Play("DoorCloseAnimation1");
    }
}
