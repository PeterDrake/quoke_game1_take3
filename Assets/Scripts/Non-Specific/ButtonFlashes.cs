using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFlashes : MonoBehaviour
{
    //public Button.ButtonClickedEvent onClick;
    //public Button theButton;

    public bool startOnAwake;
    public bool oneUseOnly;

    public Image theImage;

    public Color oneColor, otherColor;
    public float fadeSpeed;
    private Color currentColor, goalColor, initialColor;
   
    private void Start()
    {
        initialColor = theImage.color;
        currentColor = initialColor;
        goalColor = oneColor;
    }

    private void Update()
    {
        if (startOnAwake) { StartCoroutine(ButtonThrob()); }
    }
    /*
    public IEnumerator BlinkText()
    {
        while (true)
        {
            theButton.GetComponent<Image>().color = Color.red;
            yield return new WaitForSeconds(0.3f);
            theButton.GetComponent<Image>().color = Color.white;
            //theButton.colors = Color.white;
            yield return new WaitForSeconds(0.15f);
        }
    }
    */
    public IEnumerator ButtonThrob()
    {
        while (true)
        {
            if (currentColor == oneColor) { goalColor = otherColor; }
            else if (currentColor == otherColor) { goalColor = oneColor; }

            currentColor = Color.Lerp(currentColor, goalColor, fadeSpeed);
            theImage.color = currentColor;
            yield return new WaitForSeconds(1f);
        }
    }

    public void StopThrob()
    {
        StopCoroutine(ButtonThrob());
        theImage.color = initialColor;
        if (oneUseOnly) { Destroy(this); }
    }

    public void StartThrob()
    {
        StartCoroutine(ButtonThrob());
    }
}
