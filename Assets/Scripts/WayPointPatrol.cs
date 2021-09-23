using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Security.Cryptography;

public class WayPointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public Transform[] firstWaypoints;
    
    public Transform[] nextWaypoints;

    private Transform[] waypoints;
    
    int m_CurrentWaypointIndex;
    
    void Start()
    {
        waypoints = firstWaypoints;
        navMeshAgent.SetDestination(waypoints[0].position);
    }
    
    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }

    public void Switch()
    {
        waypoints = nextWaypoints;
        m_CurrentWaypointIndex = 0;
        navMeshAgent.SetDestination(waypoints[0].position);
    }
}
