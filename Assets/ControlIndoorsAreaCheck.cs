using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlIndoorsAreaCheck : MonoBehaviour
{
    public GameObject InsideCheck;
    public GameObject DeathScreen;
    public GameObject ExplosionSound;
    public GameObject house;
    public GameObject destroy;

    // Start is called before the first frame update
    void Start()
    {
        InsideCheck.SetActive(false);
        StartCoroutine(nameof(Wait));
    }

    void Update()
    {
        if (DeathScreen.activeSelf)
        {
            StartCoroutine(nameof(Wait2));
            InsideCheck.SetActive(false);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);
        InsideCheck.SetActive(true);
    }

    private IEnumerator Wait2()
    {
        yield return new WaitForSeconds(0.85f);
        ExplosionSound.SetActive(false);
        house.SetActive(false);
        destroy.SetActive(true);
    }
        
        
    
}