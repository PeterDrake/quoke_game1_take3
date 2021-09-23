using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCount : MonoBehaviour
{
    public int level;
    public Text count;
    public Text label;

    void Start()
    {
        if (level == 1)
        {
            label.text = level.ToString();
            count.text = SavedData.levelOneDeath.ToString();
        }
        else if (level == 2)
        {
            label.text = level.ToString();
            count.text = SavedData.levelTwoDeath.ToString();
        }
        else if (level == 3)
        {
            label.text = level.ToString();
            count.text = SavedData.levelThreeDeath.ToString();
        }
        else if (level == 4)
        {
            label.text = level.ToString();
            count.text = SavedData.levelFourDeath.ToString();
        }
    }
}
