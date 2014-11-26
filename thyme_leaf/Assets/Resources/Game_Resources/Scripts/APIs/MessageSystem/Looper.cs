using UnityEngine;
using System.Collections;
using System.Threading;
using System;

/// <summary>
/// This imitates like android looper almost.
/// </summary>
public class Looper : Singleton<Looper>
{
    private volatile bool active = true;
    private MessageQueue messageQueue;

    void Awake()
    {
        messageQueue = MessageQueue.Instance;
        Debug.Log(TAG + "has started");
    }

    void Update()
    {
        Loop();
    }

    void OnDestroy()
    {
        Debug.Log(TAG + "has finished");
    }

    void Loop()
    {
        try
        {
            messageQueue.Pop().Execute();
        }
        catch { }
    }

    public new const string TAG = "[Looper]";
}

//void Loop()
//{
//    //new Thread(() => DoInBackground()).Start();
//}

//void DoInBackground()
//{
//    try
//    {
//        MessageQueue messageQueue = MessageQueue.Instance;
//        while (active)
//        {
//            ICommand command = messageQueue.Pop();
//            if (command != null)
//                command.Execute();
//        }
//    }
//    catch (Exception e)
//    {
//        Debug.LogException(e);
//    }
//}

//void Stop()
//{
//    active = false;
//}
