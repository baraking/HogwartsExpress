using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        public Vector3 originPosition;
        float distanceTravelled;

        public static Vector3 heightOfCart = new Vector3(0, 0.5f, 0);
        public static float initialRotation = 90;

        private static readonly string fullTrain = "Train";
        private static readonly string trainCart = "Cart";

        private void Awake()
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.gameObject.CompareTag(trainCart))
                {
                    child.gameObject.AddComponent<PathFollower>();
                }
            }
        }

        void Start() {
            originPosition=transform.localPosition;
            if(pathCreator == null)
            {
                if (transform.parent != null && transform.parent.tag == fullTrain)
                {
                    pathCreator = transform.parent.GetComponentInParent<PathFollower>().pathCreator;
                    speed = transform.parent.GetComponentInParent<PathFollower>().speed;
                    endOfPathInstruction = transform.parent.GetComponentInParent<PathFollower>().endOfPathInstruction;
                }
            }
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = heightOfCart + pathCreator.path.GetPointAtDistance(distanceTravelled - originPosition.magnitude, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled - originPosition.magnitude, endOfPathInstruction);
                transform.rotation = Quaternion.AngleAxis(90, transform.forward) * transform.rotation;
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}