using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToggle : MonoBehaviour
{
    Vector3 startPos;
    Vector3 dist;

    void Start()
    {
        //only put start here so that the script can be enabled and disabled
    }

    void OnMouseDown()
    {
        startPos = Camera.main.WorldToScreenPoint(transform.position);
        dist = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0.1f, startPos.z));
    }

    void OnMouseDrag()
    {
        //float theX;
        //if (Input.mousePosition.x > 50) { theX = 50; }
        //else if (Input.mousePosition.x < -50) { theX = -50; }
        //else { theX = Input.mousePosition.x; }

        Vector3 lastPos = new Vector3(Input.mousePosition.x, 0, startPos.z);
        //Vector3 lastPos = new Vector3(theX, 0, startPos.z);
        transform.position = Camera.main.ScreenToWorldPoint(lastPos) + dist;
    }
}
