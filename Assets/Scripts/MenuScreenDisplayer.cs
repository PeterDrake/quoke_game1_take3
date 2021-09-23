using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuScreenDisplayer : UIElement
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject post;
    private GameObject toggler;
    private bool musicIsOff;
    private bool sfxIsOff;
    // Start is called before the first frame update
    void Start()
    {
        toggler = GameObject.Find("EscapeClosed");
        UIManager.Instance.Initialize(this);
        EventSystem.current.SetSelectedGameObject(level1);


        /*print("EscapeClosed start method");

        musicIsOff = SavedData.musicOff;
        if (!musicIsOff)
        {
            Debug.Log("music is on");
            GameObject.Find("Audio").SetActive(true);
        }
        else
        {
            Debug.Log("music is off");
            GameObject.Find("Audio").SetActive(false);
        }
        sfxIsOff = SavedData.sfxOff;
        if (!sfxIsOff)
        {
            Debug.Log("soundFX is on");
            GameObject.Find("SoundFX").SetActive(true);
        }
        else
        {
            Debug.Log("soundFX is off");
            GameObject.Find("SoundFX").SetActive(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Open()
    {
        toggler.SetActive(true);
    }

    public override void Close()
    {
        toggler.SetActive(false);
    }
}
