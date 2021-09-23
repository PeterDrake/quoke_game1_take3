using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_NPCFollowsPC : MonoBehaviour
{

    public int speed;
    private int movespeed;
    //Transform that NPC has to follow
    public Transform transformToFollow;
    //NavMesh Agent variable
    NavMeshAgent agent;
    
    public GameObject Player;
    public GameObject NPC;
    private Animator animator;
    private int runs;
    
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);
        
        if (Mathf.Abs(Player.transform.position.x - NPC.transform.position.x) > 3  ||
            Mathf.Abs(Player.transform.position.z - NPC.transform.position.z) > 3)
        {
            runs += 1;
            if (runs > 30)
            {
                
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
            }
            else
            {
                animator.SetBool("isWalking", true);
            }
        }
        
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            runs = 0;
        }
        
        agent.SetDestination(transformToFollow.position);
    }

    public void OnTriggerStay(Collider other)
    {


        if (other.tag == "Player")
        {
            movespeed = 0;
        }

    }
    public void OnTriggerExit(Collider other)
    {
        speedup();
    }

    public void speedup()
    {
        movespeed = speed;
    }
    
}