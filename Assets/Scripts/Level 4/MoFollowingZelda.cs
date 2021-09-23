using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoFollowingZelda : MonoBehaviour
{
    public float runAwayDistance;
    public GameObject targetGO;
    public GameObject NPC;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float last;
    public bool shelter;
    private int check = 20;
    private bool follow;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = NPC.GetComponent<Animator>();
        last = 0f;
    }

    public void TurnOffBanner()
    {
        GetComponent<InteractWithObject>().OnTriggerExit(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>());
        GetComponent<InteractWithObject>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
    }

    private void Update()
    {       
       if (shelter)
        {
            if (!follow)
            {
                transform.position = new Vector3(-190, transform.position.y, -263);
                GetComponent<InteractWithObject>().enabled = false;
                GetComponent<SphereCollider>().enabled = false;
                GameObject.Find("MoAlert").SetActive(false);
                follow = true;
            }
            GoToShelter();
        }

        else
        {
            Vector3 targetPosition = targetGO.transform.position;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceToTarget < runAwayDistance) 
            {
                //print("wayyyy " + distanceToTarget + " close");
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
            }

            else if (distanceToTarget > runAwayDistance && distanceToTarget < runAwayDistance + .5 || last == distanceToTarget) 
            {
                //print(distanceToTarget + "im here");
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", false);
            }

            else
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
                //print("running" + distanceToTarget);
            }
            FleeFromTarget(targetPosition);
            last = distanceToTarget;
        }
    }

    private void FleeFromTarget(Vector3 targetPosition)
    {
        Vector3 destination = PositionToFleeTowards(targetPosition);
        HeadForDestintation(destination);
    }

    private void HeadForDestintation(Vector3 destinationPosition)
    {
        navMeshAgent.SetDestination(destinationPosition);
        
    }

    private Vector3 PositionToFleeTowards(Vector3 targetPosition)
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(transform.position.x - targetPosition.x, targetPosition.y, transform.position.z - targetPosition.z));
        Vector3 runToPosition = targetPosition + (transform.forward * runAwayDistance);
        return runToPosition;
    }

    public void GoToShelter()
    {
        shelter = true;
        Vector3 destination = GameObject.Find("SchoolDoorStep").transform.position;
        float distance = Vector3.Distance(destination, transform.position);
        navMeshAgent.speed = 3f;
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
        if(check > 0)
        {
            runAwayDistance = 4f;
        }

        if (distance <= 2.5)
        {
            print("in the shelter I MAAADDDEE IT ");
            GameObject.Find("Mo").SetActive(false);
            GameObject.Find("MoPointer").SetActive(false);
            Destroy(this);
        }

        if (distance <= 4.5 && distance > 4)
        {
            animator.SetBool("isWalking", false);
            check--;
            if (check == 0)
            {
                runAwayDistance = 2f;
            }
        }
        FleeFromTarget(destination);
    }

    public void Follow()
    {
        follow = true;
    }

    public void Shelter()
    {
        shelter = true;
    }

}
