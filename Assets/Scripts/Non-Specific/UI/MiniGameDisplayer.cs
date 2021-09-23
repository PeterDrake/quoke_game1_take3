using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameDisplayer : UIElement
{
    private GameObject toggler;
    // Start is called before the first frame update
    void Start()
    {
        toggler = GameObject.Find("MiniGame");
        UIManager.Instance.Initialize(this);
    }

    // Update is called once per frame
    void Update()
    {

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
