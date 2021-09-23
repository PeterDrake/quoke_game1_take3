using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// Allows runtime registration of keys 
/// </summary>
public class InputManager : MonoBehaviour
{
   public delegate void CallBack();
   

   private Dictionary<string, CallBack> registeredKeys;
   [SerializeField] private ArrayList keys;

   private void Awake()
   {
      keys = new ArrayList();
      registeredKeys = new Dictionary<string, CallBack>();
   }
   public void RegisterKey(string key, CallBack cb)
   {
      keys.Add(key);
      registeredKeys.Add(key, cb);
   }

   public void RemoveKey(string key)
   {
      keys.Remove(key);
      registeredKeys.Remove(key);
   }

    public void Update()
    {
        foreach (string key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                registeredKeys[key].Invoke();
            }
        }

    }
}
