using UnityEngine;

public class Bookcase : MonoBehaviour
{
    public AudioSource drillSound;
    public GameObject supports;

    [Header("Will check for this item to repair bookshelf")]
    public Item CheckItem;

    public Item hasBleach;
    public Item hasTrashBag;
    public Item hasToiletPaper;
    public Item hasHandSanitizer;
    
    private const string NO_TOOLS = "This bookcase could fall over in an earthquake. I should find some tools and secure it to the wall.";
    private  const string HAS_TOOLS = "Press 'E' to secure the bookshelf";
    
    [Header("The bookcase will fall on the player the (kill_count)th time the player enters the collider")]
    public int KillCount = 4;
    private float fallThrust = 900000;

    private int count = 0;
    
    private InteractWithObject _interact;
    private InventoryHelper _inventory;

    private bool PlayerHasItem = false;

    private Rigidbody rb;
    private bool isFalling = false;
    private BoxCollider fallCollider;
    public BoxCollider TriggerCollider;


    private Vector3 vel;
    private Vector3 ang;

    private bool secure = false;
    private bool _satisfied;

    [Header("Time it takes to trigger the earthquake after the bookcase is secured")]
    public float TriggerTime = 5f;
    
    private void Start()
    {
        _interact = GetComponent<InteractWithObject>();
        _inventory = Systems.Inventory;
        if (CheckItem == null) Debug.LogError("No item to check has been specified");
        rb = GetComponent<Rigidbody>();
        
        fallCollider = transform.Find("Fall Collider").GetComponent<BoxCollider>(); 
        fallCollider.gameObject.GetComponent<CollisionCallback>().AddCallback("Player", HitPlayer);
        fallCollider.enabled = false;

        Systems.Objectives.Register("BOOKCASE", (() => _satisfied = true));

        //QuakeManager.Instance.OnQuake.AddListener(Fall);
    }

    public bool HasEverything()
    {
        if(
            (_inventory.HasItem(hasBleach, 1))
             && (_inventory.HasItem(hasHandSanitizer, 1))
                 && (_inventory.HasItem(hasToiletPaper, 1))
                         && (_inventory.HasItem(hasTrashBag, 1))) 
        {
            return true;
        }
        return false;
    }
    
    public void UpdateState()
    {
        if (isFalling) return;

        if (secure && HasEverything())
        {
            Destroy(TriggerCollider);
            QuakeManager.Instance.TriggerQuake();

        }

        if (count < KillCount)
        {
            if (secure)
            {
                _interact.BlinkWhenPlayerNear = false;
                _interact.SetInteractText("");
            }
            else if (PlayerHasItem || _inventory.HasItem(CheckItem, 1))
            {
                _interact.BlinkWhenPlayerNear = true;
                _interact.SetInteractText(HAS_TOOLS);
                PlayerHasItem = true;
            }
            else
            {
                _interact.BlinkWhenPlayerNear = false;
                _interact.SetInteractText(NO_TOOLS);
            }

            count++;
            Debug.Log(count);
            
        }
        else if (!secure)
        {
            Destroy(TriggerCollider);
            QuakeManager.Instance.TriggerQuake();
 
        }


    }

    public void Interaction()
    {
        if (!secure && !isFalling && PlayerHasItem)
        {
            _interact.StopBlink();
            _interact.SetInteractText("");
            Systems.Objectives.Satisfy("BOOKCASE");
            Systems.Inventory.RemoveItem(CheckItem, 1);
            supports.SetActive(true);
            if (GameObject.Find("BookcasePointer") != null)
            {
                GameObject.Find("BookcasePointer").GetComponent<FlatFollow>().disappear();
                GameObject.Find("BookcasePointer").SetActive(false);
            }
            drillSound.Play();
            Disable(); 
            // If player has everything, have a quake soon
            if (HasEverything())
            {
                QuakeManager.Instance.TriggerCountdown(TriggerTime);
            }
        }
    }
    

    public void Fall()
    {
        if (secure || isFalling) return;
        isFalling = true;
        
        fallCollider.enabled = true;
        rb.isKinematic = false;
        _interact.BlinkWhenPlayerNear = false;
        rb.AddRelativeTorque(new Vector3(1,0,0) * fallThrust,ForceMode.VelocityChange);
    }


    private void Disable()
    {
        
        Destroy(rb);
        // Destroy(fallCollider.gameObject.GetComponent<CollisionCallback>());
        secure = true;
    }


    private void HitPlayer()
    {
        if (!isFalling) return;
        if (rb.velocity.magnitude <= 0) Disable();
        else if (isFalling)
        {
            Debug.Log("Player Hit");
            Systems.Status.PlayerDeath("Crushed by bookcase","There was an earthquake and your bookcase crushed you :(");            
        }
    }
}
