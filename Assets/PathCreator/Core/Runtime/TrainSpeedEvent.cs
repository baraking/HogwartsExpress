using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpeedEvent : MonoBehaviour
{
    public float speed;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.fullTrain)
        {
            
        }
    }
}
