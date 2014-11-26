using UnityEngine;
using System.Collections;

/// <summary>
/// Facade of Message system
/// </summary>
public class MessageSystem : Singleton<MessageSystem>
{
    public new const string TAG = "[MessageSystem]";
    private MessageDispatcher messageDispatcher;

    /// <summary>
    /// followings are unity callback methods
    /// </summary>
    void Awake()
    {
        this.messageDispatcher = new MessageDispatcher();

        MessageQueue.Instance.Prepare();

        Looper.Instance.transform.parent = transform;
    }

    new void OnDestroy()
    {
        base.OnDestroy();
        MessageQueue.Instance.Quit();
    }

    /// <summary>
    /// followings are member functions
    /// </summary>
    //public Message ObtainMessage(IHandler h, MessageTypes what, int arg1, int arg2, ICommand command, Object obj)
    //{
    //    return Message.Obtain(h, what, arg1, arg2, command, obj);
    //}

    //public Message ObtainMessage(IHandler h, MessageTypes what, int arg1, int arg2, ICommand command)
    //{
    //    return Message.Obtain(h, what, arg1, arg2, command);
    //}

    //public Message ObtainMessage(IHandler h, MessageTypes what, int arg1, int arg2, Object obj)
    //{
    //    return Message.Obtain(h, what, arg1, arg2, obj);
    //}

    //public Message ObtainMessage(IHandler h, MessageTypes what, ICommand command, Object obj)
    //{
    //    return Message.Obtain(h, what, command, obj);
    //}

    //public Message ObtainMessage(IHandler h, MessageTypes what, Object obj)
    //{
    //    return Message.Obtain(h, what, obj);
    //}

    //public Message ObtainMessage(IHandler h, MessageTypes what, ICommand command)
    //{
    //    return Message.Obtain(h, what, command);
    //}

    //public Message ObtainMessage(IHandler h, MessageTypes what, int arg1, int arg2)
    //{
    //    return Message.Obtain(h, what, arg1, arg2);
    //}

    //public Message ObtainMessage(IHandler h, MessageTypes what)
    //{
    //    return Message.Obtain(h, what);
    //}

    //public Message ObtainMessage(IHandler h)
    //{
    //    return Message.Obtain(h);
    //}

    //public Message ObtainMessage()
    //{
    //    return Message.Obtain();
    //}

    public bool Dispatch(Message msg)
    {
        return messageDispatcher.Dispatch(msg);
    }

    public bool DispatchDelayed(Message msg, float seconds)
    {
        return messageDispatcher.DispatchDelayed(msg, seconds);
    }

    /// <summary>
    /// inner class
    /// Message dispatching system
    /// </summary>
    private class MessageDispatcher
    {
        public const string TAG = "[MessageDispatcher]";

        public bool Dispatch(Message msg)
        {
            try
            {
                MessageQueue.Instance.Push(msg);
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }

        public bool DispatchDelayed(Message msg, float seconds)
        {
            try
            {
                MessageSystem.Instance.StartCoroutine(WaitForSeconds(msg, seconds));
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }

        public IEnumerator WaitForSeconds(Message msg, float seconds)
        {
            yield return new WaitForSeconds(seconds);

            if (!Dispatch(msg))
                throw new System.Exception();
        }
    }
}