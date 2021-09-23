using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatFollow : MonoBehaviour
{
    public GameObject following;
    public float height;
    public bool mobile;
    public bool offAtStart;
    public Color realColor;

    public float xBoundSmaller;
    public float xBoundBigger;
    public float zBoundSmaller;
    public float zBoundBigger;

    private float theX;
    private float theZ;

    private Transform location;
    private Color clearColor;

    // Start is called before the first frame update
    void Start()
    {
        location = GetComponent<Transform>();
        location.transform.position = new Vector3
            (following.transform.position.x, height, following.transform.position.z);
        clearColor = Color.clear;
        if (offAtStart) { disappear(); }
    }

    // Update is called once per frame
    void Update()
    {
        if (mobile)
        {
            StartCoroutine(follow());
        }
    }

    private IEnumerator follow()
    {
        while (true)
        {
            if (following.transform.position.x < xBoundSmaller) { theX = xBoundSmaller; }
            else if (following.transform.position.x > xBoundBigger) { theX = xBoundBigger; }
            else { theX = following.transform.position.x; }

            if (following.transform.position.z < zBoundSmaller) { theZ = zBoundSmaller; }
            else if (following.transform.position.z > zBoundBigger) { theZ = zBoundBigger; }
            else { theZ = following.transform.position.z; }


            location.transform.position = new Vector3
                (theX, height, theZ);
            yield return new WaitForSeconds(0.01f);
        }
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
