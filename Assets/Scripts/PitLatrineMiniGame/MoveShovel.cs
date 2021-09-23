using System.Collections;
using UnityEngine;

public class MoveShovel : MonoBehaviour
{
    private float movementSpeed = 2f;

    //public GameObject Shovel1;
    public GameObject Pit1;
    public GameObject Pit2;
    public GameObject Dirt1;
    public GameObject Dirt2;
    public GameObject Dirt3;    //Dirt on the shovel
    public GameObject Water;

    private int digtimes = 0;

    public void Dig()
    {
        digtimes++;
        Debug.Log("Dug - hole depth " + digtimes);
        StartCoroutine(nameof(DigVertically));
    }
    
    private IEnumerator DigVertically()
    {
        //Moves shovel down
        while (transform.position.y > -0.8f)
        {
            Vector3 Target = new Vector3(transform.position.x, -10, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, Target, movementSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.0005f);
        }
        
        yield return new WaitForSeconds(0.5f);
        MakePitAppear();
        Dirt3.SetActive(true); //Dirt on the shovel

        //Moves shovel to the side
        while (transform.position.x > 71.81f && transform.position.y<1)
        {
            Vector3 Target = new Vector3(71, 0.5f, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, Target, movementSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.0005f);
        }

        Dirt3.SetActive(false);
        MakeDirtAppear();
        
        if (Dirt2.activeSelf)
        {
            Water.SetActive(true);
        }
        //Moves shovel to the start position
        transform.position = new Vector3(73.04f, 0.14f * movementSpeed * Time.deltaTime, transform.position.z);
        
    }

    private void MakePitAppear()
    {
        if (Pit1.activeSelf)
        {
            Pit2.SetActive(true);
        }
        else
        {
            Pit1.SetActive(true);
        }
    }

    private void MakeDirtAppear()
    {
        if (Dirt1.activeSelf == true)
        {
            Dirt2.SetActive(true);
        }
        else
        {
            Dirt1.SetActive(true);
        }
    }

    
}
