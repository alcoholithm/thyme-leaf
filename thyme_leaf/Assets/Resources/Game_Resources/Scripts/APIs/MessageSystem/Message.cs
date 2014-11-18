using UnityEngine;
using System.Collections;

/// <summary>
/// The way communicate to other entity.
/// </summary>
public class Message {
    public const string TAG = "[Message]";

    public int what;
    public int arg1;
    public int arg2;
    public Object obj;
    public ICommand command;

    private IHandler sender;
    private IHandler receiver;

    public Message()
    {

    }

    public static Message Obtain() {
        // Message pool 에서 땡겨온다
        return null;
    }

    public void Recycle()
    {
        // Message pool에 반납
    }

    public void send()
    {
        receiver.HandleMessage(this);
    }
}
