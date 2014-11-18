using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class Tower : GameEntity {
    public const string TAG = "[Tower]";

    private StateMachine<Tower> stateMachine;
    public StateMachine<Tower> StateMachine
    {
        get { return stateMachine; }
    }

    public Tower()
    {
        stateMachine = new StateMachine<Tower>(this);
        stateMachine.CurrentState = new TowerState_Idling();
        //stateMachine.GlobalState = new TowerState_Hitting();
    }
}
