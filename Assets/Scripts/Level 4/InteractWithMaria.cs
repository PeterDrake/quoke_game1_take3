using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Implements the basic functionality for "Player walks up to object, presses 'e' and something happens"
/// </summary>
public class InteractWithMaria : MonoBehaviour
{

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
            print("Zelda entered");
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
        interactText.ToggleVisibility(false);
        if (_meshRenderer.material != null)
        {
            if (mat_original != null)
            {
                _meshRenderer.material = mat_original;
            }
        }
        Destroy(this);
    }
}
