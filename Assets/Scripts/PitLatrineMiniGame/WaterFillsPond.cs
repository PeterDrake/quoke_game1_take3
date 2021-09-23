using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFillsPond : MonoBehaviour
{
    //This script is used for filling the pond in situation 1
    
    private Vector3 scaleChange;
    
    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(0.001f, 0, 0.001f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 0.15f)
        {
            transform.localScale += scaleChange;
        }
        
    }
}
