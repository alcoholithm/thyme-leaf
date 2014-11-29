using UnityEngine;
using System.Collections;
using System.Threading;
using System;

/// <summary>
/// This imitates like android looper almost.
/// </summary>
public class Looper : Singleton<Looper>
{
    private MessageQueue messageQueue;

    /*
     * followings are unity callback methods
     */ 
    void Awake()
    {
        messageQueue = MessageQueue.Instance;
        Debug.Log(TAG + "has started");
    }

    void Update()
    {
        Loop();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Debug.Log(TAG + "has finished");
    }

    /*
     * followings are member functions
     */ 
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

//private volatile bool active = true;
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
