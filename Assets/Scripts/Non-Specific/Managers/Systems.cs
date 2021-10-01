using UnityEngine;

public class Systems : MonoBehaviour
{
    public static Systems Instance;
    public static DialogueManager Dialogue;
    public static InventoryHelper Inventory;
    public static ObjectiveManager Objectives;
    public static StatusManager Status;
    public static InputManager Input;
    
    public DialogueManager dialogueManager;
    public InventoryHelper inventoryManager;
    public ObjectiveManager objectivesManager;
    public StatusManager statusManager;
    public InputManager inputManager;

    private void Awake()
    {
        // if one of the static references is not null, a instance of this class already exists
        if (Instance == null) Instance = this;
        else Destroy(this);

        Dialogue = dialogueManager;
        Inventory = inventoryManager;
        Objectives = objectivesManager;
        Status = statusManager;
        Input = inputManager;
    }
    
    public bool Pause(bool val)
    {
        if (val)
        {
            Time.timeScale = 0;
            return true;
        }
        
        Time.timeScale = 1f;
        return false;
    }
}
