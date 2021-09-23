using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SegueDisplayer : UIElement
{

    //[Header("A prefab object which will be instantiated for each slot in the inventory")]

    private GameObject toggler;
    public Button ExitButton;
    public Button NextButton;
    public Button BackButton;
    public GameObject page;
    //public float WaitTime;


    /*
    private int WaitForIt()
    {
        yield return new WaitForSeconds(WaitTime);
    }
    */
    private void Start()
    {
        pauseOnOpen = true;
        initialize();
        toggler.SetActive(true);

        //if (ExitButton != null)
        //{
        //    EventSystem.current.SetSelectedGameObject(null);
        //    EventSystem.current.SetSelectedGameObject(ExitButton.gameObject);
        //}

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ExitButton.gameObject);

        //UIManager.Instance.Initialize(this);
        //WaitForIt();
        //activate(false);

    }

    private void initialize() //Get all references that are needed to populate the dialogue UI
    {
        Transform main = transform.Find("SegueToggler");
        toggler = main.gameObject;
        //ExitButton.onClick.AddListener(delegate
        //{ UIManager.Instance.ToggleActive(this); });
        //ExitButton.onClick.AddListener(UIManager.Instance.ActivatePrevious);
        //byte componentsFound = 1;
    }

    public override void Open()
    {
        toggler.SetActive(true);
    }

    public override void Close()
    {
        toggler.SetActive(false);
        Destroy(this);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ExitButton.onClick.Invoke();
        }

        if (page != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && page.activeSelf)
            {
                BackButton.onClick.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && !page.activeSelf)
            {
                NextButton.onClick.Invoke();
            }
        }

    }

}