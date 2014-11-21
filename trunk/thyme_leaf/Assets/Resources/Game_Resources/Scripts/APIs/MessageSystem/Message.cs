using UnityEngine;
using System.Collections;

/// <summary>
/// The way communicate to other entity.
/// </summary>
public class Message
{ // struct로 바꾸는 거 생각해보기
    public const string TAG = "[Message]";

    public int what = -1;
    public int arg1;
    public int arg2;
    public Object obj;
    public ICommand command;

    public IHandler sender;
    public IHandler receiver;

    public Message()
    {
        command = new NoCommand();
    }

    public static Message Obtain()
    {
        return MessagePool.Instance.Allocate();
    }

    public void Recycle()
    {
        // Message pool에 반납
    }

    public void Send()
    {
        Debug.Log("Send");
        receiver.HandleMessage(this);
    }
}
