using UnityEngine;
using System.Collections;

/// <summary>
/// This imitates like android looper almost.
/// </summary>
public class Looper : ISystem
{
    public new const string TAG = "[Looper]";

    private static Looper instance = new Looper();
    public static Looper Instance
    {
        get { return instance; }
    }

    private MessageQueue messageQueue;
    private bool active = false;

    /// <summary>
    /// followings are member functions
    /// </summary>
    public void Loop()
    {
        //while (active)
        //{
        //    Message msg = messageQueue.Pop();
        //    if (msg != null)
        //        msg.Send();
        //}

    }

    /// <summary>
    /// Followings are implemeted methods
    /// </summary>
    public void Prepare()
    {
        messageQueue = MessageQueue.Instance;
        active = true;
        Loop();
    }
    public void Quit()
    {
        active = false;
    }
}
