using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCamera : MonoBehaviour
{
    public float horizon;

    void Update()
    {
       
        horizon = Mathf.Clamp(horizon, Constants.TrainCameraLeftMax, Constants.TrainCameraRightMax);
        transform.position = transform.parent.position + transform.right * horizon;
    }
}
