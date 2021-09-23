using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToggle : MonoBehaviour
{

    private bool musicIsOff;
    private bool sfxIsOff;

    public GameObject handle;
    private RectTransform handleTransform;
    private float handleSize;

    public GameObject toggleBackground;
    private RectTransform toggleTransform;
    private float onPosX;
    private float offPosX;
    public float handleOffset;
    public Text status;

    public GameObject musicAudio;
    public GameObject sfxAudio;

    // Start is called before the first frame update
    void Start()
    {
        handleTransform = handle.GetComponent<RectTransform>();
        toggleTransform = handle.GetComponent<RectTransform>();
        handleSize = handleTransform.sizeDelta.x;
        float toggleSizeX = toggleTransform.sizeDelta.x;
        onPosX = (toggleSizeX / 2) - (handleSize / 2) - handleOffset;
        offPosX = onPosX * -1;
        
        print("moveToggles start method");

        setToggles();
        
    }

    void Awake()
    {
        
    }
    
    public void setToggles()
    {
        musicIsOff = SavedData.musicOff;
        if (!musicIsOff)
        {
            Debug.Log("music is on");
            handleTransform.localPosition = new Vector3(onPosX, toggleTransform.localPosition.y, 0);
            status.text = "ON";
            musicAudio.SetActive(true);
        }
        else
        {
            Debug.Log("music is off");
            handleTransform.localPosition = new Vector3(offPosX, toggleTransform.localPosition.y, 0);
            status.text = "OFF";
            musicAudio.SetActive(false);
        }
        sfxIsOff = SavedData.sfxOff;
        if (!sfxIsOff)
        {
            Debug.Log("soundFX is on");
            handleTransform.localPosition = new Vector3(onPosX, toggleTransform.localPosition.y, 0);
            status.text = "ON";
            sfxAudio.SetActive(true);
        }
        else
        {
            Debug.Log("soundFX is off");
            handleTransform.localPosition = new Vector3(offPosX, toggleTransform.localPosition.y, 0);
            status.text = "OFF";
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
            handleTransform.localPosition = new Vector3(onPosX, toggleTransform.localPosition.y, 0);
            status.text = "ON";
            musicAudio.SetActive(true);
        }
        else
        {
            Debug.Log("music turned off");
            handleTransform.localPosition = new Vector3(offPosX, toggleTransform.localPosition.y, 0);
            status.text = "OFF";
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
            handleTransform.localPosition = new Vector3(onPosX, toggleTransform.localPosition.y, 0);
            status.text = "ON";
            sfxAudio.SetActive(true);
        }
        else
        {
            Debug.Log("soundFX turned off");
            handleTransform.localPosition = new Vector3(offPosX, toggleTransform.localPosition.y, 0);
            status.text = "OFF";
            sfxAudio.SetActive(false);
        }
    }
}

