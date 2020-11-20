using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public GameObject train;
    public List<GameObject> wizards;

    public GameObject targetPrefab;
    public GameObject targets;

    private bool spawnedTargets;

    private void Start()
    {
        spawnedTargets = false;
    }

    private void Update()
    {
        if (train != null)
        {
            if (!spawnedTargets && train.GetComponent<PathCreation.PathFollower>().openDoors)
            {
                PutTargetsOnCarts();
                spawnedTargets = true;
            }
            else
            {
                if (!train.GetComponent<PathCreation.PathFollower>().openDoors)
                {
                    spawnedTargets = false;
                    //delete them
                }
            }
        }
    }

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

    public void PutTargetsOnCarts()
    {
        foreach (Transform child in train.transform)
        {
            if (child.gameObject.CompareTag(Constants.trainCart))
            {
                Instantiate(targetPrefab, child.gameObject.transform.position + Constants.cartOffset1, Quaternion.identity);
                Instantiate(targetPrefab, child.gameObject.transform.position + Constants.cartOffset2, Quaternion.identity);
            }
        }
    }
}
