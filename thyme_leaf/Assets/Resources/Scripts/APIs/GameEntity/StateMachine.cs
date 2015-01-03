﻿using UnityEngine;
using System.Collections;

/// <summary>
/// for FSM
/// </summary>
/// <typeparam name="TGameEntity"></typeparam>

public class StateMachine<TGameEntity> : IHandler
{
    private TGameEntity owner;
    private State<TGameEntity> currentState ;
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
        globalState.Execute(owner);
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

        // 이 루틴은 1회성으로 실행되어야 한다.
        // 절대 재귀적인 호출은 불가함.
        previousState = currentState;
        currentState = NullState<TGameEntity>.Instance;
        previousState.Exit(owner);
        newState.Enter(owner);
        currentState = newState;

        //previousState = currentState;
        //currentState.Exit(owner);
        //currentState = newState;
        //currentState.Enter(owner);
    }

    [RPC]
    void SyncState()
    {

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
    public void OnMessage(Message msg)
    {
        //Debug.Log(TAG + "HandleMessage" + typeof(TGameEntity));
        Successor.OnMessage(msg);
    }

    public bool HandleMessage(Message msg)
    {
        throw new System.NotImplementedException();
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
