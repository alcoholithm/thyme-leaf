using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This imitates android MessgeQueue almost.
/// </summary>
public class MessageQueue : ISystem
{
    private Queue<Message> messages;

    private MessageQueue()
    {
        Prepare();
    }

    /*
     * followings are member functions
     */
    public void Push(Message command)
    {
        try
        {
            messages.Enqueue(command);
        }
        catch (System.Exception e)
        {
            throw e;
        }
    }

    public Message Pop()
    {
        try
        {
            return messages.Dequeue();
        }
        catch (System.Exception e)
        {
            throw e;
        }
    }

    public Message Peek()
    {
        return messages.Peek();
    }

    /*
     * Followings are implemeted methods of interface
     */
    public void Prepare()
    {
        this.messages = new Queue<Message>();
    }

    public void Quit()
    {
    }

    public const string TAG = "[MessageQueue]";
    private static MessageQueue instance = new MessageQueue();
    public static MessageQueue Instance
    {
        get { return MessageQueue.instance; }
        set { MessageQueue.instance = value; }
    }
}