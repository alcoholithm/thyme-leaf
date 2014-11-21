using UnityEngine;
using System.Collections;

/// <summary>
/// All the state classes must be derived from this class
/// </summary>
/// <typeparam name="T"></typeparam>
/// owner

public abstract class State<T> : IHandler
{
    public const string TAG = "[State]";

    //protected T owner; // static 불가능 => 공유할수없다, 메모리를 잡고 있게 됨.
    //private State<T> successor;

    /// <summary>
    /// This will be called when starting the state.
    /// </summary>
    public abstract void Enter(T owner);

    /// <summary>
    /// This will be called once per frame
    /// </summary>
    public abstract void Execute(T owner);

    /// <summary>
    /// This will be called when leaving the state
    /// </summary>
    public abstract void Exit(T owner);

    /// <summary>
    /// 
    /// </summary>
    private IHandler successor;
    public IHandler Successor
    {
        set { successor = value; }
        get { return successor; }
    }

    public abstract bool IsHandleable(Message msg);

    public void OnMessage(Message msg)
    {
        msg.command.Execute();
    }

    public void HandleMessage(Message msg)
    {
        if (IsHandleable(msg))
        {
            OnMessage(msg);
        }
        else
        {
            if (Successor != null)
                Successor.HandleMessage(msg);
        }
    }
}