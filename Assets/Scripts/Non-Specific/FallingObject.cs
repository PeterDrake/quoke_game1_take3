using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// When a piece of furniture falls on the player from the quake it kills them.
/// after it hits the ground ~1.5f it will disable the ability to kill
/// </summary>
public class FallingObject : MonoBehaviour
{
    private bool isEnabled = true;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(DontKill());
    }

    private IEnumerator DontKill()
    {
        yield return new WaitForSeconds(1.5f);
        isEnabled = false;
        //rb.isKinematic = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isEnabled && other.gameObject.CompareTag("Player"))
        {
            Systems.Status.PlayerDeath("Crushed by falling object","Crushed by falling object "+ (QuakeManager.Instance.quakes == 0? "in earthquake":"in aftershock"));
        }
    }
}
