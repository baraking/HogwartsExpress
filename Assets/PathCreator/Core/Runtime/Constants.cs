using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public static readonly float OriginalSpeed = 15;
    public static readonly int TrainStopWaitTime = 15;
    public static readonly int TrainOpenDoorsTime = 3;
    public static readonly int TrainCloseDoorsTime = 12;
    public static readonly float TrainDoorWaitTimeForBake = 1;

    public static readonly float DistanceToTarget = 1;
    public static readonly float CloseDistanceToTarget = 0.5f;

    public static readonly string fullTrain = "Train";
    public static readonly string trainCart = "Cart";
    public static readonly string doorTag = "Door";
    public static readonly string doorAnchorTag = "DoorAnchor";
    public static readonly string wizardTag = "Wizard";

    public static readonly Vector3 cartOffset1 = new Vector3(0.4f, 0, 4);
    public static readonly Vector3 cartOffset2 = new Vector3(0.4f, 0, 1.5f);

    public static readonly float chanceToBoardTrain = 75;
}
