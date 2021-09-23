using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragBarrel : MonoBehaviour
{
     Vector3 startPos;
     Vector3 dist;

     public GameObject Spout;
     public GameObject ModifySpout;
     
     void OnMouseDown()
    {
        if (Spout == null)
        {
            startPos = Camera.main.WorldToScreenPoint(transform.position);
            dist = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0.1f, startPos.z));
        }

        else
        {
            StartCoroutine(nameof(ModifySpoutFirst));
        }
        
    }

    void OnMouseDrag()
    {
        if (Spout == null)
        {
            Vector3 lastPos = new Vector3(Input.mousePosition.x, 0, startPos.z);
            transform.position = Camera.main.ScreenToWorldPoint(lastPos) + dist;
        }
    }

    public IEnumerator ModifySpoutFirst()
    {
        ModifySpout.SetActive(true);
        yield return new WaitForSeconds(3f);
        ModifySpout.SetActive(false);
    }
    
}