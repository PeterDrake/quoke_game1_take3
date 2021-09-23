using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Threading;

/// <summary>
/// Manager class for killing the player and displaying death screen 
/// </summary>
public class DeathDisplay : UIElement
{
    public GameObject player;
    public GameObject toggle;
    public Text deathText;
    public float waitTime;
    public GameObject explosion;
    public AudioSource DeathMusic;
    public GameObject DMusic;
    public GameObject OtherMusic;
    public int level;

    private bool dead;
   
    public void Start()
    {
       //pauseOnOpen = true;
       forceOpen = true;
       locked = true;
       toggle.SetActive(false);
    }
    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        print("Died but HINTS ARE " + SavedData.hints);

    }

    public void Activate(string text)
    {
        player.SetActive(false);
        if (level == 1)
        {
            SavedData.levelOneDeath++;
        }
        else if (level == 2)
        {
            SavedData.levelTwoDeath++;
        }
        else if (level == 3)
        {
            SavedData.levelThreeDeath++;
        }
        else if (level == 4)
        {
            SavedData.levelFourDeath++;
        }
        print("Level 1 Deaths = " + SavedData.levelOneDeath);
        print("Level 2 Deaths = " + SavedData.levelTwoDeath);
        print("Level 3 Deaths = " + SavedData.levelThreeDeath);
        print("Level 4 Deaths = " + SavedData.levelFourDeath);

        deathText.text = text;
        DMusic.SetActive(true);
        OtherMusic.SetActive(false);
        DeathMusic.Play();
        StartCoroutine(nameof(WaitThenShow), waitTime);
    }

    private IEnumerator WaitThenShow(float time)
    {
        float theTime = time;
        while (theTime>0)
        {
            yield return new WaitForSeconds(0.1f);
            theTime--;
        }
        UIManager.Instance.SetAsActive(this);
        while (theTime<time)
        {
            yield return new WaitForSeconds(0.5f);
            theTime++;
        }
        Destroy(explosion);
    }

    public override void Open()
    {
        toggle.SetActive(true);
    }

    public override void Close()
    {
        toggle.SetActive(false);
    }
}
