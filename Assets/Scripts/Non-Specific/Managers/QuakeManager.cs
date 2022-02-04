using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

/// <summary>
/// Handles the earthquake, falling object calls, effects, information, etc.
/// </summary>
public class QuakeManager : MonoBehaviour
{
    public static QuakeManager Instance;

    [Header("Admin Tools")]
    [SerializeField] private bool adminMode = true;
    [SerializeField] private bool showCountdown = true;
    [Space]
    [Space]

    [Tooltip("How long after starting before the earthquake goes off?")]
    [SerializeField] private float TimeBeforeQuake = 15f;

    [Tooltip("How long after the first quake before aftershock?")]
    [SerializeField] private float AftershockTime = 10f;

    //----Camera Shake Options---
    [Header("Camera Shake Options")]
    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    public float ShakeDuration;
    public float ShakeAmplitude;
    public float ShakeFrequency;

    private float ShakeElapsedTime = 0f;
    
    public bool _leaveSatisfied;

    public string leaveHouse = "LEAVEHOUSE";
    //--------------------


    [TextArea] [SerializeField] private string textOnQuake;
    [TextArea] [SerializeField] private string textAfterQuake;

    // Game object which will be disabled after quake
    [SerializeField] private GameObject enableDoors;

    [SerializeField] private GameObject frontDoor;
    //added 49
    [SerializeField] private GameObject backDoor;
    //added 51
    [SerializeField] private GameObject bedroomDoor;

    [SerializeField] private GameObject dustStormPrefab;

    // TODO Move these into a separate object
    private GameObject[] doors;
    private Rigidbody[] bodies;
    private Clobberer[] clobberers;

    private GameObject Sink;


    [HideInInspector] public bool Quaking;

    public byte quakes; //times quaked 


    private bool _inQuakeZone; // is player in a zone where the quake can happen?
    public bool _inSafeZone; // is the player safe (under the table)?

    private bool _countdownFinished;
    private float entranceGracePeriod = 2f;
    private float _timeTillQuake;

    [SerializeField] private float _minimumShakes = 1;
    private bool quakeOverride;

    private LogToServer logger;


    /*Subscribed to onQuake:
        QuakeFurniture
        Bookcase
     */
    // scripts can 'subscribe' to this Event to have their functions called when the earthquake begins
    public UnityEvent OnQuake;

    private InformationCanvas _informationCanvas;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        StartCoroutine(nameof(QuakeCountdown), TimeBeforeQuake);

        doors = GameObject.FindGameObjectsWithTag("Door");
        bodies = Array.ConvertAll(doors, d => d.GetComponent(typeof(Rigidbody)) as Rigidbody);
        clobberers = Array.ConvertAll(doors, d => d.GetComponent(typeof(Clobberer)) as Clobberer);

        Sink = GameObject.Find("Kitchen Sink").gameObject;

