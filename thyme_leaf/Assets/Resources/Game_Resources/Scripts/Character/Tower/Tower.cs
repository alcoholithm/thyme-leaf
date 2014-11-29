using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class Tower : GameEntity<Tower>
{
    private UISpriteAnimation anim;

    /*
     * followings are unity callback methods
     */ 
    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        stateMachine.Update();
    }

    /*
     * followings are member functions
     */
    public void Initialize()
    {
        this.stateMachine = new StateMachine<Tower>(this);
        this.stateMachine.CurrentState = TowerState_None.Instance;
        this.stateMachine.GlobalState = TowerState_Hitting.Instance;

        this.anim = GetComponent<UISpriteAnimation>();
        //this.anim.Pause();
    }

    public void PlayAnimation(string name)
    {
        anim.namePrefix = name;
        anim.Play();
    }

    public new const string TAG = "[Tower]";
}
