using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicSoundValues : MonoBehaviour
{
    // private GameObject mHandle;
    // private GameObject sHandle;


    void Start()
    {
        //mHandle = GameObject.Find("");
        //sHandle = GameObject.Find("");
    }

    void Update()
    {
        //if (mHandle.transform.position.x >= 50) { }
    }
    /*
    private int musicVal;
    private int sfxVal;

    public GameObject mHandle;
    public GameObject sHandle;

    private RectTransform mHandleTransform;
    private RectTransform sHandleTransform;
    private float handleSize;

    private RectTransform mToggleTransform;
    private RectTransform sToggleTransform;

    private GameObject musicAudio;
    private GameObject sfxAudio;

    // Start is called before the first frame update
    void Start()
    {
        musicVal = SavedData.musicInt;
        sfxVal = SavedData.sfxInt;

        mHandleTransform = GameObject.Find("handle 1").GetComponent<RectTransform>();
        sHandleTransform = GameObject.Find("handle 2").GetComponent<RectTransform>();
        //handleSize = 35;

        //mHandleTransform.x = musicVal - 50;
        //sHandleTransform.x = sfxVal - 50;

        
    }

    // Update is called once per frame
    void Update()
    {
        musicVal = GameObject.Find("MusicSlider").GetComponent<Slider>().Value;
        sfxVal = GameObject.Find("SFXSlider").GetComponent<Slider>().Value;


        SavedData.musicInt = musicVal;
        SavedData.sfxInt = sfxVal;
    }

    public void ChangeMusicValue(int val)
    {

    }*/
}
