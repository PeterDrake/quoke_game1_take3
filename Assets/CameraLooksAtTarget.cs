using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLooksAtTarget : MonoBehaviour
{
 
    public Transform myCamera;
    public Transform target;
    public float z;
 
    void Update () 
    {
        myCamera.position = target.position + new Vector3(0, 0, z);
    }
}
