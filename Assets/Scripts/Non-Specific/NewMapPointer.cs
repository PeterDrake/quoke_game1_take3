using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMapPointer : MonoBehaviour{

public GameObject following;


public float startingX;
public float startingZ;
public float currentX;
public float currentZ;
public float height;
public bool offAtStart;

public Vector3 startingLocation;
public Vector3 currentLocation;
public Vector3 dotLocation;
private Transform location;
private Color clearColor;
public Color realColor;

    // Start is called before the first frame update
    void Start()
    {
        location = GetComponent<Transform>();
        //location.transform.position = new Vector3
           // (following.transform.position.x, height, following.transform.position.z);
           startingLocation = new Vector3(following.transform.position.x, 0, following.transform.position.z);
           currentLocation = startingLocation;
           //dotLocation = new Vector3();
        clearColor = Color.clear;
        if (offAtStart) { disappear(); }
    }

    // Update is called once per frame
    void Update()
    {
        currentLocation.x = following.transform.position.x;
        currentLocation.z = following.transform.position.z;
        location.transform.position = new Vector3(currentX, 0, currentZ);
    }
    
    public void appear()
    {
        GetComponent<SpriteRenderer>().color = realColor;
    }
    public void disappear()
    {
        GetComponent<SpriteRenderer>().color = clearColor;
    }
}
