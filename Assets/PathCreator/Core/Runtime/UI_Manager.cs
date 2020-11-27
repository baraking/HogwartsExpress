using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject mapCamera;
    public Slider heightSlider;
    public GameObject train;

    public Text numberOfPassengers;

    void Start()
    {
        heightSlider.maxValue = Constants.MapMaxHeight;
        heightSlider.minValue = Constants.MapMinHeight;
    }

    void Update()
    {
        numberOfPassengers.text = train.GetComponent<PathCreation.PathFollower>().numberOfPassengers.ToString();
        mapCamera.GetComponent<Map>().height = heightSlider.value;
    }
}
