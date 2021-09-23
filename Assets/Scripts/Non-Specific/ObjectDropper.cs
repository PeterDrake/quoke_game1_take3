using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drops and staggers the falling furniture, the longer the quake goes, the faster stuff falls, at the very end
/// we drop an object directly on the players head if they still haven't gotten under the table or died
/// </summary>
public class ObjectDropper: MonoBehaviour
{
    public GameObject[] falling_objects;
    
    private GameObject player;
    public GameObject playerKiller;

    [Header("Delay before things start dropping")]
    public float DropDelay = 3f;
    
    [Header("Time before falling objects kill the player")]
    public float TotalDropTime = 15f;

    private float fallRate;
    
    private bool dropping;
    private Vector3 playerTransform;
    private int i = 0;
    
    private void Start()
    {
        playerKiller.SetActive(false);
        player = GameObject.FindWithTag("Player");
        fallRate = TotalDropTime / (falling_objects.Length + 1);
    }

    public void Drop()
    {
        StartCoroutine(DropEm());
    }
    
    private IEnumerator DropEm()
    {
        dropping = true;
        if(DropDelay > 0) yield return new WaitForSeconds(DropDelay);

        foreach (var obj in falling_objects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                if(fallRate > 0) yield return new WaitForSeconds(fallRate);
            }

            if (!QuakeManager.Instance.Quaking) break;
        }

        if (QuakeManager.Instance.Quaking && playerKiller != null)
        {
            playerKiller.transform.position = player.transform.position + new Vector3(0f, 3f, 0f);
            playerKiller.SetActive(true);
        }
    }
}
