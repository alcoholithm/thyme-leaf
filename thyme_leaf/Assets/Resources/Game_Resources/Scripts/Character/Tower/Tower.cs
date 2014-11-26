using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class Tower : GameEntity<Tower> {
    public new const string TAG = "[Tower]";

    private UISpriteAnimation anim;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        stateMachine.Update();
    }

    /// <summary>
    /// followings are member functions
    /// </summary>
    public void PlayAnimation(string name)
    {
        anim.namePrefix = name;
        anim.Play();
    }

    public void Initialize()
    {
        this.stateMachine = new StateMachine<Tower>(this);
        this.stateMachine.CurrentState = TowerState_None.Instance;
        this.stateMachine.GlobalState = TowerState_Hitting.Instance;

        this.anim = GetComponent<UISpriteAnimation>();
        this.anim.Pause();
    }
}
