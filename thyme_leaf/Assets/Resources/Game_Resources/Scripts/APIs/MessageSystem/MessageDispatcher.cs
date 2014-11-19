using UnityEngine;
using System.Collections;

/// <summary>
/// Message dispatching system
/// </summary>

public class MessageDispatcher : Singleton<MessageDispatcher> {
    public new const string TAG = "[MessageDispatcher]";

    public Message ObtainMessage()
    {
        return Message.Obtain(); 
    }

    public void Dispatch(Message msg) 
    {
        MessageQueue.Instance.Push(msg);
    }

    public void DispatchDelayed(Message msg) 
    {
    }
}