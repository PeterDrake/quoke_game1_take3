using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsLocationManager : MonoBehaviour
{
    public GameObject[] tasks;
    private int check;

    // Start is called before the first frame update
    void Start()
    {
        print("STARTINGGGG CHECKLIST");
        check = 0;
        for (int i = 0; i < tasks.Length; i++)
        {
            print("setting tasks #" + i);
            if (i == 0 && SavedData.hints)
            {
                tasks[0].SetActive(true);
                foreach (Transform child in tasks[0].transform)
                {
                   print("task active =" + child.name);
                   child.GetComponent<FlatFollow>().appear();
                    
                }
            }
            else
            {
                foreach (Transform child in tasks[i].transform)
                {
                    print("task active =" + child.name);
                    child.GetComponent<FlatFollow>().disappear();

                }
                print(tasks[i].name + "not active");
                tasks[i].SetActive(false);
            }
        }
    }


    public void nextTask()
    {
        check++;
        print("next task is #" + check);
        if (check > tasks.Length)
        {
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

    public void TasksOn()
    {
        print("turning the points on for whatever task we are on");
        for (int i = 0; i < tasks.Length; i++)
        {
            if (tasks[i].activeSelf)
            {
                print(tasks[i].name + " is on");
                foreach (Transform child in tasks[i].transform)
                {
                    child.GetComponent<FlatFollow>().appear();
                }
            }
        }
    }

    public void TaskCompleted(GameObject last)
    {
        print("turning off " + last.name);
        last.SetActive(false);
    }
}
