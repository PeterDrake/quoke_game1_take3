using UnityEngine;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles writing of log strings to log files
/// </summary>
public class Logger : Singleton<Logger>
{
    // Prevent non-singleton constructor use
    protected Logger()
    {
    }

    private StreamWriter writer;

    private bool DoLog = false; // change to true if you want logging

    public void Log(string message)
    {
        if (!DoLog)
        {
            return;
        }

        int time = (int) Math.Round(Time.realtimeSinceStartup);
        try
        {
            writer.WriteLine(time + ": " + message);
        }
        catch (Exception e)
        {
            Debug.LogError(e.StackTrace);
            DoLog = false;
        }

        // Debug.Log(time + ": " + message);
    }

    protected void Awake()
    {
        if (!DoLog)
        {
            return;
        }
        try
        {
            writer = new StreamWriter(GenerateFileName());
            Log("Game started.");
            SceneManager.sceneLoaded += Flush;
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.LogError("Unable to find log file, logging has been disabled!");
            Debug.LogError(e.StackTrace);
            DoLog = false;
        }
    }

    private void Flush(Scene s, LoadSceneMode lsm)
    {
        if (!DoLog) return;

        writer.Flush();
    }

    protected void OnApplicationQuit()
    {
        if (!DoLog)
        {
            return;
        }
        Log("Game ended.");
        writer.Close();
    }


    // Generates a name for the log file
    private string GenerateFileName()
    {
        return "QuokeLogFiles/" + GetMacAddress() + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".log";
    }

    // Adapted from http://www.independent-software.com/getting-local-machine-mac-address-in-csharp.html/ 
    private string GetMacAddress()
    {
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet &&
                nic.NetworkInterfaceType != NetworkInterfaceType.Wireless80211)
            {
                continue;
            }

            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                return nic.GetPhysicalAddress().ToString();
            }
        }

        throw new Exception("No MAC address found");
    }
    
}