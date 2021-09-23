using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartVideoLoad : MonoBehaviour
{

    public GameObject Video;
    private bool loadedAlert;

    private VideoPlayer _videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        _videoPlayer = Video.GetComponent<VideoPlayer>();
        //Video.SetActive(true);
        _videoPlayer.source = VideoSource.Url;
        _videoPlayer.Prepare();
        Debug.Log("Started to load video");
        loadedAlert = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!loadedAlert && _videoPlayer.isPrepared)
        {
            Debug.Log("Video loaded");
            loadedAlert = true;
        }
    }
}
