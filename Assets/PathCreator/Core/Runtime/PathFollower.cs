using System;
using System.Collections;
using UnityEngine;

namespace PathCreation
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        private EndOfPathInstruction previousEndOfPathInstruction;
        public float speed = Constants.OriginalSpeed;
        public float targetSpeed;
        public Vector3 originPosition;
        public float distanceTravelled;
        public bool openDoors = false;

        public GameObject doorAnchor;
        public DoorAnimation door;
        public DoorAnimation1 door1;

        public static float yExtraHeight = .9f;
        public static Vector3 heightOfCart = new Vector3(0, yExtraHeight, 0);
        public static float initialRotation = 90;

        public static float accelarationSpeed = 0.1f;
        public float innerClock=-1;

        public int trainSize = 0;
        public float sizeOfCart = 8;
        public float sizeOfSpaceBetweenCarts = 0.5f;
        public float extraSpace = 0;

        private void Awake()
        {
            previousEndOfPathInstruction = endOfPathInstruction;
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.CompareTag(Constants.trainCart))
                {
                    child.gameObject.AddComponent<PathFollower>();
                    trainSize++;
                }
            }
            extraSpace = trainSize * sizeOfCart + (trainSize - 1) * sizeOfSpaceBetweenCarts;
        }

        void Start() {
            originPosition = transform.localPosition;
            targetSpeed = speed;
            if (pathCreator == null)
            {
                if (transform.parent != null && transform.parent.tag == Constants.fullTrain)
                {
                    UpdateChildCart();
                }
            }
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                distanceTravelled += extraSpace;
                pathCreator.pathUpdated += OnPathChanged;
            }
            if (transform.tag == Constants.trainCart)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.gameObject.CompareTag(Constants.doorAnchorTag))
                    {
                        doorAnchor = child.gameObject;
                        //add doors
                        foreach (Transform grandChild in child.transform)
                        {
                            if (grandChild.gameObject.CompareTag(Constants.doorTag))
                            {
                                if (grandChild.name==("Door")){
                                    door = grandChild.GetComponent<DoorAnimation>();
                                }
                                else if (grandChild.name == ("Door (1)"))
                                {
                                    door1 = grandChild.GetComponent<DoorAnimation1>();
                                }
                            }
                        }
                    }
                }
                
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                if (transform.tag == Constants.fullTrain)
                {
                    if (innerClock >= 0)
                    {
                        if(innerClock>= Constants.TrainOpenDoorsTime&& innerClock < Constants.TrainCloseDoorsTime&& !openDoors)
                        {
                            //notify carts to open doors
                            openDoors = true;
                            UpdateAllChildrenDoorStatus();
                        }
                        if (innerClock >= Constants.TrainCloseDoorsTime && openDoors)
                        {
                            //notify carts to close doors
                            openDoors = false;
                            UpdateAllChildrenDoorStatus();
                        }

                        if (innerClock >= Constants.TrainStopWaitTime)
                        {
                            innerClock = -1;
                            UpdateAllChildrenData();
                        }
                        innerClock += Time.deltaTime;
                    }
                    else
                    {
                        if (speed < targetSpeed)
                        {
                            speed += accelarationSpeed;
                            if (speed > targetSpeed)
                            {
                                speed = targetSpeed;
                            }
                        }
                        else if (speed > targetSpeed)
                        {
                            speed -= accelarationSpeed;
                            if (speed < targetSpeed)
                            {
                                speed = targetSpeed;
                            }
                        }
                        UpdateAllChildrenData();
                    }
                }
                else if (transform.tag == Constants.trainCart)
                {
                    if (door != null && door1 != null)
                    {
                        if (door.operDoors != openDoors || door1.operDoors != openDoors)
                        {
                            if (door.operDoors)
                            {
                                door.DoorCloseAnimation();
                                door1.DoorCloseAnimation1();
                            }
                            else
                            {
                                door.DoorOpenAnimation();
                                door1.DoorOpenAnimation1();
                            }
                        }
                    }
                }

                distanceTravelled += speed * Time.deltaTime;
                transform.position = heightOfCart + pathCreator.path.GetPointAtDistance(distanceTravelled - originPosition.magnitude * 0.75f, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled - originPosition.magnitude * 0.75f, endOfPathInstruction);
                transform.rotation = Quaternion.AngleAxis(90, transform.forward) * transform.rotation;
            }
        }

        public void TrainAccelerate(float newTargetSpeed)
        {
            targetSpeed = newTargetSpeed;
            UpdateAllChildrenData();
        }

        public void TrainStop(float newTargetSpeed)
        {
            speed = 0;
            UpdateSpeedForChildCarts(speed);
            innerClock = 0.0f;
            targetSpeed = newTargetSpeed;
        }

        public void TrainSetSpeed(float newSpeed)
        {
            speed = newSpeed;
            UpdateSpeedForChildCarts(speed);
        }

        void UpdateSpeedForChildCarts(float newSpeed)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.CompareTag(Constants.trainCart))
                {
                    child.GetComponent<PathFollower>().speed = newSpeed;
                    child.GetComponent<PathFollower>().targetSpeed = targetSpeed;
                }
            }
        }

        void UpdateAllChildrenData()
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.CompareTag(Constants.trainCart))
                {
                    child.GetComponent<PathFollower>().pathCreator = pathCreator;
                    child.GetComponent<PathFollower>().speed = speed;
                    child.GetComponent<PathFollower>().targetSpeed = targetSpeed;
                    child.GetComponent<PathFollower>().endOfPathInstruction = endOfPathInstruction;
                    child.GetComponent<PathFollower>().previousEndOfPathInstruction = previousEndOfPathInstruction;
                }
            }
        }

        void UpdateAllChildrenDoorStatus()
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.CompareTag(Constants.trainCart))
                {
                    child.GetComponent<PathFollower>().openDoors = openDoors;
                }
            }
        }

        void UpdateAllChildrenDistanceTravelled()
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.CompareTag(Constants.trainCart))
                {
                    child.GetComponent<PathFollower>().distanceTravelled = distanceTravelled;
                }
            }
        }

        void UpdateChildCart(){
            pathCreator = transform.parent.GetComponentInParent<PathFollower>().pathCreator;
            speed = transform.parent.GetComponentInParent<PathFollower>().speed;
            endOfPathInstruction = transform.parent.GetComponentInParent<PathFollower>().endOfPathInstruction;
            previousEndOfPathInstruction = endOfPathInstruction;
            trainSize = transform.parent.GetComponentInParent<PathFollower>().trainSize;
            extraSpace = transform.parent.GetComponentInParent<PathFollower>().extraSpace;
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        float GetRealtedLocationOnTrack()
        {
            return ((distanceTravelled - originPosition.magnitude * 0.75f) / pathCreator.path.length);
        }


    }
}