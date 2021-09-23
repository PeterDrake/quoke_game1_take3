using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSelection : MonoBehaviour
{

    private DialogueDisplayer displayer;
    public GameObject toggler;
    public Color original;
    public Color flash;
    private Color goal;
    private Color current;

    // Start is called before the first frame update
    void Start()
    {
        displayer = GetComponent<DialogueDisplayer>();
        current = original;
    }

    // Update is called once per frame
    void Update()
    {
        if (toggler.activeInHierarchy)
        {
            Selected();
        }
        else
        {
            StopCoroutine("ButtonSelect");
        }
    }

    public void Selected()
    {
        StartCoroutine("ButtonSelect");
    }

    public IEnumerator ButtonSelect()
    {
        while (toggler.activeSelf)
        {
            if (current == flash) { goal = original; }
            else if(current == original) { goal = flash; }

            current = Color.Lerp(current, goal, .4f);
            //print("Selected is " + displayer.selectedOption.name);
            //displayer.nextOption.GetComponent<Image>().color = original;
            //displayer.lastOption.GetComponent<Image>().color = original;
            //displayer.selectedOption.GetComponent<Image>().color = current;

            yield return new WaitForSeconds(1f);
        }
    }
}
