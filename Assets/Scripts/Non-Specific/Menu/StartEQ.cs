using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class StartEQ : MonoBehaviour
{
    public AudioSource eqSound;
    public AudioSource background;
    public Image wave;
    public GameObject button;
    public GameObject tree;
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        StartCoroutine("Read");
        background.Play();
    }

    public IEnumerator Read()
    {
        Debug.Log("im reading");
        yield return new WaitForSeconds(2f);
        StartCoroutine("ShakeCamera");
        StartCoroutine("Wave");
        
        

    }
    IEnumerator Wave()
    {
        float time = 5;
        while (time > 0)
        {
            wave.fillAmount += .04f;
            yield return new WaitForSeconds(.2f);
            time -= .2f;
        }
        
    }

    IEnumerator ShakeCamera()
    {
        eqSound.Play();
        Debug.Log("starting to shake");
        virtualCameraNoise.m_AmplitudeGain = 3f;
        virtualCameraNoise.m_FrequencyGain = 3f;
        tree.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(5f);
        virtualCameraNoise.m_AmplitudeGain = 0f;
        virtualCameraNoise.m_FrequencyGain = 0f;
    }

    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}