        _informationCanvas = GameObject.Find("MiniGameClose").transform.Find("GUI").GetComponent<GuiDisplayer>().GetBanner();
        virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
    }

    void Update()
    {
        if ((_countdownFinished && !Quaking && (quakeOverride || _inQuakeZone)) || (adminMode && Input.GetKeyDown("p")))
        {
            TriggerQuake();
        }
        
    }

    // Starts a countdown of 'time' seconds. When the countdown finishes, the earthquake will happen
    public void TriggerCountdown(float time)
    {
        _countdownFinished = false;
        StopCoroutine(nameof(QuakeCountdown));
        StartCoroutine(nameof(QuakeCountdown), time);
    }

    // the actual countdown
    private IEnumerator QuakeCountdown(float CountdownTime)
    {
        _timeTillQuake = CountdownTime;
        while (_timeTillQuake > 0)
        {
            yield return new WaitForSeconds(1f);
            _timeTillQuake--;
            if (showCountdown) Debug.Log("Time Till Quake: " + _timeTillQuake);
        }
        _countdownFinished = true;
    }

    // flaps  each o the doors for the given duration
    public IEnumerator FlapDoors(float duration)
    {
        while (duration > 0)
        {
            Vector3 kick = Random.onUnitSphere * 1;
            foreach (Rigidbody b in bodies)
            {
                b.AddRelativeForce(kick, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(0.25f);
            duration -= 0.25f;

        }
    }

    // everything that happens during the earthquake
    private IEnumerator ShakeIt()
    {
        Instantiate(dustStormPrefab, new Vector3(100, 10, -65), Quaternion.identity);

        foreach (Clobberer c in clobberers)
        {
            c.enabled = true;
        }

        int shakes = 1;
        while (true)
        {
            shakecamera(ShakeDuration, ShakeAmplitude, ShakeFrequency);
            // camera.ShakeCamera(duration, amplitude, frequency, false);
            StartCoroutine(FlapDoors(ShakeDuration));
            yield return new WaitForSeconds(ShakeDuration);
            // if the player is in the safezone, and the earthquake has gone long enough, stop it 
            if (_inSafeZone && shakes >= _minimumShakes)
            {
                break;
            }

            shakes++;
        }

        StopQuake();
        //foreach (Clobberer c in clobberers)
        //{
        //    c.enabled = false;
        //}

        frontDoor.GetComponent<Clobberer>().enabled = false;
        //added 189
        backDoor.GetComponent<Clobberer>().enabled = false;
        //added 193
        bedroomDoor.GetComponent<Clobberer>().enabled = false;
        bedroomDoor.GetComponent<Clobberer>().aftershock = true;

        _informationCanvas.ChangeText(textAfterQuake);
        Systems.Objectives.Satisfy("SURVIVEQUAKE");
        Systems.Objectives.Register(QuakeManager.Instance.leaveHouse, () => _leaveSatisfied = true);

        enableDoors.SetActive(false); // allow player to exit house

        quakes++;
    }

    public void TriggerQuake()
    {
        if (Quaking) return;
        Systems.Status.Pause();

        Quaking = true;
        Logger.Instance.Log((quakes == 0 ? "Earthquake" : "Aftershock") + " triggered!");
        logger.sendToLog("Quake triggered!","EVENT");
        //StopAllCoroutines();
        StopCoroutine(nameof(QuakeCountdown));
        StopCoroutine(nameof(AftershockTime));
        

        OnQuake.Invoke(); // every function subscribed to OnQuake is called here

        _informationCanvas.ChangeText(textOnQuake);

        StartCoroutine(ShakeIt());
    }

    public void StopQuake()
    {
        if (!Quaking || quakes > 0) return;
        Logger.Instance.Log("Quake Stopped");

        virtualCameraNoise.m_AmplitudeGain = 0f;
        ShakeElapsedTime = 0f;

        Quaking = false;
        if (GameObject.Find("Interactables") != null)
        {
            GameObject.Find("Interactables").transform.Find("Falling Bookshelf").GetComponent<InteractWithObject>().BlinkOnInteract = false;

        }
        Destroy(Sink.GetComponent<InteractWithObject>());
        Systems.Status.UnPause();
        Debug.Log("TriggerCountdown called from StopQuake");
        TriggerCountdown(AftershockTime);
    }

    public void InSafeZone(bool status)
    {
        _inSafeZone = status;
    }

    public void PlayerInQuakeZone(bool status)
    {

        Debug.Log("Grace Period" + entranceGracePeriod);
        if (quakes > 0 && status == false)
        {
            Debug.Log("TriggerCountdown called from PlayerInQuakeZone short");
            TriggerCountdown(entranceGracePeriod);
        }

        if (status && (_countdownFinished || _timeTillQuake < entranceGracePeriod) && (_inQuakeZone != status))
        {
            Debug.Log("TriggerCountdown called from PlayerInQuakeZone long");
            TriggerCountdown(entranceGracePeriod);
        }

        _inQuakeZone = status;
    }

    public void ManualTriggerAftershock(float gracePeroid)
    {
        if (quakes > 0)
        {
            quakeOverride = true;
            Debug.Log("TriggerCountdown called from ManualTriggerAftershock");
            TriggerCountdown(gracePeroid);
        }
    }

    public void shakecamera(float duration, float amplitude, float frequency)
    {
        ShakeElapsedTime = duration;
        while (ShakeElapsedTime > 0)
        {

            // Set Cinemachine Camera Noise parameter
            virtualCameraNoise.m_AmplitudeGain = amplitude;
            virtualCameraNoise.m_FrequencyGain = frequency;

            // Update Shake Timer
            ShakeElapsedTime -= Time.deltaTime;

        }


    }
}



