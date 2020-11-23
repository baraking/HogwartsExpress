using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardModel : MonoBehaviour
{

    public GameObject hat;
    public GameObject scarf;
    public GameObject head;
    public GameObject robeModel;
    public GameObject hatPoint;

    public Material[] skins;
    public Material[] robes;
    public Material[] colors;

    void Start()
    {
        int clothColor = Random.Range(0, colors.Length);
        hat.GetComponent<Renderer>().material = colors[clothColor];
        scarf.GetComponent<Renderer>().material = colors[clothColor];

        int skinColor = Random.Range(0, skins.Length);
        head.GetComponent<Renderer>().material = skins[skinColor];

        int robeColor = Random.Range(0, robes.Length);
        robeModel.GetComponent<Renderer>().material = robes[clothColor];
        hatPoint.GetComponent<Renderer>().material = robes[clothColor];
    }

    void Update()
    {
        
    }
}
