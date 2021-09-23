using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPointersOnQuake : MonoBehaviour
{

    public FlatFollow[] preQuake;
    public FlatFollow[] postQuake;
    
    public void Switch()
    {   if (SavedData.hints)
        {
            foreach (var point in preQuake)
            {
                point.disappear();
            }
            foreach (var point in postQuake)
            {
                point.appear();
            }
        }

    }


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
