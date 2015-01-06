using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>

public class AutomatTower_Controller
{
    private AutomatTower view;
    private Tower model;

    public AutomatTower_Controller(AutomatTower view, Tower model)
    {
        this.view = view;
        this.model = model;
    }

    public void Attack()
    {
        model.FindBestTarget();
    }

    //public void TakeDamage(int damage)
    //{
    //    //Debug.Log("HP : " + model.HP + " / " + model.MaxHP);
    //    model.HP -= damage;

    //    if (model.IsDead())
    //    {
    //        view.ChangeState(TowerState_Dying.Instance);
    //    }
    //}

    public void EnemyEnter(GameEntity enemy)
    {
        model.AddEnemy(enemy);
    }

    public void EnemyLeave(GameEntity enemy)
    {
        model.RemoveEnemy(enemy);
    }
}

public class AutomatTower : GameEntity, IAutomatTower, IStateMachineControllable<AutomatTower>, IObserver
{
    //-------------------- Children
    [SerializeField]
    private Weapon _weapon;
    //--------------------

    private NGUISpriteAnimation anim;

    private StateMachine<AutomatTower> stateMachine;

    //--------------------- MVC
    [SerializeField]
    private Tower _model;
    private AutomatTower_Controller controller;
    //---------------------


    ////--------------------- model에 넣을 것들..
    //[SerializeField]
    //private Weapon _weapon;
    ////--------------------- ScriptableObject를 상속하기 전까지만 쓴다.

    /*
    * followings are unity callback methods
    */
    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    void OnEnable()
    {
        Reset();
        //this.PrepareUI();
    }

    void Update()
    {
        stateMachine.Update();
    }

    void OnDisable()
    {
        // MVC
        this._model = null;
        this.controller = null;

        // set state machine
        this.stateMachine = null;

        this.anim.Pause();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tag.TagProjectile)
            || other.CompareTag(Tag.TagArcherAutomart())
            || other.CompareTag(Tag.TagBarrierAutomart())
            || other.CompareTag(Tag.TagHealerAutomart())
            || other.CompareTag(Tag.TagSupporterAutomart())
            || other.CompareTag(Tag.TagWarriorAutomart())
            || other.CompareTag(Tag.TagCommandCenter)
            || other.CompareTag(Tag.TagTower)
        )
            return;

        controller.EnemyEnter(other.GetComponent<GameEntity>()); // 모든 상태에서 적은 계속 리스트에 넣어야 함. 셀링상태에서도 사용자가 취소를 누를 경우 대비
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Tag.TagProjectile)
            || other.CompareTag(Tag.TagArcherAutomart())
            || other.CompareTag(Tag.TagBarrierAutomart())
            || other.CompareTag(Tag.TagHealerAutomart())
            || other.CompareTag(Tag.TagSupporterAutomart())
            || other.CompareTag(Tag.TagWarriorAutomart())
            || other.CompareTag(Tag.TagCommandCenter)
            || other.CompareTag(Tag.TagTower)
        )
            return;

        controller.EnemyLeave(other.GetComponent<GameEntity>());
    }

    /*
     * followings are member functions
     */
    private void Initialize()
    {
        // MVC
        this._model = new Tower(this);
        this._model.RegisterObserver(this, ObserverTypes.Enemy);
        this.controller = new AutomatTower_Controller(this, _model);

        // set children
        this.Add(_weapon);

        // set state machine
        this.stateMachine = new StateMachine<AutomatTower>(this);
        this.stateMachine.CurrentState = TowerState_None.Instance;
        this.stateMachine.GlobalState = TowerState_Hitting.Instance;

        this.anim = GetComponent<NGUISpriteAnimation>();

    }

    private void Reset()
    {
        // MVC
        this._model = new Tower(this);
        this._model.RegisterObserver(this, ObserverTypes.Enemy);
        this.controller = new AutomatTower_Controller(this, _model);

        // set state machine
        this.stateMachine = new StateMachine<AutomatTower>(this);
        this.stateMachine.CurrentState = TowerState_None.Instance;
        this.stateMachine.GlobalState = TowerState_Hitting.Instance;

        this.anim.Pause();
    }

    /*
    * followings are implemented methods of "IAutomatTower"
    */
    //public void TakeDamage(int damage)
    //{
    //    controller.TakeDamage(damage);
    //}

    public void Attack()
    {
        controller.Attack();
    }

    /*
     * followings are implemented methods of "IStateMachineControllable"
    */
    public void ChangeState(State<AutomatTower> newState)
    {
        stateMachine.ChangeState(newState);
    }

    public void RevertToPreviousState()
    {
        stateMachine.RevertToPreviousState();
    }

    /*
    * followings are implemented methods of "IObserver"
    */
    public void Refresh(ObserverTypes field)
    {
        if (field == ObserverTypes.Enemy)
        {
            UpdateUI();
        }
    }


    /*
     * Followings are attributes
     */
    public override IHandler Successor
    {
        get { return stateMachine; }
    }
    public StateMachine<AutomatTower> StateMachine
    {
        get { return stateMachine; }
    }
    public NGUISpriteAnimation Anim
    {
        get { return anim; }
        set { anim = value; }
    }

    public Weapon FlameThrower
    {
        get { return _weapon; }
        set { _weapon = value; }
    }

    public Tower Model
    {
        get { return _model; }
        set { _model = value; }
    }
    public AutomatTower_Controller Controller
    {
        get { return controller; }
        set { controller = value; }
    }

    public new const string TAG = "[AutomatTower]";
}
