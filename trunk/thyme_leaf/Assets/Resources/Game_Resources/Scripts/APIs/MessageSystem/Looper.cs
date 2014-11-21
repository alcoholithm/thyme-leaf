using UnityEngine;
using System.Collections;
using System.Threading;
using System;

/// <summary>
/// This imitates like android looper almost.
/// </summary>
public class Looper : ISystem
{
    private volatile bool active = true;

    /// <summary>
    /// followings are member functions
    /// </summary>
    void Loop()
    {
        new Thread(() => DoInBackground()).Start();
    }

    void DoInBackground()
    {
        try
        {
            MessageQueue messageQueue = MessageQueue.Instance;
            while (active)
            {
                Message msg = messageQueue.Pop();
                if (msg != null)
                    msg.Send();
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    void Stop()
    {
        active = false;
    }

    /// <summary>
    /// Followings are implemeted methods
    /// </summary>
    public void Prepare()
    {
        Loop();
        Debug.Log(TAG + "has Started");
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Stop();
    }

    /// <summary>
    /// followings are data members
    /// </summary>
    public const string TAG = "[Looper]";

    private static Looper instance = new Looper();
    public static Looper Instance
    {
        get { return instance; }
    }
}
