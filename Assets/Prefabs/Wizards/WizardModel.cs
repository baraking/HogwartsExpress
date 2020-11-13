using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardModel : MonoBehaviour
{

    public GameObject hat;
    public GameObject scarf;
    public GameObject head;

    public Material[] skins;
    public Material[] colors;

    void Start()
    {
        int clothColor = Random.Range(0, colors.Length);
        hat.GetComponent<Renderer>().material = colors[clothColor];
        scarf.GetComponent<Renderer>().material = colors[clothColor];

        int skinColor = Random.Range(0, skins.Length);
        head.GetComponent<Renderer>().material = skins[skinColor];
    }

    void Update()
    {
        
    }
}
