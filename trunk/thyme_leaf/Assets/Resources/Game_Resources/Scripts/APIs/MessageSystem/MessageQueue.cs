using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// This imitates android MessgeQueue almost.
/// </summary>
public class MessageQueue : Singleton<MessageQueue>
{
    public new const string TAG = "[MessageQueue]";

    private List<Message> messages = new List<Message>();

    public void Push(Message msg)
    {
        messages.Add(msg);
    }

    public Message pop()
    {
        Message msg = messages[messages.Count - 1];
        messages.RemoveAt(messages.Count - 1);
        return msg;
    }

    public Message top()
    {
        return messages[messages.Count - 1];
    }
}
