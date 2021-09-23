using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvokeController : MonoBehaviour
{
    public GameObject Controller;
    
    void Start()
    {
        StartCoroutine(nameof(Evoke));
    }

    private IEnumerator Evoke()
    {
        yield return new WaitForSeconds(1f);
        Controller.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
