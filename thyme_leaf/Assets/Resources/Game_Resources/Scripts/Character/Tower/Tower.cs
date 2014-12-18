﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>

public class Tower : GameEntity, IObservable
{
    private NGUISpriteAnimation anim;

    private Dictionary<ObserverTypes, List<IObserver>> observers = new Dictionary<ObserverTypes, List<IObserver>>();

    private StateMachine<Tower> stateMachine;

    private List<GameEntity> enemies = new List<GameEntity>();
    private GameEntity currentTarget;

    //---------------------model
    //private string description = "Super Power zzang zzang tower";

    [SerializeField]
    //private TowerModel towerModel;
    private float maxHP = 100;
    private float currentHP = 100;

    [SerializeField]
    private Weapon weapon;

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

        this.anim = GetComponent<NGUISpriteAnimation>();
        this.anim.Pause();

        this.weapon = new Weapon(this);
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(reloadingTime);
            //yield return StartCoroutine(weapon.Fire(this));
            if (currentTarget != null) // 임시코드
                yield return StartCoroutine(weapon.Fire(currentTarget));
        }
    }

    /*
     * followings are public member functions
     */
    public void ChangeState(State<Tower> newState)
    {
        stateMachine.ChangeState(newState);
    }

    public void RevertToPreviousState()
    {
        stateMachine.RevertToPreviousState();
    }

    public void SetAttackable(bool active)
    {
        if (active)
            StartCoroutine("Attack");
        else
            StopCoroutine("Attack");
    }



    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("HP : " + currentHP + " / " + maxHP);
        NotifyObservers(ObserverTypes.Health);
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
        if (other.CompareTag("TagProjectile"))
            return;

        enemies.Add(other.GetComponent<GameEntity>()); // 모든 상태에서 적은 계속 리스트에 넣어야 함. 셀링상태에서도 사용자가 취소를 누를 경우 대비
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TagProjectile"))
            return;

        enemies.Remove(other.GetComponent<GameEntity>());
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

    public NGUISpriteAnimation Anim
    {
        get { return anim; }
        set { anim = value; }
    }

    public void RegisterObserver(IObserver o, ObserverTypes field)
    {
        if (!observers.ContainsKey(field))
        {
            observers.Add(field, new List<IObserver>());
        }
        observers[field].Add(o);
    }

    public void RemoveObserver(IObserver o, ObserverTypes field)
    {
        if (observers[field].Count <= 1)
            observers.Remove(field);
        else
            observers[field].Remove(o);
    }

    public void NotifyObservers(ObserverTypes field)
    {
        observers[field].ForEach(o => o.Refresh(field));
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
