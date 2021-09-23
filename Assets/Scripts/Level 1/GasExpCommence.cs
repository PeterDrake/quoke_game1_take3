using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GasExpCommence : MonoBehaviour
{

    private bool LeftHouse;
    public AudioSource boomAudio;
    public GameObject bigBoom;
    public GameObject animateBoom;
    public GameObject house;
    public GameObject destroyed;
    public UnityEvent OnDeath;

    public bool _gasTalkSatisfied;

    public void LeavingHouse()
    {
        LeftHouse = true;
        Systems.Objectives.Satisfy(QuakeManager.Instance.leaveHouse, false);
        Debug.Log("Left house");
        Systems.Objectives.Register("GASTALK", (() => _gasTalkSatisfied = true));
        Systems.Objectives.printObjectives();
    }


    // Start is called before the first frame update
    void Start()
    {
        LeftHouse = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (LeftHouse)
            {
                //QuakeManager.Instance.TriggerQuake();
                //player dies in an explosion
                //new WaitForSeconds(3f);
                bigBoom.SetActive(true);
                animateBoom.SetActive(true);
                //new WaitForSeconds(1f);
                //boomAudio.Play();
                StartCoroutine(DestroyHouse());
                OnDeath.Invoke();
                Systems.Status.PlayerDeath("Gas explosion","You died in a gas explosion");
            }
        }
    }

    private IEnumerator DestroyHouse()
    {
        yield return new WaitForSeconds(0.75f);
        house.SetActive(false);
        destroyed.SetActive(true);
    }
}
