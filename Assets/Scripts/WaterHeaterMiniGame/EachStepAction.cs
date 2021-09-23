using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class EachStepAction : MonoBehaviour
{
    public GameObject check;
    public GameObject current;

    public GameObject PIP;

    public WaterHeaterMaster Master;

    private KnobRotating move;

    public GameObject rawimage;

    private LogToServer logger;

    private void Awake()
    {
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        check.SetActive(false);
        PIP.SetActive(false);
        rawimage.SetActive(false);
    }

    public void OnMouseDown()
    {
        if (Master.CheckAnswers(current.tag) == 1)
        {
            if (current.tag == "ElectricBox")
            {
                Debug.Log("Electric Box selected");
                logger.sendToLog("Electrical Box selected","MINIGAME");
                GameObject.Find("Flip").AddComponent<KnobRotating>();
                Master.stepOne = true;

            }
            else if (current.tag == "WaterPipe")
            {
                Debug.Log("Water Pipe selected");
                logger.sendToLog("Water pipe selected","MINIGAME");
                GameObject.Find("Turn").AddComponent<KnobRotating>();
                Master.stepTwo = true;

            }
            else if (current.tag == "AirPipe")
            {
                Debug.Log("Air Pipe Selected");
                logger.sendToLog("Air pipe selected","MINIGAME");
                GameObject.Find("Lever").AddComponent<KnobRotating>();
                Master.stepThree = true;

            }
            else if (current.tag == "WaterSpout")
            {
                Debug.Log("Water spout selected");
                logger.sendToLog("Water spout selected","MINIGAME");
                GameObject.Find("Water").AddComponent<KnobRotating>();
                Master.stepFour = true;

            }
            //.Log("CHECKED OFF " + current.tag);
            check.SetActive(true);
            PIP.SetActive(true);
            rawimage.SetActive(true);
            check.GetComponent<Image>().color = Color.green;
            Master.isDone();

        }
        if (Master.CheckAnswers(current.tag) == -1)
        {
            //Master.TryAgain();
           
            Master.resetSteps();
            Debug.Log("Wrong step - Steps reset");
            logger.sendToLog("Wrong step - steps reset","MINIGAME");
            //Debug.Log("Wrong Step");
            
        }

        if (Master.CheckAnswers(current.tag) == 0)
        {
            Debug.Log("Already Clicked");
        }
       
    }
}
