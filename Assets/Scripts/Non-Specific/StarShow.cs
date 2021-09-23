using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarShow : MonoBehaviour
{
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    public GameObject Star4;
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        if (level == 1)
        {
            SavedData.levelOneDone = true;
        }
        if (level == 2)
        {
            SavedData.levelTwoDone = true;
        }
        if (level == 3)
        {
            SavedData.levelThreeDone = true;
        }
        if (level == 4)
        {
            SavedData.levelFourDone = true;
        }


        if (SavedData.levelOneDone)
        {
            Star1.SetActive(true);
        }
        if (SavedData.levelTwoDone)
        {
            Star2.SetActive(true);
        }
        if (SavedData.levelThreeDone)
        {
            Star3.SetActive(true);
            Star2.SetActive(false);
        }

        if (SavedData.levelThreeDone && SavedData.levelTwoDone)
        {
            Star3.SetActive(true);
            Star2.SetActive(true);
        }

        if (SavedData.levelFourDone)
        {
            Star4.SetActive(true);
        }
    }
}
