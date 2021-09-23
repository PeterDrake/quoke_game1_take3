using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameColorChange : MonoBehaviour
{
    public GameObject frame1;
    public GameObject frame2;
    public GameObject frame3;
    public GameObject frame4;

    private float timer;
    private bool blinkOn;

    private void Start()
    {
        frame1.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Ground");
        frame2.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Ground");
        frame3.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Ground");
        frame4.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Ground");
    }

    public void Blink()
    {
        frame1.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Mirror");
        frame2.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Mirror");
        frame3.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Mirror");
        frame4.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Mirror");


    }

    public void Empty()
    {
        frame1.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Ground");
        frame2.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Ground");
        frame3.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Ground");
        frame4.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Ground");
    }

    public void Correct()
    {
        frame1.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Green");
        frame2.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Green");
        frame3.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Green");
        frame4.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Green");
    }

    public void Wrong()
    {
        frame1.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Red");
        frame2.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Red");
        frame3.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Red");
        frame4.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Red");
    }
}
