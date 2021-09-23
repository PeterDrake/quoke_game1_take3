using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReadyToBark : MonoBehaviour
{
    private const string EventKey = "LEVELFINISHED";
    private bool _satisfied;

    public UnityEvent OnEnter;

    // Start is called before the first frame update
    void Start()
    {
        Systems.Objectives.Register(EventKey, () => _satisfied = true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_satisfied)
        {
            OnEnter.Invoke();
        }
    }
}
