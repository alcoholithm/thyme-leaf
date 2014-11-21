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
        Initialize(MessageQueue.Instance);
        Initialize(Looper.Instance);

        this.messageDispatcher = new MessageDispatcher();
    }

    new void OnDestroy()
    {
        base.OnDestroy();
        Finalize(Looper.Instance);
        Finalize(MessageQueue.Instance);
    }

    /// <summary>
    /// followings are member functions
    /// </summary>
    /// <param name="system"></param>
    private void Initialize(ISystem system)
    {
        system.Prepare();
    }
    private void Finalize(ISystem system)
    {
        system.Quit();
    }

    public Message ObtainMessage()
    {
        return messageDispatcher.ObtainMessage();
    }

    public void Dispatch(Message msg)
    {
        messageDispatcher.Dispatch(msg);
    }

    public void DispatchDelayed(Message msg, float seconds)
    {
        messageDispatcher.DispatchDelayed(msg, seconds);
    }

    /// <summary>
    /// inner class
    /// Message dispatching system
    /// </summary>
    private class MessageDispatcher
    {
        public const string TAG = "[MessageDispatcher]";

        public Message ObtainMessage()
        {
            return Message.Obtain();
        }

        public void Dispatch(Message msg)
        {
            MessageQueue.Instance.Push(msg);
        }

        public void DispatchDelayed(Message msg, float seconds)
        {
            MessageSystem.Instance.StartCoroutine(WaitForSeconds(msg, seconds));
        }

        public IEnumerator WaitForSeconds(Message msg, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Dispatch(msg);
        }
    }
}  