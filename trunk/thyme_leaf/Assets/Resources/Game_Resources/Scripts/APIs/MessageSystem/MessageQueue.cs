using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This imitates android MessgeQueue almost.
/// </summary>
class MessageQueue : ISystem
{
    public new const string TAG = "[MessageQueue]";

    private static MessageQueue instance = new MessageQueue();

    internal static MessageQueue Instance
    {
        get { return MessageQueue.instance; }
        set { MessageQueue.instance = value; }
    }

    private Queue<Message> messages;

    /// <summary>
    /// followings are member functions
    /// </summary>
    public void Push(Message msg)
    {
        messages.Enqueue(msg);
    }

    public Message Pop()
    {
        try
        {
            return messages.Dequeue();
        }
        catch
        {
            return null;
        }
    }

    public Message PeekTop()
    {
        return messages.Peek();
    }


    /// <summary>
    /// Followings are implemeted methods
    /// </summary>
    public void Prepare()
    {
        messages = new Queue<Message>();
    }

    public void Quit()
    {
    }
}