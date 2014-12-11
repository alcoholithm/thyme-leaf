using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class Tower : GameEntity
{
    private UISpriteAnimation anim;

    private StateMachine<Tower> stateMachine;

    private List<GameEntity> enemies;
    private GameEntity currentTarget;

    //---------------------
    private string description = "Super Power zzang zzang tower";

    private float MaxHP = 100;
    private float CurrentHP = 100;

    private Weapon weapon;

    private float reloadingTime; // 재장전시간 // 재장전은 무기의 주인이 하는 것이니 여기에 정의
    //---------------------

    /*
     * followings are member functions
     */
    void Initialize()
    {
        this.stateMachine = new StateMachine<Tower>(this);
        this.stateMachine.CurrentState = TowerState_None.Instance;
        this.stateMachine.GlobalState = TowerState_Hitting.Instance;

        this.enemies = new List<GameEntity>();

        this.anim = GetComponent<UISpriteAnimation>();
        this.anim.Pause();
    }

    public void ChangeState(State<Tower> newState)
    {
        stateMachine.ChangeState(newState);
    }

    public void PlayAnimation(string name)
    {
        anim.namePrefix = name;
        anim.Play();
    }

    public IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(reloadingTime);
            yield return weapon.Fire(currentTarget);

            //currentTarget.GetComponent<Hero>();
            //currentTarget.ObtainMessage(MessageTypes.MSG_DAMAGE, new AttackCommand<(currentTarget.GetComponent<Hero>(), power));
        }
    }

    public void TakeDamage()
    {
        throw new NotImplementedException();
    }

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

    void OnTriggerEnter2D(Collider2D other)
    {
        enemies.Add(other.GetComponent<GameEntity>()); // 모든 상태에서 적은 계속 리스트에 넣어야 함. 셀링상태에서도 사용자가 취소를 누를 경우 대비

        Message msg =
            this.ObtainMessage<Tower>(
            MessageTypes.MSG_ENEMY_ENTER,
            tower => tower.ChangeState(TowerState_Attacking.Instance));

        this.DispatchMessage(msg);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        enemies.Remove(other.GetComponent<GameEntity>());

        Message msg =
            this.ObtainMessage<Tower>(
            MessageTypes.MSG_ENEMY_LEAVE,
            tower => tower.ChangeState(TowerState_Idling.Instance));
        this.DispatchMessage(msg);
    }

    /*
     * followings are attributes
     */
    public new const string TAG = "[Tower]";
    public List<GameEntity> Enemies
    {
        get { return enemies; }
        set { enemies = value; }
    }

    public StateMachine<Tower> StateMachine
    {
        get { return stateMachine; }
    }

    public override IHandler Successor
    {
        get { return stateMachine; }
    }
}
