using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public GameObject train;
    public List<GameObject> wizards;

    public GameObject targetPrefab;
    public GameObject stationTargets;
    public GameObject trainTargets;

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

                foreach (GameObject wizard in wizards)
                {
                    int thisWizardChanceToBoardTrain = Random.Range(1, 101);
                    if (thisWizardChanceToBoardTrain <= Constants.chanceToBoardTrain)
                    {
                        wizard.GetComponent<WizardMovement>().mode = WizardMovement.Mode.FollowParent;
                        wizard.GetComponent<WizardMovement>().possibleTargets = trainTargets;
                        wizard.GetComponent<WizardMovement>().FindNewTarget();
                    }
                }
            }
            else
            {
                if (!train.GetComponent<PathCreation.PathFollower>().openDoors)
                {
                    spawnedTargets = false;
                    for (int i = 0; i < trainTargets.transform.childCount; i++) 
                    { 
                        GameObject.Destroy(trainTargets.transform.GetChild(0).gameObject);
                    }

                    foreach (GameObject wizard in wizards)
                    {
                        //wizard.GetComponent<WizardMovement>().mode = WizardMovement.Mode.SearchTarget;
                        //wizard.GetComponent<WizardMovement>().possibleTargets = stationTargets;
                    }
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
                var newTarget1 = Instantiate(targetPrefab, child.gameObject.transform.position + Constants.cartOffset1, Quaternion.identity);
                var newTarget2 = Instantiate(targetPrefab, child.gameObject.transform.position + Constants.cartOffset2, Quaternion.identity);

                newTarget1.transform.parent = trainTargets.transform;
                newTarget2.transform.parent = trainTargets.transform;

                newTarget1.GetComponent<target>().parentHolder = child.gameObject;
                newTarget2.GetComponent<target>().parentHolder = child.gameObject;
            }
        }
    }
}
