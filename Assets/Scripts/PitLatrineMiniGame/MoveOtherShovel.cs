using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOtherShovel : MonoBehaviour
{
    private float movementSpeed = 2f;
    private bool Check;
    private bool Check1;

    public GameObject Pit1;
    public GameObject Pit2;
    public GameObject Pit3;
    public GameObject Dirt1;
    public GameObject Dirt2;
    public GameObject Dirt3;
    public GameObject Dirt4;
    public GameObject Dirt5;    //Dirt on the shovel
    public GameObject Depth1;
    public GameObject Depth2;
    public GameObject Depth3;
    public GameObject Depth4;
    public GameObject Depth5;

    private LogToServer logger;
    public static int digCount = 0;
    public void Dig()
    {
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        digCount++;
        Debug.Log("Dug - hole depth " + digCount);
        StartCoroutine(nameof(DigVertically));
    }
    
    private IEnumerator DigVertically()
    {
        //Moves shovel down
        while (transform.position.y > 1f)
        {
            Vector3 Target = new Vector3(transform.position.x, -5, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, Target, movementSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.0005f);
        }
        
        yield return new WaitForSeconds(0.5f);
        
        MakePitAppear();
        
        Dirt5.SetActive(true); //Dirt on the shovel

        //Moves shovel to the side
        while (transform.position.x > 77 && transform.position.y<1.41)
        {
            Vector3 Target = new Vector3(77, 1.4f, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, Target, movementSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.0005f);
        }

        Dirt5.SetActive(false);
        MakeDirtAppear();
        
        //Moves shovel to the start position
        transform.position = new Vector3(78.34f, 1.42f, transform.position.z);
        
    }

    private void MakePitAppear()
    {
        if (Pit3.activeSelf)
        {
            //Pit2.SetActive(false) happens in Erosion script attached to Pit3
            Check = false;    //Check is used to see if Pit2 is active AND increased in size, which is case3
            //Disabling Depth4 happens in Erosion script
        }
        
        else if (Pit2.activeSelf && Check1)
        {
            Check1 = false;
            Depth4.SetActive(false);
            Depth5.SetActive(true);
            Pit3.SetActive(true);
            Pit3.transform.localScale = new Vector3(0.2f, 0.01f, 0.12f);
        }
        
        else if (Pit2.activeSelf && Check)
        {
            
            Check = false;
            Check1 = true;
            Depth4.SetActive(true);
            Depth3.SetActive(false);
        }
        
        else if (Pit2.activeSelf)
        {
            Check = true;
            Pit2.transform.localScale = new Vector3(0.16f, 0.01f, 0.09f);
            Depth3.SetActive(true);
            Depth2.SetActive(false);
        }
        
        else if (Pit1.activeSelf)
        {
            Pit2.transform.localScale = new Vector3(0.1f, 0.01f, 0.09f);
            Pit2.SetActive(true);
            Depth2.SetActive(true);
        }
        else
        {
            Depth2.SetActive(false);
            Depth3.SetActive(false);
            Depth4.SetActive(false);
            Pit1.SetActive(true);
            Depth1.SetActive(true);
        }
    }

    private void MakeDirtAppear()
    {
        if (Dirt3.activeSelf)
        {
            Dirt4.SetActive(true);
        }
        
        else if (Dirt2.activeSelf)
        {
            Dirt3.SetActive(true);
        }
        
        else if (Dirt1.activeSelf)
        {
            Dirt2.SetActive(true);
        }
        else
        {
            Dirt1.SetActive(true);
        }
    }

    public static void ResetDigCount()
    {
        digCount = 0;
        GameObject.Find("5 feet deep").SetActive(false);
    }
}
