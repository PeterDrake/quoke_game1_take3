using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A manager to keep track of GUI elements (status bars, text banner, interact text & misc buttons (inventory))
/// </summary>
public class GuiDisplayer : UIElement
{
    private GameObject toggler;
    public Button SegueButton;
    [SerializeField] private InformationCanvas Banner;
    [SerializeField] private InformationCanvas Interact;
    
    private void Start()
    {
        pauseOnOpen = true;
        toggler = transform.Find("GUIToggler").gameObject;
        UIManager.Instance.Initialize(this);
        toggler.SetActive(false);
        SegueButton.onClick.AddListener(
        delegate{ toggler.SetActive(true);
            pauseOnOpen = false;
            Systems.Instance.Pause(this.PauseOnOpen());
        });
    }

    public override void Open()
    {
        toggler.SetActive(true);

    }

    public override void Close()
    {
        toggler.SetActive(false);
    }

    public InformationCanvas GetBanner()
    {
        return Banner;
    }
    
    public InformationCanvas GetInteract()
    {
        return Interact;
    }
}