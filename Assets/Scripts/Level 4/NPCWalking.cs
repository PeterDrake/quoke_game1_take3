using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWalking : MonoBehaviour
{
    public GameObject targetSpot;
    public GameObject NPC;
    public GameObject spotToShow;
    public bool speakOnArrival;
    public GameObject alert;
    public InformationCanvas _interact;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool check;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = NPC.GetComponent<Animator>();
        
    }

    public void TurnOffBanner()
    {
        GetComponent<InteractWithObject>().OnTriggerExit(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>());
        GetComponent<InteractWithObject>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!check)
        {
            animator.SetBool("isWalking", true);
            check = true;
        }
        Vector3 targetposition = targetSpot.transform.position;
        
        float distance = Vector3.Distance(targetposition, transform.position);
        if (distance < 1)
        {
            print("I MADE IT TO THE COMPOST");
            animator.SetBool("isWalking", false);
            if (speakOnArrival)
            {
                GetComponent<InteractWithObject>().enabled = true;
                GetComponent<SphereCollider>().enabled = true;
            }
            if (speakOnArrival && alert != null) { alert.GetComponent<FlatFollow>().appear(); }
            if (spotToShow != null)
            {
                spotToShow.SetActive(true);
                transform.LookAt(spotToShow.transform.position);
            }
            navMeshAgent.enabled = false;
            Destroy(this);

        }
        else
        {
            FleeFromTarget(targetposition);
        }
    }

    private void FleeFromTarget(Vector3 target)
    {
        Vector3 destination = PositionToFleeTowards(target);
        HeadForDestination(destination);
    }

    private void HeadForDestination(Vector3 destinationPos)
    {
        navMeshAgent.SetDestination(destinationPos);
    }
    private Vector3 PositionToFleeTowards(Vector3 target)
    {
        Vector3 runToPosition = target + (transform.forward);
        return runToPosition;
    }
}
