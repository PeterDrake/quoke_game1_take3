using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostingMaster : MonoBehaviour
{
    public GameObject cube;
    public GameObject box;
    public GameObject carbon;
    public GameObject circle;
    public InformationCanvas _interact;
    public InformationCanvas _banner;
    public GameObject CorgiMinigame;
    public GameObject Frank;


    private Item boards;
    private Item mulch;
    private Item paper;
    public bool made;

    // Start is called before the first frame update
    void Start()
    {
        boards = Resources.Load<Item>("Items/Boards");
        mulch = Resources.Load<Item>("Items/Mulch");
        paper = Resources.Load<Item>("Items/ShreddedPaper");
        
    }

    public void BuildBox()
    {
        if (!made)
        {
            
            if (Systems.Inventory.HasItem(boards, 1))
            {
                Systems.Inventory.RemoveItem(boards, 1);
                box.GetComponent<MeshRenderer>().enabled = true;
                made = true;
                cube.transform.position = new Vector3(cube.transform.position.x, .5f, cube.transform.position.z);
                cube.GetComponent<InteractWithObject>().SetInteractText("Press 'E' to add carbon material to box");

            }
            else
            {
                cube.GetComponent<InteractWithObject>().SetInteractText("You need to gather some wood");
            }
        }
    }

    public void AddCarbon()
    {
        if (made)
        {
            if (Systems.Inventory.HasItem(mulch, 1) && Systems.Inventory.HasItem(paper, 1))
            {

                carbon.SetActive(true);
                Systems.Objectives.Satisfy("COMPOSTFINISHED");
                Systems.Objectives.Satisfy("HasBuiltCompostingSystem");
                if (GameObject.Find("BruceAlert") != null) { GameObject.Find("BruceAlert").GetComponent<FlatFollow>().appear(); }
                _banner.ChangeText("Talk to Bruce");
                Systems.Inventory.RemoveItem(mulch, 1);
                Systems.Inventory.RemoveItem(paper, 1);
                
                CorgiMinigame.SetActive(true);
                Frank.SetActive(false);

                
                _interact.ToggleVisibility(false);
                circle.SetActive(false);
                Destroy(cube);
                
            }
            else
            {
                cube.GetComponent<InteractWithObject>().SetInteractText("You need to gather more carbon material");
            }
        }
    }


    public void SwapText()
    {
        if (!made)
        {
            cube.GetComponent<InteractWithObject>().SetInteractText("Press 'E' to build a box");
        }
        else
        {
            cube.GetComponent<InteractWithObject>().SetInteractText("Press 'E' to add carbon material to box");

        }
    }
        
}
