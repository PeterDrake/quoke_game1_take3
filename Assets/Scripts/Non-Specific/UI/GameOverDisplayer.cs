using UnityEngine;

public class GameOverDisplayer : UIElement
{
    [SerializeField] private GameObject toggler;

    private void Start()
    {
        UIManager.Instance.Initialize(this);
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
