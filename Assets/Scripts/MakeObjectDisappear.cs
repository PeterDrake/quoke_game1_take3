using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObjectDisappear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dog")
        {
            Destroy(other.gameObject);
        }
    }
}
