using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSpawner : MonoBehaviour
{
    public int startingAmountOfWizards = 8;
    public float timeToNextSpawn = 10;
    public GameObject wizardPrefab;

    public float innerClock;

    void Start()
    {
        innerClock = 0;
        for (int i = 0; i < startingAmountOfWizards; i++)
        {
            Instantiate(wizardPrefab, transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        innerClock += Time.deltaTime;
        if (innerClock >= timeToNextSpawn)
        {
            innerClock = 0;
            Instantiate(wizardPrefab, transform.position, Quaternion.identity);
        }
    }
}
