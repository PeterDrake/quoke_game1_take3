using UnityEngine;

/// <summary>
/// Used in conjunction with InteractWithObject
/// </summary>
public class VanNextLevel : MonoBehaviour
{
    private const string EventKey = "LEVELFINISHED";
    private const string SATISFIED = "Press 'E' to rest in Ahmad's van";
    private const string NOT_SATISFIED = "";
    public UIElement winCanvas;
    public GameObject Trophy;
    public GameObject levelMusic;
    public GameObject WinSound;
    private InteractWithObject _interact;
    private bool _satisfied;
    
    private void Start()
    {
        _interact = GetComponent<InteractWithObject>();
        Systems.Objectives.Register(EventKey,() => _satisfied = true);
    }


    public void OnEnter()
    {
        _interact.SetInteractText(_satisfied? SATISFIED:NOT_SATISFIED);
        _interact.BlinkWhenPlayerNear = _satisfied;
    }
    public void Interact()
    {
        if (_satisfied)
        {
            Systems.Status.Pause();
            UIManager.Instance.SetAsActive(winCanvas); //changed from winCanvas
            Trophy.SetActive(true);
            LogToServer logger = GameObject.Find("Logger").GetComponent<LogToServer>();
            logger.sendToLog("Completed Level 2!", "LEVEL");

            
            levelMusic.SetActive(false);
            WinSound.SetActive(true);
        }
    }
}
