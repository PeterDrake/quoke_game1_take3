using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Implements the basic functionality for "Player walks up to object, presses 'e' and something happens"
/// </summary>
public class ShelterVisit : MonoBehaviour
{

    public GameObject Rain;
    public GameObject BarrelWithoutWater;
    public GameObject BarrelWithWater;
    public GameObject Zelda;
    public GameObject Wheelchair;
    public GameObject Maria;
    public GameObject Ahmad;
    public GameObject StormCanvas;

    public GameObject SaveCorgiAppear;
    public GameObject Controller;
    public InformationCanvas Banner;

    private const string EventKey = "COMPOSTFINISHED";

    private bool firstVisit;
    private bool _satisfied;

    //-----Material Blinking-------
    public bool BlinkWhenPlayerNear = true;
    private Material mat_original;
    private Material mat_blink;
    private MeshRenderer _meshRenderer;
    private float timer = 0;
    private bool blinkOn = false;
    private bool playerInCollider = false;
    //-----------------------------

    //-----Item Manipulation------
    public Item[] itemToReceive = new Item[1];
    private InventoryHelper inventory;

    private bool hasItem;
    //---------------------------

    //-----Interaction Text-----
    public string InteractionDisplayText;
    [Header("if null, will attempt to find")]
    public InformationCanvas interactText;
    //---------------------------

    //------Event Methods--------
    [Header("Called when Player presses Interact Button (likely 'E')")]
    public UnityEvent CallOnInteract;
    [Header("Called when Player enters the trigger collider on this object")]
    public UnityEvent CallOnEnterCollider;
    //----------------------------

    [Header("Destroys script after use")]
    public bool killAfterUse = true;

    [Header("Destroys this gameObject after use")]
    public bool DestoryObjectAfterUse = false;

    private byte interactionDelayFrames = 0;
    private byte interactionDelayFramesMax = 20;

    private void Awake()
    {
        hasItem = (itemToReceive != null);
        if (hasItem && itemToReceive.Length > 0)
        {
            foreach (var item in itemToReceive)
            {
                if (item == null)
                {
                    hasItem = false;
                    Debug.LogWarning(name + "has non-zero item-to-give length, but one of the items is null!");
                    break;
                }
            }
        }
    }


    public void Start()
    {
        if (interactText == null)
            interactText = GameObject.Find("MiniGameClose").transform.Find("GUI").GetComponent<GuiDisplayer>().GetInteract();

        // get reference for inventory manipulation
        if (hasItem)
            inventory = Systems.Inventory;

        // materials for material blinking
        if (BlinkWhenPlayerNear)
        {
            mat_original = gameObject.GetComponent<MeshRenderer>().material;
            mat_blink = Resources.Load("Materials/Transparent Object 1", typeof(Material)) as Material;
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        firstVisit = true;
    }

    public void FixedUpdate() // Fixed update responds to timescale
    {
        if (BlinkWhenPlayerNear && playerInCollider)
        {
            timer += Time.deltaTime;
            if (timer > .6)
            {
                timer = 0;
                if (blinkOn)
                {
                    _meshRenderer.material = mat_original;
                    blinkOn = false;
                }
                else
                {
                    _meshRenderer.material = mat_blink;
                    blinkOn = true;
                }
            }
        }


        if (interactionDelayFrames <= 0 && playerInCollider && Input.GetAxis("Interact") > 0)
        {
            interactionDelayFrames = interactionDelayFramesMax;
            if (hasItem && itemToReceive != null)
            {
                foreach (var item in itemToReceive)
                    inventory.AddItem(item, 1);

                itemToReceive = null;
            }
            CallOnInteract.Invoke();


            if (firstVisit)
            {
                OnFirstVisit();
            }
            else if (Systems.Objectives.Check(EventKey))
            {
                OnVisitsAfterCompost();
            }
            else
            {
                OnVisitsBeforeCompost();
            }

            if (DestoryObjectAfterUse)
            {
                interactText.ToggleVisibility(false);
                Destroy(gameObject);
            }

            if (killAfterUse)
            {
                Kill();
            }

        }
        else if (interactionDelayFrames > 0)
        {
            interactionDelayFrames--;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (!playerInCollider && other.CompareTag("Player"))
        {
            CallOnEnterCollider.Invoke();

            interactText.ChangeText(InteractionDisplayText);

            playerInCollider = true;
            timer = .6f;
            blinkOn = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!playerInCollider && other.CompareTag("Player"))
        {
            interactText.ToggleVisibility(true);
            playerInCollider = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.ToggleVisibility(false);
            playerInCollider = false;
            if (BlinkWhenPlayerNear) _meshRenderer.material = mat_original;
        }
    }

    public void SetInteractText(string newText)
    {
        this.InteractionDisplayText = newText;
        interactText.ChangeText(InteractionDisplayText);
    }

    public void DeleteItems()
    {
        itemToReceive = null;
        hasItem = false;
    }

    public void StopBlink()
    {
        _meshRenderer.material = mat_original;
        blinkOn = false;
        BlinkWhenPlayerNear = false;
    }

    public void Kill()
    {
        Destroy(this);
    }

    public void OnFirstVisit()
    {
        StormCanvas.SetActive(true);

        //if (GameObject.Find("SchoolPointer") != null)
        //{ GameObject.Find("SchoolPointer").GetComponent<FlatFollow>().disappear(); }
        if (GameObject.Find("BarrelPointer") != null)
        { GameObject.Find("BarrelPointer").GetComponent<FlatFollow>().appear(); }
        if (GameObject.Find("AhmadAlert") != null)
        { GameObject.Find("AhmadAlert").GetComponent<FlatFollow>().appear(); }

        Systems.Status.AffectWarmth(50);
        //Systems.Status.AffectRelief(100);
        GameObject.Find("MeterDing").GetComponent<AudioSource>().Play();
        Systems.Status.SlowDownWarmthLoss();
        Banner.ChangeText("Talk to other survivors");

        interactText.ToggleVisibility(false);
        _meshRenderer.material = mat_original;

        Rain.SetActive(false);
        Systems.Objectives.Satisfy("RAINSTORM");
        BarrelWithoutWater.SetActive(false);
        Destroy(BarrelWithoutWater);
        BarrelWithWater.SetActive(true);

        Zelda.transform.eulerAngles = new Vector3(Zelda.transform.eulerAngles.x, Zelda.transform.eulerAngles.y + 180, Zelda.transform.eulerAngles.z);
        Maria.transform.position = new Vector3(-205, 0, -262);
        Maria.transform.Rotate(0f, 200f, 0f);
        Ahmad.transform.position = new Vector3(-209, 0, -254);
        Ahmad.transform.Rotate(0f, 130f, 0f);

        firstVisit = false;
    }

    public void OnVisitsAfterCompost()
    {
        SaveCorgiAppear.SetActive(true);
        Controller.SetActive(true);
        Systems.Status.AffectRelief(100);
        Systems.Status.AffectWarmth(50);
        GameObject.Find("MeterDing").GetComponent<AudioSource>().Play();
    }

    public void OnVisitsBeforeCompost()
    {
        Systems.Status.AffectWarmth(50);
        GameObject.Find("MeterDing").GetComponent<AudioSource>().Play();
    }
}
