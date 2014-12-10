using UnityEngine;
using System.Collections;

/// <summary>
/// for FSM
/// </summary>
/// <typeparam name="T"></typeparam>

public class StateMachine<TGameEntity> : IHandler
{
    private TGameEntity owner;
    private State<TGameEntity> currentState;
    private State<TGameEntity> globalState;
    private State<TGameEntity> previousState;

    public StateMachine(TGameEntity owner)
    {
        this.owner = owner;
    }

    /*
    * followings are member functions
    */
    public void Update()
    {
        currentState.Execute(owner);
    }

    public void ChangeState(State<TGameEntity> newState)
    {
        if (newState == null || IsInState(newState))
        {
            //Debug.LogError("Trying to change to a null state");
            //Debug.Break();
            return;
        }

        previousState = currentState;
        currentState = NullState.Instance as State<TGameEntity>;
        previousState.Exit(owner);
        newState.Enter(owner);
        currentState = newState;
    }

    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }

    public bool IsInState(State<TGameEntity> state)
    {
        return currentState.Equals(state);
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

    /*
    * followings are attributes
    */
    public IHandler Successor
    {
        get { return currentState; }
    }

    public State<TGameEntity> CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public State<TGameEntity> GlobalState
    {
        get { return globalState; }
        set { globalState = value; }
    }

    public State<TGameEntity> PreviousState
    {
        get { return previousState; }
        set { previousState = value; }
    }

    public const string TAG = "[StateMachine]";
}
