using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusic : UIElement
{
    private bool musicIsOff;
    private bool sfxIsOff;

    public GameObject music;
    public GameObject mToggle;
    public GameObject sfx;
    public GameObject sToggle;

    // Start is called before the first frame update
    void Start()
    {

        //music = GameObject.Find("Audio");
        //sfx = GameObject.Find("SoundFX");

        print("SetMusic start method");

        musicIsOff = SavedData.musicOff;
        if (!musicIsOff)
        {
            Debug.Log("music is on");
            music.SetActive(true);
        }
        else
        {
            Debug.Log("music is off");
            music.SetActive(false);
        }
        sfxIsOff = SavedData.sfxOff;
        if (!sfxIsOff)
        {
            Debug.Log("soundFX is on");
            sfx.SetActive(true);
        }
        else
        {
            Debug.Log("soundFX is off");
            sfx.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Open()
    {
    }

    public override void Close()
    {
    }
}
