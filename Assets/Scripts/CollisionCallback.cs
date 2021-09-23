using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCallback : MonoBehaviour
{
	public delegate void CallBack();

	private Dictionary<string, CallBack> callback_functions;

	public void Awake()
	{
		callback_functions = new Dictionary<string, CallBack>();
	}

	void OnTriggerEnter(Collider obj)
	{
		if (callback_functions.ContainsKey(obj.tag))
		{
			callback_functions[obj.tag]();
		}
	}

	public void AddCallback(string tag, CallBack cb)
	{
		if (!callback_functions.ContainsKey(tag))
		{
			callback_functions.Add(tag, cb);
		}
	}

	public void RemoveCallback(string tag)
	{

		callback_functions.Remove(tag);
	}

}
