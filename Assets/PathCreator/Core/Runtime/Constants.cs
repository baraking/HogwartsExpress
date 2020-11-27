using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public static readonly float OriginalSpeed = 15;
    public static readonly int TrainStopWaitTime = 18;
    public static readonly int TrainOpenDoorsTime = 3;
    public static readonly int TrainCloseDoorsTime = 15;
    public static readonly float TrainDoorWaitTimeForBake = 1;

    public static readonly float DistanceToTarget = 1;
    public static readonly float CloseDistanceToTarget = 0.5f;

    public static readonly string fullTrain = "Train";
    public static readonly string trainCart = "Cart";
    public static readonly string doorTag = "Door";
    public static readonly string doorAnchorTag = "DoorAnchor";
    public static readonly string wizardTag = "Wizard";

    public static readonly float cartOffset1 = 4.1f;
    public static readonly float cartOffset2 = 1.6f;

    public static readonly Vector3 WizardScale = new Vector3(0.15f, 0.15f, 0.15f);

    public static readonly float chanceToBoardTrain = 75;
    public static readonly float chanceToLeaveTrain = 95;

    public static readonly int MapMinHeight = 25;
    public static readonly int MapMaxHeight = 100;
}
