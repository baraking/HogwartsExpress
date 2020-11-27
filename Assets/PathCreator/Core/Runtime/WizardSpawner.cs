using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSpawner : MonoBehaviour
{
    public int startingAmountOfWizards = 8;
    public float timeToNextSpawn = 10;
    public GameObject wizardPrefab;
    public GameObject station;

    public float innerClock;

    void Start()
    {
        innerClock = 0;
        for (int i = 0; i < startingAmountOfWizards; i++)
        {
            var newTarget = Instantiate(wizardPrefab, transform.position, Quaternion.identity);
            newTarget.transform.SetParent(transform.parent, true);
        }
    }

    void Update()
    {
        if (station.GetComponent<Station>().train == null)
        {
            innerClock += Time.deltaTime;
            if (innerClock >= timeToNextSpawn)
            {
                innerClock = 0;
                var newTarget = Instantiate(wizardPrefab, transform.position, Quaternion.identity);
                newTarget.transform.SetParent(transform.parent, false);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
