using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject mapCamera;
    public GameObject trainCamera;
    public Slider heightSlider;
    public Slider horizonSlider;
    public Slider speedSlider;
    public GameObject train;

    public Text numberOfPassengers;
    public Text trainSpeedText;

    void Start()
    {
        heightSlider.maxValue = Constants.MapMaxHeight;
        heightSlider.minValue = Constants.MapMinHeight;

        horizonSlider.maxValue = Constants.TrainCameraRightMax;
        horizonSlider.minValue = Constants.TrainCameraLeftMax;

        speedSlider.maxValue = Constants.maxSpeed;
        speedSlider.minValue = Constants.minSpeed;

        speedSlider.wholeNumbers = true;
    }

    void Update()
    {
        numberOfPassengers.text = train.GetComponent<PathCreation.PathFollower>().numberOfPassengers.ToString();
        trainSpeedText.text = train.GetComponent<PathCreation.PathFollower>().targetSpeed.ToString();
        heightSlider.onValueChanged.AddListener(delegate { UpdateHeight(); });
        horizonSlider.onValueChanged.AddListener(delegate { UpdateHorizon(); });
        speedSlider.onValueChanged.AddListener(delegate { UpdateSpeed(); });
        UpdateHeight();
        UpdateHorizon();
        UpdateSpeed();
    }

    void UpdateHeight()
    {
        mapCamera.GetComponent<Map>().height = heightSlider.value;
    }

    void UpdateHorizon()
    {
        trainCamera.GetComponent<TrainCamera>().horizon = horizonSlider.value;
    }

    void UpdateSpeed()
    {
        train.GetComponent<PathCreation.PathFollower>().targetSpeed = speedSlider.value;
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
