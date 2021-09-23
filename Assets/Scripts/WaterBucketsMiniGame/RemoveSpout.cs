using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSpout : MonoBehaviour
{

    public GameObject Spout;
    
    void OnMouseDown()
    {
        Destroy(Spout);
    }
}
