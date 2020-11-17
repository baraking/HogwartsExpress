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
        float distanceToTarget = Vector3.Distance(gameObject.transform.position, curTarget);
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
        else if (mode==Mode.SearchTarget) {
            if (distanceToTarget < Constants.DistanceToTarget || curTarget == Vector3.zero)
            {
                FindNewTarget();
            }
        }
        else if (mode == Mode.FollowParent)
        {

        }
            
    }

    public void FindNewTarget()
    {
        int random = Random.Range(0, possibleTargets.transform.childCount);
        Vector3 nextTarget = possibleTargets.transform.GetChild(random).position;
        curTarget = nextTarget;
        navMeshAgent.SetDestination(curTarget);
    }
}
