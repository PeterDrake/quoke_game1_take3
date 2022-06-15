using UnityEngine;
using UnityEngine.AI;

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
            Debug.Log("a heading for " + waypoints[m_CurrentWaypointIndex].name);
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }

    public void Switch()
    {
        waypoints = nextWaypoints;
        m_CurrentWaypointIndex = 0;
        Debug.Log("a heading for " + waypoints[0].name);
        navMeshAgent.SetDestination(waypoints[0].position);
    }
}
