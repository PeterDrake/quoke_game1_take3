using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class FillWater : MonoBehaviour
{
    public WaterHeaterMaster Master;
    public Image water;
    public GameObject button;
    private LogToServer logger;

    public void pourWater()
    {
        
        logger = GameObject.Find("Logger").GetComponent<LogToServer>();
        StartCoroutine(WaitToFillWater());
        button.SetActive(false);
        Debug.Log("Filled water");
        logger.sendToLog("Filled water","MINIGAME");
    }
    public IEnumerator WaitToFillWater()
    {
        while (water.fillAmount < .75f)
        {
            water.fillAmount += Mathf.Lerp(0f, .8f, Time.deltaTime*10);
            yield return new WaitForSeconds(.5f);
        }
        water.fillAmount = 1f;
        yield return new WaitForSeconds(1f);
        Master.StopCoroutine(Master.TryAgain());
        Master.StopCoroutine(Master.BlinkText());
        Debug.Log("Won Water Heater minigame");
        logger.sendToLog("Won Water heater minigame", "MINIGAME");
        Master.Win.SetActive(true);
        Master.Canvas.SetActive(false);
        GameObject.Find("ImportantObjects").GetComponent<MiniWin>().MiniGameWon();
        StopCoroutine(WaitToFillWater());
     
        

    }

}
