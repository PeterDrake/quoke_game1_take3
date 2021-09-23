using UnityEngine;

public class WinDisplayer : UIElement
{
    [SerializeField] private GameObject toggler;

    private void Start()
    {
        locked = true;
        //pauseOnOpen = true;
    }

    public override void Open()
    {
        toggler.SetActive(true);
    }

    public override void Close()
    {
        toggler.SetActive(false);
    }
}
