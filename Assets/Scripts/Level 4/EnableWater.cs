using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWater : MonoBehaviour
{
    public GameObject Water;
    public GameObject Water1;
    //public GameObject Pot;
    public InformationCanvas _canvas;
    public string words;
    public GameObject BruceController;
    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(EnableTheWater));
    }
    
    private IEnumerator EnableTheWater()
    {
        _canvas.ChangeText("Bring water to a rolling boil for 1 minute");
        yield return new WaitForSeconds(7f);
        Water1.SetActive(false);
        //Pot.SetActive(false);
        Water.SetActive(true);

        Systems.Objectives.Satisfy("PotWithWater");

        _canvas.ChangeText(words);
        GameObject.Find("LocationsOfInterest").GetComponent<HintController>().StartThisTask("GetWater");

        if (GameObject.Find("MariaAlert") != null)
        { GameObject.Find("MariaAlert").GetComponent<FlatFollow>().appear(); }
        BruceController.transform.position = new Vector3(-205, 0, -175);
        if (GameObject.Find("BrucePointer") != null)
        { GameObject.Find("BrucePointer").GetComponent<FlatFollow>().appear(); }
        if (GameObject.Find("BruceAlert") != null) 
        { GameObject.Find("BruceAlert").GetComponent<FlatFollow>().appear(); }
    }
}
