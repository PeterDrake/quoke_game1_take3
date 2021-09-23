using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WaterHeaterMaster : MonoBehaviour
{
    public GameObject Win;
    //public GameObject WinSound;
    public GameObject Wrong;
    public GameObject Canvas;

    public UnityAction OnWin;
    public UnityAction OnExit;

    public GameObject ElectricBoxCheck;
    public GameObject WaterPipeCheck;
    public GameObject AirPipeCheck;
    public GameObject WaterSpoutCheck;

    public bool stepOne;
    public bool stepTwo;
    public bool stepThree;
    public bool stepFour;

    public Button FillButton;
    public GameObject Fill;

    private GameObject ElectricBox;
    private GameObject WaterPipe;
    private GameObject AirPipe;
    private GameObject WaterSpout;

    public GameObject PIP1;
    public GameObject PIP2;
    public GameObject PIP3;
    public GameObject PIP4;

    public GameObject RawImage;
    public GameObject RawImage1;
    public GameObject RawImage2;
    public GameObject RawImage3;

    private LogToServer logger;

    private void Start()
    {
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        stepOne = false;
        stepTwo = false;
        stepThree = false;
        stepFour = false;
        ElectricBoxCheck.SetActive(false);
        WaterPipeCheck.SetActive(false);
        AirPipeCheck.SetActive(false);
        WaterSpoutCheck.SetActive(false);
        Fill.SetActive(false);

        //PIP1.SetActive(false);
        //PIP2.SetActive(false);
        //PIP3.SetActive(false);
        //PIP4.SetActive(false);

        ElectricBox = GameObject.FindGameObjectWithTag("ElectricBox");
        WaterPipe = GameObject.FindGameObjectWithTag("WaterPipe");
        AirPipe = GameObject.FindGameObjectWithTag("AirPipe");
        WaterSpout = GameObject.FindGameObjectWithTag("WaterSpout");
    }


    public int CheckAnswers(string tag)
    {
        //next step to add check
        if ((tag == "ElectricBox" && !stepOne && !stepTwo && !stepThree && !stepFour )
        || (tag == "WaterPipe" && stepOne && !stepTwo && !stepThree && !stepFour)
        || (tag == "AirPipe" && stepOne && stepTwo && !stepThree && !stepFour)
        || (tag == "WaterSpout" && stepOne && stepTwo && stepThree && !stepFour))
        {
            //Debug.Log("Good step");
            return 1;
        }
        //same step thats already check
        else if ((tag == "ElectricBox" && stepOne)
        || (tag == "WaterPipe" && stepOne && stepTwo)
        || (tag == "AirPipe" && stepOne && stepTwo && stepThree)
        || (tag == "WaterSpout" && stepOne && stepTwo && stepThree && stepFour))
        {
            return 0;
        }
        else
        {
            return -1;
        }
    }

    public void isDone()
    {
        if(stepOne && stepTwo && stepThree && stepFour)
        {
            //Destroy(ElectricBox.GetComponent<MouseInteract>());
            //Destroy(WaterPipe.GetComponent<MouseInteract>());
            //Destroy(AirPipe.GetComponent<MouseInteract>());
            //Destroy(WaterSpout.GetComponent<MouseInteract>());
            Destroy(ElectricBox.GetComponent<EachStepAction>());
            Destroy(WaterPipe.GetComponent<EachStepAction>());
            Destroy(AirPipe.GetComponent<EachStepAction>());
            Destroy(WaterSpout.GetComponent<EachStepAction>());

            Fill.SetActive(true);
            StartCoroutine(BlinkText());
        }
    }

    public void resetSteps()
    {
        StartCoroutine(TryAgain());
        if (stepOne)
        {
            GameObject.Find("Flip").transform.Rotate(0, -150, 0);
        }
        if (stepTwo)
        {
            GameObject.Find("Turn").transform.Rotate(0, 0, 360);
        }
        if (stepThree)
        {
            GameObject.Find("Lever").transform.Rotate(0, 0, 90);
        }
        if (stepFour)
        {
            GameObject.Find("Water").transform.Rotate(0, 0, -500);
        }

        stepOne = false;
        stepTwo = false;
        stepThree = false;
        stepFour = false;
        ElectricBoxCheck.SetActive(false);
        WaterPipeCheck.SetActive(false);
        AirPipeCheck.SetActive(false);
        WaterSpoutCheck.SetActive(false);

        PIP1.SetActive(false);
        PIP2.SetActive(false);
        PIP3.SetActive(false);
        PIP4.SetActive(false);

    }

    public void Leave()
    {
        OnExit.Invoke();
    }

    public void WinLeave()
    {
        OnWin.Invoke();
    }

    public IEnumerator TryAgain()
    {
        RawImage.SetActive(false);
        RawImage1.SetActive(false);
        RawImage2.SetActive(false);
        RawImage3.SetActive(false);
        Debug.Log("try again message");
        logger.sendToLog("try again message","MINIGAME");
        Wrong.SetActive(true);
        yield return new WaitForSeconds(2f);
        Wrong.SetActive(false);
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            FillButton.GetComponent<UnityEngine.UI.Image>().color = Color.blue;
            FillButton.GetComponentInChildren<Text>().color = Color.white;
            yield return new WaitForSeconds(.5f);
            FillButton.GetComponent<UnityEngine.UI.Image>().color = new Color(80,130,255,255);
            FillButton.GetComponentInChildren<Text>().color = Color.black;
            yield return new WaitForSeconds(.5f);
        }
    }

}
