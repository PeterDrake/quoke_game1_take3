using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniWin : MonoBehaviour
{
    public GameObject playMiniWin;

    public void MiniGameWon()
    {
        //miniWin.SetActive(true);
        playMiniWin.GetComponent<AudioSource>().Play();
    }
}
