using UnityEngine;
using System.Collections;

/// <summary>
/// The way communicate to other entity.
/// </summary>
public class Message : ICommand
{ // struct로 바꾸는 거 생각해보기
    public const string TAG = "[Message]";

    public MessageTypes what = MessageTypes.MSG_BUILD_TOWER;
    public int arg1;
    public int arg2;
    public Object obj;
    public ICommand command;

    public IHandler sender;
    public IHandler receiver;

    public Message()
    {
        command = new NullCommand();
    }

    public static Message Obtain(IHandler h, MessageTypes what, int arg1, int arg2, ICommand command, Object obj)
    {
        Message msg = Obtain();
        msg.receiver = h;
        msg.what = what;
        msg.arg1 = arg1;
        msg.arg2 = arg2;
        msg.command = command;
        msg.obj = obj;
        return msg;
    }

    public static Message Obtain(IHandler h, MessageTypes what, int arg1, int arg2, ICommand command)
    {
        Message msg = Obtain();
        msg.receiver = h;
        msg.what = what;
        msg.arg1 = arg1;
        msg.arg2 = arg2;
        msg.command = command;
        return msg;
    }

    public static Message Obtain(IHandler h, MessageTypes what, int arg1, int arg2, Object obj)
    {
        Message msg = Obtain();
        msg.receiver = h;
        msg.what = what;
        msg.arg1 = arg1;
        msg.arg2 = arg2;
        msg.obj = obj;
        return msg;
    }

    public static Message Obtain(IHandler h, MessageTypes what, ICommand command, Object obj)
    {
        Message msg = Obtain();
        msg.receiver = h;
        msg.what = what;
        msg.command = command;
        msg.obj = obj;
        return msg;
    }

    public static Message Obtain(IHandler h, MessageTypes what, Object obj)
    {
        Message msg = Obtain();
        msg.receiver = h;
        msg.what = what;
        msg.obj = obj;
        return msg;
    }

    public static Message Obtain(IHandler h, MessageTypes what, ICommand command)
    {
        Message msg = Obtain();
        msg.receiver = h;
        msg.what = what;
        msg.command = command;
        return msg;
    }

    public static Message Obtain(IHandler h, MessageTypes what, int arg1, int arg2)
    {
        Message msg = Obtain();
        msg.receiver = h;
        msg.what = what;
        msg.arg1 = arg1;
        msg.arg2 = arg2;
        return msg;
    }

    public static Message Obtain(IHandler h, MessageTypes what)
    {
        Message msg = Obtain();
        msg.receiver = h;
        msg.what = what;
        return msg;
    }

    public static Message Obtain(IHandler h)
    {
        Message msg = Obtain();
        msg.receiver = h;
        return msg;
    }

    public static Message Obtain()
    {
        return MessagePool.Instance.Allocate();
    }

    public void Recycle()
    {
        // Message pool에 반납
    }

    /// <summary>
    /// 
    /// </summary>
    public void Execute()
    {
        receiver.HandleMessage(this);
    }
}
