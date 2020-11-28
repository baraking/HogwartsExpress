using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GameObject mapCamera;
    public Slider heightSlider;
    public Slider speedSlider;
    public GameObject train;

    public Text numberOfPassengers;
    public Text trainSpeedText;

    void Start()
    {
        heightSlider.maxValue = Constants.MapMaxHeight;
        heightSlider.minValue = Constants.MapMinHeight;

        speedSlider.maxValue = Constants.maxSpeed;
        speedSlider.minValue = Constants.minSpeed;

        speedSlider.wholeNumbers = true;
    }

    void Update()
    {
        numberOfPassengers.text = train.GetComponent<PathCreation.PathFollower>().numberOfPassengers.ToString();
        trainSpeedText.text = train.GetComponent<PathCreation.PathFollower>().targetSpeed.ToString();
        heightSlider.onValueChanged.AddListener(delegate { UpdateHeight(); });
        speedSlider.onValueChanged.AddListener(delegate { UpdateSpeed(); });
        UpdateHeight();
        UpdateSpeed();
    }

    void UpdateHeight()
    {
        mapCamera.GetComponent<Map>().height = heightSlider.value;
    }

    void UpdateSpeed()
    {
        train.GetComponent<PathCreation.PathFollower>().targetSpeed = speedSlider.value;
    }
}
