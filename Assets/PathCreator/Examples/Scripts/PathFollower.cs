using System.Collections;
using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public static readonly float OriginalSpeed = 15;

        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        private EndOfPathInstruction previousEndOfPathInstruction;
        public float speed = OriginalSpeed;
        public Vector3 originPosition;
        public float distanceTravelled;

        public static Vector3 heightOfCart = new Vector3(0, 0.5f, 0);
        public static float initialRotation = 90;

        public float distanceFromStation = 0.95f;
        public float distanceFromStationThreshold = 0.15f;

        public int trainSize = 0;
        public float sizeOfCart = 4;
        public float sizeOfSpaceBetweenCarts = 0.5f;
        public float extraSpace = 0;

        private static readonly string fullTrain = "Train";
        private static readonly string trainCart = "Cart";

        private void Awake()
        {
            previousEndOfPathInstruction = endOfPathInstruction;
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.CompareTag(trainCart))
                {
                    child.gameObject.AddComponent<PathFollower>();
                    trainSize++;
                }
            }
            extraSpace = trainSize * sizeOfCart + (trainSize - 1) * sizeOfSpaceBetweenCarts;
        }

        void Start() {
            originPosition = transform.localPosition;
            if (pathCreator == null)
            {
                if (transform.parent != null && transform.parent.tag == fullTrain)
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
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                float relatedLocationOnTrack = GetRealtedLocationOnTrack();
                float extraSpace = 0f;
                if (transform.tag == fullTrain)
                {
                    if (relatedLocationOnTrack < 1- distanceFromStation)
                    {
                        TrainSetSpeed(OriginalSpeed / 3);
                    }
                    else if (relatedLocationOnTrack > 1 - distanceFromStation&&relatedLocationOnTrack < 1 - distanceFromStation + distanceFromStationThreshold)
                    {
                        TrainAccelerateUp(relatedLocationOnTrack, OriginalSpeed / 3, OriginalSpeed);
                    }
                    else if(relatedLocationOnTrack > distanceFromStation - distanceFromStationThreshold && relatedLocationOnTrack < distanceFromStation)
                    {
                        TrainAccelerateDown(relatedLocationOnTrack, OriginalSpeed,OriginalSpeed / 3);
                    }
                    else if (relatedLocationOnTrack > distanceFromStation && relatedLocationOnTrack < 1)
                    {
                        TrainSetSpeed(OriginalSpeed / 3);
                    }
                    else if (relatedLocationOnTrack >= 1)
                    {
                        if(endOfPathInstruction== EndOfPathInstruction.Stop)
                        {
                            TrainStop();
                        }
                        else if(endOfPathInstruction == EndOfPathInstruction.Loop)
                        {
                            distanceTravelled = extraSpace;
                            UpdateAllChildrenData();
                        }
                        else if (endOfPathInstruction == EndOfPathInstruction.Reverse)
                        {
                            //Need To Add Here!!!
                            UpdateAllChildrenData();
                            TrainSetSpeed(OriginalSpeed / 3);
                        }
                    }
                    else if (speed != OriginalSpeed)
                    {
                        TrainSetSpeed(OriginalSpeed);
                    }
                }
                print(extraSpace);
                transform.position = heightOfCart + pathCreator.path.GetPointAtDistance(distanceTravelled - originPosition.magnitude * 0.75f, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled - originPosition.magnitude * 0.75f, endOfPathInstruction);
                transform.rotation = Quaternion.AngleAxis(90, transform.forward) * transform.rotation;
            }
        }

        void TrainAccelerateDown(float relatedLocationOnTrack,float originSpeed ,float targetSpeed)
        {
            float minValue = distanceFromStation - distanceFromStationThreshold;
            float speedVar = Mathf.Lerp(0, 1, (relatedLocationOnTrack - minValue) * (1 / distanceFromStationThreshold));
            speed = (originSpeed - (speedVar * (originSpeed - (targetSpeed))));
            UpdateSpeedForChildCarts(speed);
        }

        void TrainAccelerateUp(float relatedLocationOnTrack, float originSpeed, float targetSpeed)
        {
            float minValue = distanceFromStation - distanceFromStationThreshold;
            float speedVar = Mathf.Lerp(0, 1, (relatedLocationOnTrack - (1 - distanceFromStation)) * (1 / distanceFromStationThreshold));
            print(speedVar);
            speed = (originSpeed + (speedVar * (targetSpeed - originSpeed)));
            UpdateSpeedForChildCarts(speed);
        }

        void TrainStop()
        {
            float minValue = distanceFromStation - distanceFromStationThreshold;
            speed = 0;
            UpdateSpeedForChildCarts(speed);
            StartCoroutine(WaitFunction(3));
            //speed = OriginalSpeed/3;
            //UpdateSpeedForChildCarts(speed);
        }

        void TrainSetSpeed(float newSpeed)
        {
            speed = newSpeed;
            UpdateSpeedForChildCarts(speed);
        }

        void UpdateSpeedForChildCarts(float newSpeed)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.CompareTag(trainCart))
                {
                    child.GetComponent<PathFollower>().speed = newSpeed;
                }
            }
        }

        void UpdateAllChildrenData()
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.CompareTag(trainCart))
                {
                    child.GetComponent<PathFollower>().pathCreator = pathCreator;
                    child.GetComponent<PathFollower>().speed = speed;
                    child.GetComponent<PathFollower>().endOfPathInstruction = endOfPathInstruction;
                    child.GetComponent<PathFollower>().previousEndOfPathInstruction = previousEndOfPathInstruction;
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

        public IEnumerator WaitFunction(int time)
        {
            yield return new WaitForSeconds(time);
        }
    }
}