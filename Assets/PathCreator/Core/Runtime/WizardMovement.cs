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

    public bool takeOffThisStation;

    private void Start()
    {
        mode = Mode.SearchTarget;
        takeOffThisStation = false;
    }

    void Update()
    {
        if (navMeshAgent.enabled)
        {
            if (possibleTargets == null)
            {
                return;
            }

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
                if (distanceToTarget < Constants.DistanceToTarget || curTarget == Vector3.zero || curTarget == null)
                {
                    transform.SetParent(possibleTargets.transform.parent.transform.parent, true);
                    transform.localScale = Constants.WizardScale;
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

            if (mode == Mode.FollowParent)
            {
                transform.SetParent(possibleTargets.transform.GetChild(random).GetComponent<target>().parentHolder.transform, true);
                transform.localScale = Constants.WizardScale;
            }
            //transform.parent = possibleTargets.transform.GetChild(random).GetComponent<target>().parentHolder.transform;
        }
    }

    public void SetNewParent()
    {
        int random = Random.Range(0, possibleTargets.transform.childCount);
        transform.SetParent(possibleTargets.transform.GetChild(random).GetComponent<target>().parentHolder.transform, true);
        transform.localScale = Constants.WizardScale;
    }
}
