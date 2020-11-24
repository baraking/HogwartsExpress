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

    public static readonly Vector3 cartOffset1 = new Vector3(-4.1f, 0, 0);
    public static readonly Vector3 cartOffset2 = new Vector3(-1.6f, 0, 0);

    public static readonly Vector3 WizardScale = new Vector3(0.15f, 0.15f, 0.15f);

    public static readonly float chanceToBoardTrain = 75;
    public static readonly float chanceToLeaveTrain = 95;
}
