using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private NavMeshPath _navMeshPath;
    private LineRenderer lineRenderer;

    public float detectDistance;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshPath = new NavMeshPath();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, detectDistance))
        {
            if(hit.transform.tag == "Player")
            {
                SetAITargetLocation(hit.point);
            }
        }

        if (_navMeshAgent.hasPath)
        {
            CreateAIPath();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void SetAITargetLocation(Vector3 targetLocation)
    {
        _navMeshAgent.SetDestination(targetLocation);
    }

    // Creates the path in game view from the nav mesh path using a line renderer
    private void CreateAIPath()
    {
        lineRenderer.enabled = true;
        NavMesh.CalculatePath(transform.position, _navMeshAgent.destination, NavMesh.AllAreas, _navMeshPath);
        lineRenderer.positionCount = _navMeshPath.corners.Length;
        lineRenderer.SetPositions(_navMeshPath.corners);
    }
}