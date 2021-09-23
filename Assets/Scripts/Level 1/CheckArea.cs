using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArea : MonoBehaviour
{
    public bool Bath = false;
    public bool Kitch = false;

    public Collider Bathroom;
    public Collider Kitchen;
    public Collider MainRoom;

    public Collider Player;

    
    

    private void Update()
    {
        OnTriggerEnter(Player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //QuakeManager.Instance.TriggerQuake();
            if (MainRoom && Bath && Kitch)
            {
                QuakeManager.Instance.TriggerQuake();
            }
            else if (Bathroom)
            {
                Bath = true;
            }
            else if (Kitchen)
            {
                Kitch = true;
            }
        }
    }
    
    
}
