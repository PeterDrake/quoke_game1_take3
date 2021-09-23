using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : UIElement
{
    
    [Header("A prefab object which will be instantiated for each slot in the inventory")]
    [SerializeField] private GameObject SlotPrefab;

    public InvButtonFlash inventoryFlasher;

    // The inventory display script finds components based on name. If you want to create
    // a new inventory display, see the Basic Inventory Display for the proper names and locations in
    // the hierarchy for the UI elements (with respect to the InventoryToggler)

    private byte requiredComponentsAmount = 13;
    private GameObject toggler;

    private Image displayImage;
    private Image[] itemSlots;
    private Text description;
    private Text displayName;
    private Text displayAmount;

    private GameObject useToggle;
    private Text useText;
    private GameObject openToggle;
    private GameObject openImage;
    private Text openText;
   
    private Pamphlet pamControl;

    private int selectedItem;

    private byte capacity;
    private Item[] items;
    private byte[] amounts;

    private MenuDisplayer menu;

    

    public override void Open()
    {
        Load(Systems.Inventory.GetItems(), Systems.Inventory.GetAmounts());
    }

    private void Load(Item[] items, byte[] amounts)
    {
        activate(true);
        useToggle.SetActive(false);
        openToggle.SetActive(false);
        this.items = items;
        this.amounts = amounts;

        if (selectedItem != -1 && amounts[selectedItem] == 0) selectedItem = -1;
        
        int i = 0;
        bool first = false;

        foreach (Image item in itemSlots)
        {
            if (i >= items.Length) break;
            if (amounts[i] == 0)
            {
                item.enabled = false;
            }
            else
            {
                item.enabled = true;
                item.sprite = items[i].Icon;
                if (!first && selectedItem == -1)
                {
                    selectedItem = i;
                    first = true;
                }                
            }
            i += 1;
        }
        if (amounts.Max() != 0) // if there is something in the inventory
            setActiveItem(selectedItem);

        if (amounts.Max() == 0)
        {
            displayName.text = "Empty";
            description.text = "There is nothing in your inventory!";
            displayImage.sprite = Resources.Load<Sprite>("Sprites/Items/displayimage");
            displayAmount.text = "0";
        }
    }

    private void Start()
    {
        pauseOnOpen = true;
        locked = true;
        
        selectedItem = 0;
        // capacity = Systems.Inventory.GetCapacity();
        capacity = 8; // Temporary kludge
        itemSlots = new Image[capacity];
        amounts = new byte[]{0};
        items = new Item[capacity];

        menu = GameObject.Find("Basic Pause Menu").GetComponent<MenuDisplayer>();

        initialize();
        activate(false);
        Systems.Input.RegisterKey("i", delegate
            {
                UIManager.Instance.ToggleActive(this);
                inventoryFlasher.ThrobOff();
            }
            );
    }

    private void initialize() //Get all references that are needed to populate the dialogue UI
    {
        Transform main = transform.Find("InventoryToggler");
        toggler = main.gameObject;
        byte componentsFound = 1;
        
        foreach (Transform child in main)
        {
            switch (child.name)
            {
                case "itemPanel":
                    componentsFound += 1;
                    for (int i = 0; i < capacity; i++)
                    {
                        GameObject inst = Instantiate(SlotPrefab, child);
                        itemSlots[i] = inst.transform.Find("icon").GetComponent<Image>();
                        
                        int k = i;
                        inst.GetComponent<Button>().onClick.AddListener(delegate {setActiveItem(k);});
                    }
                    break;
                case "displayImage":
                    componentsFound += 1;
                    displayImage = child.GetComponent<Image>();
                    break;
                case "displayAmount":
                    componentsFound += 1;
                    displayAmount = child.GetComponent<Text>();
                    break;
                case "description":
                    componentsFound += 1;
                    description = child.GetComponent<Text>();
                    break;
                case "displayName":
                    componentsFound += 1;
                    displayName = child.GetComponent<Text>();
                    break;
                case "exitButton":
                    componentsFound += 1;
                    child.GetComponent<Button>().onClick.AddListener(UIManager.Instance.ActivatePrevious);
                    break;
                case "useToggler":
                    componentsFound += 3;
                    useToggle = child.gameObject;
                    Transform button = child.Find("use");
                    button.GetComponent<Button>().onClick.AddListener(useSelectedItem);
                    useText = button.Find("text").GetComponent<Text>();
                    break;
                case "openToggler":
                    componentsFound += 3;
                    openToggle = child.gameObject;
                    Transform button2 = child.Find("open");
                    pamControl = child.Find("open").GetComponent<Pamphlet>();
                    openImage = button2.Find("Image").gameObject;
                    openText = button2.Find("text").GetComponent<Text>();
                    break;
            }
        }
        if (componentsFound != requiredComponentsAmount) 
            throw new Exception("Required Components for Dialogue Displayer were not found!");
    }
    private void setActiveItem(int i)
    {
        if (items[i] == null) return;

        selectedItem = i;
        
        displayImage.sprite = items[i].DisplayImage;
        displayName.text = items[i].DisplayName;
        description.text = items[i].Description;
        displayAmount.text = amounts[i].ToString();
        itemSlots[i].color = new Color(131,171,255);

        if (items[i].ID == "PAM")
        {
            openImage.SetActive(false);
            useToggle.SetActive(false);
            openToggle.SetActive(true);
            openText.text = "Open Pamphlet";

        }

        else if (items[i].action != null)
        {
            openToggle.SetActive(false);
            useToggle.SetActive(true);
            useText.text = items[i].action.ActionWord;
        }
        else
        {
            useToggle.SetActive(false);
            openToggle.SetActive(false);
        }
    }
    
    public override void Close()
    {
        activate(false);   
    }
    
    private void activate(bool active)
    {
        toggler.SetActive(active);
        if (active) { menu.openedCanvi(this); }
        else { menu.closedCanvi(); }
    }

    private void useSelectedItem()
    {
        items[selectedItem].action.Use(ref items[selectedItem]);
       UIManager.Instance.SetAsActive(this);
    }

}
