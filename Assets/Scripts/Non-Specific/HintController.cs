using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintController : MonoBehaviour
{
    public GameObject[] tasks;
    public Text hintText;

    private int check = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        print("HINTS ARE " + SavedData.hints);
        SavedData.hints = false;
        hintText.text = "Turn Hints ON";

        print("starting CHECKLIST");
        for (int i = 0; i < tasks.Length; i++)
        {
            print("setting tasks #" + i);
            if (i == 0)
            {
                tasks[0].SetActive(true);
            }
            else
            {
                tasks[i].SetActive(false);
            }

            foreach(Transform child in tasks[i].transform)
            {
                if (SavedData.hints)
                {
                    print("task active = " + child.name);
                    child.GetComponent<FlatFollow>().appear();
                }
                else
                {
                    print("task NOT active = " + child.name);
                    child.GetComponent<FlatFollow>().disappear();
                }
            }
        }
    }

    public void ButtonClick()
    {
        //hints are off turning on
        if (!SavedData.hints){
            SavedData.hints = true;
            hintText.text = "Turn Hints OFF";
            print("HINTS ARE " + SavedData.hints);
            print("CHECKLIST UPDATED");
            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i].activeSelf)
                {
                    foreach(Transform child in tasks[i].transform)
                    {
                        print("Task turned active = " + child.name);
                        child.GetComponent<FlatFollow>().appear();
                    }
                }
                else
                {
                    foreach (Transform child in tasks[i].transform)
                    {
                        print("Task NOT active = " + child.name);
                        child.GetComponent<FlatFollow>().disappear();
                    }
                }
            }
        }
        // hints are on turnning off
        else{
            hintText.text = "Turn Hints ON";
            SavedData.hints = false;
            print("HINTS ARE " + SavedData.hints);
            print("CHECKLIST UPDATED");
            for (int i = 0; i < tasks.Length; i++)
            {
                foreach (Transform child in tasks[i].transform)
                {
                    print("Task NOT active = " + child.name);
                    child.GetComponent<FlatFollow>().disappear();
                }
            }
        }

    }

    public void NextTask()
    {
        check++;
        print("next task is #" + check);
        if (check > tasks.Length)
        {
            print("YOOU PASSED TASK LIMIT");
            return;
        }
        tasks[check].SetActive(true);
        foreach(Transform child in tasks[check].transform)
        {
            print(child.name + "is the next task");
            if (SavedData.hints)
            {
                child.GetComponent<FlatFollow>().appear();
            }
            else
            {
                child.GetComponent<FlatFollow>().disappear();
            }
        }
    }

    public void AllTaskCompleted(GameObject turnoff)
    {
        print("TURNING " + turnoff.name + " OFF");
        foreach(Transform child in turnoff.transform)
        {
            child.GetComponent<FlatFollow>().disappear();
        }
        turnoff.SetActive(false);
    }


    public void StartThisTask(string start)
    {
        for(int i = 0; i < tasks.Length; i++)
        {
            if(tasks[i].name == start)
            {
                tasks[i].SetActive(true);
                print("STARTING " + tasks[i].name);

                foreach (Transform child in tasks[i].transform)
                {
                    if (SavedData.hints)
                    {
                        child.GetComponent<FlatFollow>().appear();
                        print(child.name + " will appear");

                    }
                    else
                    {
                        print(child.name + "will not appear");
                        child.GetComponent<FlatFollow>().disappear();
                    }
                }
                return;
            }
        }
        
    }

}
