using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class Tower : GameEntity {
    public new const string TAG = "[Tower]";

    private StateMachine<Tower> stateMachine;
    public StateMachine<Tower> StateMachine
    {
        get { return stateMachine; }
    }

    [SerializeField]
    UISpriteAnimation anim;

    void Awake()
    {
        stateMachine = new StateMachine<Tower>(this);
        stateMachine.CurrentState = TowerState_None.Instance;
        stateMachine.GlobalState = TowerState_Hitting.Instance;

        //stateMachine.CurrentState = gameObject.AddComponent<TowerState_None>();
    }

    void Update()
    {
        stateMachine.Update();
    }

    public override void HandleMessage(Message msg)
    {
        stateMachine.ChangeState(TowerState_Building.Instance);
    }

    public void PlayAnimation(string name)
    {
        anim.namePrefix = name;
    }
}
