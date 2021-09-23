using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobRotating : MonoBehaviour
{

    private float move1 = 0, move2 = 0, move3 = 0, move4 = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "ElectricBox")
        {
            if (move1 < 25)
            {

                transform.Rotate(0, 6f, 0);
                move1 += 1f;
            }
            else
            {
                Destroy(this);
            }
        }
        if (gameObject.tag == "WaterPipe")
        {

            if (move2 < 360)
            {
                transform.Rotate(0, 0, -5);
                move2 += 5;
            }
            else
            {
                Destroy(this);
            }
        }
        if (gameObject.tag == "AirPipe")
        {
            if (move3 < 90)
            {
                transform.Rotate(0, 0, -5);
                move3 += 5;
            }
            else
            {
                Destroy(this);
            }
        }
        if (gameObject.tag == "WaterSpout")
        {
            if (move4 < 500)
            {
                transform.Rotate(0, 0, 5);
                move4 += 5;
            }
            else
            {
                Destroy(this);
            }
        }


    }
}
