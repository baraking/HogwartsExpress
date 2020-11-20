using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WizardMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public enum Mode { SearchTarget,FollowParent};

    public Mode mode;
    public GameObject possibleTargets;
    Vector3 curTarget;

    private void Start()
    {
        possibleTargets = GameObject.Find("Targets");
        mode = Mode.SearchTarget;
    }

    void Update()
    {
        if (navMeshAgent.enabled)
        {
            float distanceToTarget = Vector3.Distance(gameObject.transform.position, curTarget);
            if (mode == Mode.SearchTarget)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mouse = Input.mousePosition;
                    Ray castPoint = Camera.main.ScreenPointToRay(mouse);
                    RaycastHit hit;
                    if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                    {
                        curTarget = hit.point;
                        navMeshAgent.SetDestination(curTarget);
                    }
                }
                if (distanceToTarget < Constants.DistanceToTarget || curTarget == Vector3.zero)
                {
                    FindNewTarget();
                }
            }
            else if (mode == Mode.FollowParent)
            {
                if (distanceToTarget < Constants.CloseDistanceToTarget || curTarget == Vector3.zero)
                {
                    navMeshAgent.enabled = false;
                }
            }
        }   
    }

    public void FindNewTarget()
    {
        if (navMeshAgent.enabled)
        {
            int random = Random.Range(0, possibleTargets.transform.childCount);
            Vector3 nextTarget = possibleTargets.transform.GetChild(random).position;
            curTarget = nextTarget;
            navMeshAgent.SetDestination(curTarget);
            transform.parent = possibleTargets.transform.GetChild(random).GetComponent<target>().parentHolder.transform;
        }
    }
}
