using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    public GameObject ui;
    public int height;

    void Update()
    {
        height = Mathf.Clamp(height, Constants.MapMinHeight, Constants.MapMaxHeight);
        transform.position = new Vector3(transform.position.x,height, transform.position.z);

        if (Input.GetKeyDown(KeyCode.M))
        {
            ui.SetActive(!ui.active);
            print("Map toggled");
        }
    }
}
