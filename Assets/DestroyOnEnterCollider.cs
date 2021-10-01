using UnityEngine;
using UnityEngine.AI;

public class DestroyOnEnterCollider : MonoBehaviour
{
    public GameObject WillBeDestroyed;

    private NavMeshAgent ToDestroy;
    private WayPointPatrol script;

    void Start()
    {
        ToDestroy = WillBeDestroyed.GetComponent<NavMeshAgent>();
        script = WillBeDestroyed.GetComponent<WayPointPatrol>();
    }

    // TODO This definition used to be mysteriously INSIDE a definition of Update, where it didn't do anything. Can it be removed?
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            Debug.Log("HERE IT IS");
            script.enabled = false;
            ToDestroy.enabled = false;
        }
    }
}