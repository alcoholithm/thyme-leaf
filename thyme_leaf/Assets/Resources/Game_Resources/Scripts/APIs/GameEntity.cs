using UnityEngine;
using System.Collections;

/// <summary>
/// All the game entities must be derived from this class.
/// </summary>
public abstract class GameEntity<T> : MonoBehaviour, IHandler // client
{
    public const string TAG = "[GameEntity]";

    protected StateMachine<T> stateMachine;
    public StateMachine<T> StateMachine
    {
        get { return stateMachine; }
    }

    public IHandler Successor
    {
        set { }
        get { return stateMachine; }
    }

    public bool IsHandleable()
    {
        throw new System.NotImplementedException();
    }

    public void OnMessage(Message msg)
    {
        throw new System.NotImplementedException();
    }

    public void HandleMessage(Message msg)
    {
        Successor.HandleMessage(msg);
    }
}
