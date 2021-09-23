using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EscapeCanvi : UIElement
{

    public GameObject toggler;

    // Start is called before the first frame update
    void Start()
    {
        //Systems.Input.RegisterKey("escape", delegate { UIManager.Instance.ToggleActive(this); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Open()
    {
        activate(true);
    }
    public override void Close()
    {
        activate(false);
    }

    private void activate(bool active)
    {
        toggler.SetActive(active);
    }
}
