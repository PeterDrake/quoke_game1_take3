using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// Ensures that only one window will be active on the UI at any given moment
/// </summary>
public class UIManager : MonoBehaviour
{
    // public static reference so other scripts can easily reference this
    public static UIManager Instance;
    
    private bool initialized;

    private UIElement activeWindow; // Currently displayed window
    private UIElement previousWindow; // window displayed before active

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    /// <summary>
    /// opens newActive and closes the current active window
    /// </summary>
    /// <param name="newActive"></param>
    public void SetAsActive(UIElement newActive)
    {
        if (!initialized)
        {
            Debug.Log("not init");
            Initialize(newActive);
            return;
        }

        if (newActive == activeWindow)
        {
            activeWindow.Open();
            return;
        }
        
        if (!newActive.Force() && activeWindow.IsLocked()) return;
        
        previousWindow = activeWindow;
        previousWindow.Close();
        activeWindow = newActive;
        activeWindow.Open();
        print("50previous = " + previousWindow);
        print("51current = " + activeWindow);

        Systems.Instance.Pause(activeWindow.PauseOnOpen());
    }
    
    public void Initialize(UIElement active)
    {
        activeWindow = active;
        active.Open();
        print("60current = " + activeWindow);
        initialized = true;
        Systems.Instance.Pause(activeWindow.PauseOnOpen());
    }

    /// <summary>
    /// if newActive is currently active it will be closed, otherwise it will be opened
    /// </summary>
    /// <param name="newActive"></param>
    public void ToggleActive(UIElement newActive)
    {
        if (newActive == activeWindow)
        {
            ActivatePrevious();
        }
        else
        {
            SetAsActive(newActive);
        }
    }
    
    /// <summary>
    /// Close the current window and go back to the last one
    /// </summary>
    public void ActivatePrevious()
    {
        print("86previous = " + previousWindow);
        //the first time this is used, SetAsActive has been prematurely returned
        //so previousWindow has not been set
        activeWindow.Close();
        previousWindow.Open();
        
        UIElement temp = activeWindow;
        activeWindow = previousWindow;
        previousWindow = temp;
        
        Systems.Instance.Pause(activeWindow.PauseOnOpen());
    }
}
