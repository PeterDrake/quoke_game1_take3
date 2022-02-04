using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // for Convert

public class RotateObjectWithMouse : MonoBehaviour
{
	public delegate void CallBack();
	// Start is called before the first frame update
	private float[] xConstraint;
	private float xTarget;
	private float xTargetThreshhold;
	private CallBack successCallback;
	private bool initialized = false;

    // Update is called once per frame
    void Update()
    {
		if (!initialized)
		{
			Debug.LogError("RotateObjectWithMouse Script on " +gameObject.name + " has not been initialized!");
			return;
		}
		var dif = Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;
		var angle = Mathf.Rad2Deg*Mathf.Atan2(dif.x, dif.y);
		if (angle > xConstraint[0]) //check lowerBound
			angle = xConstraint[0];
		else if (angle < xConstraint[1]) //check upperBound
			angle = xConstraint[1];
		transform.localRotation = Quaternion.Euler(angle, transform.localRotation.y, transform.localRotation.z);
		if (Math.Abs(angle - xTarget) < xTargetThreshhold)
		{
			successCallback();
		}
    }

	public void Initialize(float lowerBound, float upperBound, float xTarget, float xTargetThreshhold, CallBack callback)
	{
		this.xTarget = xTarget;
		this.xTargetThreshhold = xTargetThreshhold;
		successCallback = callback;
		initialized = true;

		xConstraint = new float[2];
		xConstraint[0] = lowerBound;
		xConstraint[1] = upperBound;
	}
}
