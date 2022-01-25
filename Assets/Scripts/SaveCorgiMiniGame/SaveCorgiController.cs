using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Video;
using Debug = UnityEngine.Debug;

public class SaveCorgiController : MonoBehaviour
{
    private DragTarp script;

    public GameObject camera;
    public GameObject Frank;
    public GameObject Tarp;
    public GameObject Corgi;
    public GameObject Banner;
    public GameObject VideoBackground;
    public GameObject VideoDisplayer;
    public GameObject Video;
    public GameObject Win;
    public GameObject VideoBackdrop;
    
    private bool gameOver;
    private bool start;
    private bool videoAlreadyLaunched;

    void Start()
    {
        Destroy(GameObject.Find("EnvironmentObjects"));
        Destroy(GameObject.Find("SM_Veh_Car_Muscle_02 (2)"));
        script = Tarp.GetComponent<DragTarp>();
        Video.SetActive(true);
        Video.GetComponent<VideoPlayer>().source = VideoSource.Url;
        // string filepath = "http://3.128.90.245/testbuild/StreamingAssets/CorgiVideoFINALE.webm";
        string filepath = System.IO.Path.Combine(Application.streamingAssetsPath, "CorgiFINALEsmall.mp4");
        Video.GetComponent<VideoPlayer>().url = filepath;
        
        Video.GetComponent<VideoPlayer>().Prepare();
        gameOver = false;
        start = false;
        Video.GetComponent<VideoPlayer>().loopPointReached += CheckOver;
    }

    void Update()
    {
        if (!videoAlreadyLaunched)
        {

            if (!gameOver && !start)
            {
                if (Tarp.transform.position.x < -117f)
                {
                    if (GameObject.Find("MiniMusic") != null)
                    {
                        GameObject.Find("MiniMusic").SetActive(false);
                    }

                    Destroy(script);
                    Banner.SetActive(false);
                    // StartVideo();
                    videoAlreadyLaunched = true;
                    Debug.Log("Starting the second Corgi video -- this should only happen once!");
                    StartCoroutine(nameof(PlayVideo));
                }

                if (Tarp.transform.position.x > -110f)
                {
                    // print("too far left");
                    Tarp.transform.position = new Vector3(-113f, 107.1427f, -156f);
                }
            }
        }
    }

    private IEnumerator PlayVideo()
    {
        // print("starting video now");
        VideoPlayer player = Video.GetComponent<VideoPlayer>();
        yield return new WaitForSeconds(.5f);
        Video.SetActive(true);
        // VideoDisplayer.SetActive(true);
        // VideoBackground.SetActive(true);
        VideoBackdrop.SetActive(true);
        player.Play();
        start = true;
        yield return new WaitForSeconds(1f);
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        LogToServer logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        logger.sendToLog("Completed Level 4!", "LEVEL");
        print("Video Is Over");
        GameObject.Find("Mo1").GetComponent<SaveCorgiVisit>().CorgiRescue();
        gameOver = true;
        print("done WINNEr <3");
        VideoBackground.SetActive(false);
        VideoDisplayer.SetActive(false);
        Video.SetActive(false);
        Tarp.SetActive(false);
        Corgi.SetActive(false);
//        Frank.SetActive(false);
        camera.transform.position = new Vector3(-112.58f, 109.9f, -141.5f);
        camera.transform.rotation = Quaternion.Euler(10, 180, 0);
        Win.SetActive(true);
    }
}