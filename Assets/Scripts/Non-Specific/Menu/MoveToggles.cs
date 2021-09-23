using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class MoveToggles : MonoBehaviour
{
    private bool musicIsOff;
    private bool sfxIsOff;

    public GameObject mHandle;
    public GameObject sHandle;

    private RectTransform mHandleTransform;
    private RectTransform sHandleTransform;
    private float handleSize;

    private RectTransform mToggleTransform;
    private RectTransform sToggleTransform;
    private float onPosX;
    private float offPosX;

    public float handleOffset;
    public Text mStatus;
    public Text sStatus;

    public GameObject musicAudio;
    public GameObject sfxAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        mHandleTransform = mHandle.GetComponent<RectTransform>();
        sHandleTransform = sHandle.GetComponent<RectTransform>();

        mToggleTransform = mHandle.GetComponent<RectTransform>();
        sToggleTransform = sHandle.GetComponent<RectTransform>();

        handleSize = mHandleTransform.sizeDelta.x;
        float toggleSizeX = mToggleTransform.sizeDelta.x;
        onPosX = (toggleSizeX / 2) - (handleSize / 2) - handleOffset;
        offPosX = onPosX * -1;

        print("moveToggles start method");

        setToggles();
    }

    public void setToggles()
    {
        musicIsOff = SavedData.musicOff;
        if (!musicIsOff)
        {
            Debug.Log("music is on");
            mHandleTransform.localPosition = new Vector3(onPosX, mToggleTransform.localPosition.y, 0);
            mStatus.text = "ON";
            musicAudio.SetActive(true);
        }
        else
        {
            Debug.Log("music is off");
            mHandleTransform.localPosition = new Vector3(offPosX, mToggleTransform.localPosition.y, 0);
            mStatus.text = "OFF";
            musicAudio.SetActive(false);
        }
        sfxIsOff = SavedData.sfxOff;
        if (!sfxIsOff)
        {
            Debug.Log("soundFX is on");
            sHandleTransform.localPosition = new Vector3(onPosX, sToggleTransform.localPosition.y, 0);
            sStatus.text = "ON";
            sfxAudio.SetActive(true);
        }
        else
        {
            Debug.Log("soundFX is off");
            sHandleTransform.localPosition = new Vector3(offPosX, sToggleTransform.localPosition.y, 0);
            sStatus.text = "OFF";
            sfxAudio.SetActive(false);
        }
    }

    public void MusicToggle()
    {
        musicIsOff = !SavedData.musicOff;
        SavedData.musicOff = musicIsOff;
        print("musicIsOff = " + musicIsOff);
        print("SavedData.musicOff = " + SavedData.musicOff);
        if (!musicIsOff)
        {
            Debug.Log("music turned on");
            mHandleTransform.localPosition = new Vector3(onPosX, mToggleTransform.localPosition.y, 0);
            mStatus.text = "ON";
            musicAudio.SetActive(true);
        }
        else
        {
            Debug.Log("music turned off");
            mHandleTransform.localPosition = new Vector3(offPosX, mToggleTransform.localPosition.y, 0);
            mStatus.text = "OFF";
            musicAudio.SetActive(false);
        }
    }

    public void SoundFXToggle()
    {
        sfxIsOff = !SavedData.sfxOff;
        SavedData.sfxOff = sfxIsOff;
        print("sfxIsOff = " + sfxIsOff);
        print("SavedData.sfxOff = " + SavedData.sfxOff);
        if (!sfxIsOff)
        {
            Debug.Log("soundFX turned on");
            sHandleTransform.localPosition = new Vector3(onPosX, sToggleTransform.localPosition.y, 0);
            sStatus.text = "ON";
            sfxAudio.SetActive(true);
        }
        else
        {
            Debug.Log("soundFX turned off");
            sHandleTransform.localPosition = new Vector3(offPosX, sToggleTransform.localPosition.y, 0);
            sStatus.text = "OFF";
            sfxAudio.SetActive(false);
        }
    }

}
