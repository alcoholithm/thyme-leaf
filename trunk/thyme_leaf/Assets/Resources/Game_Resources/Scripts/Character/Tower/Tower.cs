using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class Tower : GameEntity<Tower>
{
    private UISpriteAnimation anim;

    private List<GameObject> enemies;

    /*
     * followings are unity callback methods
     */
    void Awake()
    {
        Initialize();
    }

    //void Update()
    //{
    //    stateMachine.Update();
    //}

    IEnumerator Update()
    {
        return stateMachine.Update();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        enemies.Add(other.gameObject); // 모든 상태에서 적은 계속 리스트에 넣어야 함. 셀링상태에서도 사용자가 취소를 누를 경우 대비

        Message msg =
            this.ObtainMessage<Tower>(
            MessageTypes.MSG_ENEMY_ENTER,
            tower => tower.ChangeState(TowerState_Attacking.Instance));

        this.DispatchMessage(msg);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        enemies.Remove(other.gameObject);

        Message msg =
            this.ObtainMessage<Tower>(
            MessageTypes.MSG_ENEMY_LEAVE,
            tower => tower.ChangeState(TowerState_Idling.Instance));
        this.DispatchMessage(msg);
    }

    /*
     * followings are member functions
     */
    public void Initialize()
    {
        this.stateMachine = new StateMachine<Tower>(this);
        this.stateMachine.CurrentState = TowerState_None.Instance;
        this.stateMachine.GlobalState = TowerState_Hitting.Instance;

        this.enemies = new List<GameObject>();

        this.anim = GetComponent<UISpriteAnimation>();
        this.anim.Pause();
    }

    public void PlayAnimation(string name)
    {
        anim.namePrefix = name;
        anim.Play();
    }

    /*
     * 
     */ 
    public new const string TAG = "[Tower]";
    public List<GameObject> Enemies
    {
        get { return enemies; }
        set { enemies = value; }
    }
}
