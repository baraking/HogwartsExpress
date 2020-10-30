using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpeedEvent : MonoBehaviour
{
    public enum SpeedEventMode { set, accelerate, stop };

    public SpeedEventMode mode;
    public float speed;

    void OnDrawGizmos()
    {
        if(mode== SpeedEventMode.set)
        {
            Gizmos.color = Color.green;
        }
        else if (mode == SpeedEventMode.stop)
        {
            Gizmos.color = Color.red;
        }
        else if (mode == SpeedEventMode.accelerate)
        {
            Gizmos.color = Color.blue;
        }

        Gizmos.DrawSphere(transform.position, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.trainCart || other.tag == Constants.fullTrain)
        {
            if (mode == SpeedEventMode.set)
            {
                if (other.gameObject.transform.parent.tag == Constants.fullTrain)
                {
                    other.gameObject.transform.parent.GetComponent<PathCreation.PathFollower>().TrainSetSpeed(speed);
                }
                else if (other.tag == Constants.fullTrain)
                {
                    other.GetComponent<PathCreation.PathFollower>().TrainSetSpeed(speed);
                }
            }
            else if (mode == SpeedEventMode.stop)
            {
                if (other.gameObject.transform.parent.tag == Constants.fullTrain)
                {
                    other.gameObject.transform.parent.GetComponent<PathCreation.PathFollower>().TrainStop();
                    StartCoroutine(StopTrain(other.gameObject.transform.parent,Constants.TrainStopWaitTime));
                }
                else if (other.tag == Constants.fullTrain)
                {
                    other.GetComponent<PathCreation.PathFollower>().TrainStop();
                    StartCoroutine(StopTrain(other.gameObject.transform, Constants.TrainStopWaitTime));
                }
            }
            else if (mode == SpeedEventMode.accelerate)
            {
                if (other.gameObject.transform.parent.tag == Constants.fullTrain)
                {
                    other.gameObject.transform.parent.GetComponent<PathCreation.PathFollower>().TrainAccelerate(speed);
                }
                else if (other.tag == Constants.fullTrain)
                {
                    other.GetComponent<PathCreation.PathFollower>().TrainAccelerate(speed);
                }
            }
        }
    }

    public IEnumerator StopTrain(Transform transform, int time)
    {
        print("Start a new");
        yield return new WaitForSeconds(time);
        transform.GetComponent<PathCreation.PathFollower>().TrainAccelerate(speed);
        StopAllCoroutines();
        yield break;
    }
}
