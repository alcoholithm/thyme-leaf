using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class Tower : GameEntity<Tower> {
    public new const string TAG = "[Tower]";

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

    public void PlayAnimation(string name)
    {
        anim.namePrefix = name;
    }
}
