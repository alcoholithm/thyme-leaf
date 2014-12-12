using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// The way communicate to other entity.
/// </summary>
public class Message : ICommand
{ // struct로 바꾸는 거 생각해보기
    public const string TAG = "[Message]";

    public MessageTypes what = MessageTypes.MSG_TOWER_READY;
    public int arg1;
    public int arg2;
    public UnityEngine.Object obj;
    public ICommand command;

    public IHandler sender;
    public IHandler receiver;

    public Message()
    {
        command = new NullCommand();
    }

    public static Message Obtain(IHandler h, MessageTypes what, int arg1, int arg2, ICommand command, UnityEngine.Object obj)
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
    public static Message Obtain(IHandler h, MessageTypes what, int arg1, int arg2, UnityEngine.Object obj)
    {
        Message msg = Obtain();
        msg.receiver = h;
        msg.what = what;
        msg.arg1 = arg1;
        msg.arg2 = arg2;
        msg.obj = obj;
        return msg;
    }
    public static Message Obtain(IHandler h, MessageTypes what, ICommand command, UnityEngine.Object obj)
    {
        Message msg = Obtain();
        msg.receiver = h;
        msg.what = what;
        msg.command = command;
        msg.obj = obj;
        return msg;
    }
    public static Message Obtain(IHandler h, MessageTypes what, UnityEngine.Object obj)
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
    public static Message Obtain(IHandler h, MessageTypes what, int arg1)
    {
        Message msg = Obtain();
        msg.receiver = h;
        msg.what = what;
        msg.arg1 = arg1;
        return msg;
    }
    public static Message Obtain(IHandler h, MessageTypes what)
    {
        Message msg = Obtain();
        msg.receiver = h;
        msg.what = what;
        return msg;
    }
    public static Message Obtain<TArg>(IHandler h, MessageTypes what, Action<TArg> action) where TArg : class
    {
        Message msg = Obtain();
        msg.receiver = h;
        msg.what = what;
        msg.command = new ActionCommand<TArg>(h as TArg, action);
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

    /*
     * 
     */ 
    public void Execute()
    {
        receiver.HandleMessage(this);
    }

    /*
     * 
     */ 
    private class ActionCommand<TArg> : ICommand
    {
        private TArg receiver;
        private Action<TArg> action;

        public ActionCommand(TArg receiver, Action<TArg> action)
        {
            this.receiver = receiver;
            this.action = action;
        }
        public void Execute()
        {
            action(receiver);
        }
    }
}
