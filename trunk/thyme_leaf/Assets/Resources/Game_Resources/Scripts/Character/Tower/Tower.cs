using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class Tower : GameEntity, IObservable_User
{
    private UISpriteAnimation anim;

    private List<IObserver_User> observers = new List<IObserver_User>();

    private StateMachine<Tower> stateMachine;

    private List<GameEntity> enemies= new List<GameEntity>();
    private GameEntity currentTarget;

    //---------------------model
    //private string description = "Super Power zzang zzang tower";

    private float maxHP = 100;
    private float currentHP = 100;

    private Weapon weapon = new Weapon();

    private float reloadingTime = 0.2f; // 재장전시간 // 재장전은 무기의 주인이 하는 것이니 여기에 정의
    //---------------------

    /*
     * followings are member functions
     */
    void Initialize()
    {
        this.stateMachine = new StateMachine<Tower>(this);
        this.stateMachine.CurrentState = TowerState_None.Instance;
        this.stateMachine.GlobalState = TowerState_Hitting.Instance;

        this.anim = GetComponent<UISpriteAnimation>();
        this.anim.Pause();
    }

    public void ChangeState(State<Tower> newState)
    {
        stateMachine.ChangeState(newState);
    }

    public void RevertToPreviousState()
    {
        stateMachine.RevertToPreviousState();
    }

    public void PlayAnimation(string name)
    {
        anim.namePrefix = name;
        anim.Play();
    }

    public void SetAttackable(bool active)
    {
        if (active)
            StartCoroutine("Attack");
        else
            StopCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(reloadingTime);
            yield return StartCoroutine(weapon.Fire(this));
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("HP : " + currentHP + " / " + maxHP);
        NotifyObservers<HealthBarView>();
    }

    public bool IsDead()
    {
        return currentHP <= 0;
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
    public override IHandler Successor
    {
        get { return stateMachine; }
    }
    public StateMachine<Tower> StateMachine
    {
        get { return stateMachine; }
    }
    public List<GameEntity> Enemies
    {
        get { return enemies; }
        set { enemies = value; }
    }
    public GameEntity CurrentTarget
    {
        get { return currentTarget; }
        set { currentTarget = value; }
    }

    public float MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }

    public float CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

    public void RegisterObserver(IObserver_User o)
    {
        observers.Add(o);
    }

    public void RemoveObserver(IObserver_User o)
    {
        observers.Remove(o);
    }

    public void NotifyObservers<TObserver>()
    {
        observers.ForEach(o => o.Refresh<TObserver>());
    }

    public void HasChanged()
    {
        throw new NotImplementedException();
    }

    public void SetChanged()
    {
        throw new NotImplementedException();
    }
}
