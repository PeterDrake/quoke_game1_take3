using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTsuChangeDirection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("Corgi").GetComponent<WayPointPatrol>().Switch();
            Destroy(this);
        }
    }
}
