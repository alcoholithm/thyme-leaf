using UnityEngine;
using System.Collections;

/// <summary>
/// for FSM
/// </summary>
/// <typeparam name="T"></typeparam>

public class StateMachine<T> : IHandler
{
    public const string TAG = "[StateMachine]";

    private State<T> currentState;
    public State<T> CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    private State<T> globalState;
    public State<T> GlobalState
    {
        get { return globalState; }
        set { globalState = value; }
    }

    private State<T> previousState;
    public State<T> PreviousState
    {
        get { return previousState; }
        set { previousState = value; }
    }

    private T owner;

    public StateMachine(T owner)
    {
        this.owner = owner;
    }

    public void Update()
    {
        currentState.Execute(owner);
    }

    public void ChangeState(State<T> newState)
    {
        if (newState == null)
        {
            Debug.LogError("Trying to change to a null state");
            Debug.Break();
            return;
        }

        currentState.Exit(owner);
        previousState = currentState;
        CurrentState = newState;
        currentState.Enter(owner);
    }

    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }

    public bool IsInState(State<T> state)
    {
        throw new System.NotImplementedException();
    }

    public IHandler Successor
    {
        set { }
        get { return currentState; }
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
