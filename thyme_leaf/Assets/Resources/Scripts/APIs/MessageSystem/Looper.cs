using UnityEngine;
using System.Collections;
using System.Threading;
using System;

/// <summary>
/// Class "Looper" imitates like "android looper" almost.
/// </summary>
public class Looper : Singleton<Looper>
{
    private MessageQueue messageQueue;

    /*
     * followings are unity callback methods
     */
    protected override void Awake()
    {
        messageQueue = MessageQueue.Instance;
    }

    void Update()
    {
        GetMessageAndExecute();
    }

    /*
     * followings are member functions
     */
    void GetMessageAndExecute()
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
