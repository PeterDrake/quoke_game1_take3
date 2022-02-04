using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilWaterMaster : MonoBehaviour
{
   
    public InformationCanvas _canvas;
    private string words;
    private string words1;


    public Item Wood;
    public Item Pot;
    public Item PotWithWater;
    public AudioSource waterSplash;

    public GameObject BarrelWithWater;
    public GameObject Sphere;
    public GameObject Particles;
    public GameObject Controller;
    public GameObject Pot1;
    public GameObject Firecomplex;

    private int check=0;
    private InteractWithObject script;
    private InteractWithObject script1;

    // Start is called before the first frame update
    void Start()
    {
        

        script = BarrelWithWater.GetComponent<InteractWithObject>();
        script1 = Controller.GetComponent<InteractWithObject>();
        

    }

    public void FillPotWithWater()
    {
        Systems.Inventory.RemoveItem(Pot, 1);
        //Systems.Inventory.AddItem(PotWithWater, 1);
        Sphere.SetActive(true);
        Particles.SetActive(true);
        // if (GameObject.Find("BarrelPointer") != null)
        // { GameObject.Find("BarrelPointer").GetComponent<FlatFollow>().disappear(); }
        // if (GameObject.Find("BoilPointer") != null)
        // { GameObject.Find("BoilPointer").GetComponent<FlatFollow>().appear(); }
        waterSplash.Play();
        print("this is happening we fillingthe pot");

        

    }

    public void RemoveWood()
    {
        Systems.Inventory.RemoveItem(Wood, 1);
    }

    public void RemovePotWater()
    {
        Systems.Inventory.RemoveItem(PotWithWater, 1);
        script1.enabled = false;
        //Controller.SetActive(false);
        Pot1.SetActive(true);
        // Firecomplex.SetActive(false);
        Systems.Status.SlowDownHydrationLoss();
        Systems.Status.SlowDownReliefLoss();
    }

    // Update is called once per frame
    void Update()
    {
        if (Systems.Inventory.HasItem(Wood, 1)&& check==0)
        {
            script.enabled = true;
            check = 1;
        }

        words = "Build a fire and set up the pot to boil water";
        

        if (Systems.Inventory.HasItem(Resources.Load<Item>("Items/Wood"), 1)
                    && Systems.Inventory.HasItem(Resources.Load<Item>("Items/Pot"), 1))
        {
            _canvas.ChangeText(words);
        }

    }
}
