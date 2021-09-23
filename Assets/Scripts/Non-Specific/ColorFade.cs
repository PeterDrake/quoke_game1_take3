using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFade : MonoBehaviour
{

    public Color firstColor, secondColor;
    public bool fade;
    private Color currentGoalColor;
    private MeshRenderer myRenderer;
    public float speed;
    //private bool working;
    
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        myRenderer.material.color = firstColor;
        currentGoalColor = firstColor;
        fade = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(fade)
        {
            if (myRenderer.material.color == firstColor)
            {
                currentGoalColor = secondColor;
            }
            else if (myRenderer.material.color == secondColor)
            {
                currentGoalColor = firstColor;
            }
            /*else if (currentColor == secondColor)
            {
                currentColor = firstColor;
            }*/
        }
         
        myRenderer.material.color = Color.Lerp (myRenderer.material.color, currentGoalColor, speed);

    }

    public void KillIt()
    {
        Destroy(this);
    } 
}
