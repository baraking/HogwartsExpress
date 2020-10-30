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
        public Vector3 originPosition;
        public float distanceTravelled;

        public static Vector3 heightOfCart = new Vector3(0, 0.5f, 0);
        public static float initialRotation = 90;

        public float distanceFromStation = 0.95f;
        public float distanceFromStationThreshold = 0.15f;

        public static float accelarationSpeed = 10f;

        public int trainSize = 0;
        public float sizeOfCart = 4;
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
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = heightOfCart + pathCreator.path.GetPointAtDistance(distanceTravelled - originPosition.magnitude * 0.75f, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled - originPosition.magnitude * 0.75f, endOfPathInstruction);
                transform.rotation = Quaternion.AngleAxis(90, transform.forward) * transform.rotation;
            }
        }

        public void TrainAccelerate(float targetSpeed)
        {

            StartCoroutine(ChangeSpeed(targetSpeed));
        }

        IEnumerator ChangeSpeed(float targetSpeed)
        {
            float duration = Mathf.Abs(speed - targetSpeed) / accelarationSpeed;
            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                if (speed > targetSpeed)
                {
                    speed = Mathf.Lerp(speed, targetSpeed, elapsed / duration);
                }
                else
                {
                    speed = Mathf.Lerp(targetSpeed, speed, elapsed / duration);
                }
                UpdateSpeedForChildCarts(speed);
                elapsed += Time.deltaTime;
                yield return null;
            }
            speed = targetSpeed;
            UpdateSpeedForChildCarts(speed);
        }

        public void TrainStop()
        {
            speed = 0;
            UpdateSpeedForChildCarts(speed);
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
                    child.GetComponent<PathFollower>().endOfPathInstruction = endOfPathInstruction;
                    child.GetComponent<PathFollower>().previousEndOfPathInstruction = previousEndOfPathInstruction;
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