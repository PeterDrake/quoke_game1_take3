using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowMouse : MonoBehaviour
{
	private float obj_z;
    
	void Start() 
    {
        // gets constant z from camera position
		obj_z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
	}

	void Update()
	{
		transform.position = GetMouseAsWorldPoint();
	}
    
	private Vector3 GetMouseAsWorldPoint()
	{
		// Pixel coordinates of mouse (x,y)
		Vector3 mousePoint = Input.mousePosition;

		// z coordinate of game object on screen
		mousePoint.z = obj_z;

		// Convert it to world points
		return Camera.main.ScreenToWorldPoint(mousePoint);
	}
}
