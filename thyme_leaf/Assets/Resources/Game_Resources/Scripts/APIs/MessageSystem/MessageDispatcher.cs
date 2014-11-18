using UnityEngine;
using System.Collections;

/// <summary>
/// Message dispatching system
/// </summary>

public class MessageDispatcher : Singleton<MessageDispatcher> {
    public const string TAG = "[MessageDispatcher]";

    public Message ObtainMessage() { return null; }
    public void Dispatch(Message msg) { }
    public void DispatchDelayed(Message msg) { }
}
