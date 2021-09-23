using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorgiController : MonoBehaviour
{
    public GameObject NewMo;
    private bool check = false;

    private void Awake()
    {
        NewMo.SetActive(true);
    }

    private void Update()
    { 
        if (Systems.Objectives.Check("SaveCorgiTalk") & !check){
            check = true;
            print("do it here");
            StartCoroutine(nameof(EnterSaveCorgi));

        }
    }

    public void TalkToMo()
    {
        GetComponent<StartDialogue>().Interact();

    }

    private IEnumerator EnterSaveCorgi()
    {
        print("starting countdown");
        yield return new WaitForSeconds(.1f);
        GetComponent<SaveCorgiVisit>().Interaction();
        StopCoroutine(nameof(EnterSaveCorgi));

    }
}
