using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public GameObject train;
    public List<GameObject> wizards;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.wizardTag)
        {
            wizards.Add(other.gameObject);
        }
        if (other.tag == Constants.fullTrain)
        {
            train = other.gameObject;
        }
        if(other.tag == Constants.trainCart)
        {
            train = other.gameObject.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Constants.wizardTag)
        {
            wizards.Remove(other.gameObject);
        }
        else if (other.tag == Constants.fullTrain || other.tag == Constants.trainCart)
        {
            train = null;
        }
    }
}
