using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// All the game entities must be derived from this class.
/// </summary>
public abstract class GameEntity<TGameEntity> : MonoBehaviour, IHandler // client
{
    protected StateMachine<TGameEntity> stateMachine;


    /*
    * followings are member functions
    */
    public Message ObtainMessage(MessageTypes what, int arg1, int arg2, ICommand command, UnityEngine.Object obj)
    {
        return Message.Obtain(this, what, arg1, arg2, command, obj);
    }
    public Message ObtainMessage(MessageTypes what, int arg1, int arg2, ICommand command)
    {
        return Message.Obtain(this, what, arg1, arg2, command);
    }
    public Message ObtainMessage(MessageTypes what, int arg1, int arg2, UnityEngine.Object obj)
    {
        return Message.Obtain(this, what, arg1, arg2, obj);
    }
    public Message ObtainMessage(MessageTypes what, ICommand command, UnityEngine.Object obj)
    {
        return Message.Obtain(this, what, command, obj);
    }
    public Message ObtainMessage(MessageTypes what, UnityEngine.Object obj)
    {
        return Message.Obtain(this, what, obj);
    }
    public Message ObtainMessage(MessageTypes what, ICommand command)
    {
        return Message.Obtain(this, what, command);
    }
    public Message ObtainMessage(MessageTypes what, int arg1, int arg2)
    {
        return Message.Obtain(this, what, arg1, arg2);
    }
    public Message ObtainMessage(MessageTypes what)
    {
        return Message.Obtain(this, what);
    }
    public Message ObtainMessage<TArg>(MessageTypes what, Action<TArg> action) where TArg : class
    {
        return Message.Obtain<TArg>(this, what, action);
    }
    public Message ObtainMessage()
    {
        return Message.Obtain(this);
    }
    
    public bool DispatchMessage(Message msg)
    {
        return MessageSystem.Instance.Dispatch(msg);
    }
    public bool DispatchMessageDelayed(Message msg, float seconds)
    {
        return MessageSystem.Instance.DispatchDelayed(msg, seconds);
    }

    public void ChangeState(State<TGameEntity> newState)
    {
        stateMachine.ChangeState(newState);
    }

    /*
    * followings are implemented methods of interface
    */
    public bool IsHandleable(Message msg)
    {
        throw new System.NotImplementedException();
    }

    public void OnMessage(Message msg)
    {
        throw new System.NotImplementedException();
    }

    public void HandleMessage(Message msg)
    {
        //Debug.Log(TAG + "HandleMessage");
        Successor.HandleMessage(msg);
    }

    public const string TAG = "[GameEntity]";
    /*
    * followings are attributes
    */
    public StateMachine<TGameEntity> StateMachine
    {
        get { return stateMachine; }
    }
    public IHandler Successor
    {
        get { return stateMachine; }
    }
}
