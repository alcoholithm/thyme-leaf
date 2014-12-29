using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>

public class Agt_Controller
{
    private Agt_Type1 view;
    private Tower model;

    public Agt_Controller(Agt_Type1 view, Tower model)
    {
        this.view = view;
        this.model = model;
    }

    public IEnumerator Attack()
    {
        return model.Attack();
    }

    public void TakeDamage(int damage)
    {
        model.HP -= damage;
        Debug.Log("HP : " + model.HP + " / " + model.MaxHP);

        if (model.IsDead())
        {
            view.ChangeState(TowerState_Dying.Instance);
        }
    }

    public bool IsDead()
    {
        return model.IsDead();
    }

    public void EnemyEnter(GameEntity enemy)
    {
        model.AddEnemy(enemy);
    }

    public void EnemyLeave(GameEntity enemy)
    {
        model.RemoveEnemy(enemy);
    }
}

public class Agt_Type1 : GameEntity, IAgt, IStateMachineControllable<Agt_Type1>, IObserver
{
    //-------------------- Children
    [SerializeField]
    private HealthBar healthbar;
    //--------------------

    private NGUISpriteAnimation anim;

    private StateMachine<Agt_Type1> stateMachine;

    //--------------------- MVC
    [SerializeField]
    private Tower _model;
    private Agt_Controller controller;
    //---------------------


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

        controller.EnemyEnter(other.GetComponent<GameEntity>()); // 모든 상태에서 적은 계속 리스트에 넣어야 함. 셀링상태에서도 사용자가 취소를 누를 경우 대비
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TagProjectile"))
            return;

        controller.EnemyLeave(other.GetComponent<GameEntity>());
    }

    /*
     * followings are member functions
     */
    void Initialize()
    {
        // MVC
        this._model = new Tower(this);
        this.controller = new Agt_Controller(this, _model);

        // set children
        this.healthbar.Model = this._model;
        this.Add(healthbar);

        // set state machine
        this.stateMachine = new StateMachine<Agt_Type1>(this);
        this.stateMachine.CurrentState = TowerState_None.Instance;
        this.stateMachine.GlobalState = TowerState_Hitting.Instance;

        this.anim = GetComponent<NGUISpriteAnimation>();
        //this.anim.Pause();


        this._model.RegisterObserver(this, ObserverTypes.Health);
    }
    
    /*
    * followings are implemented methods of "IAgt"
    */
    public void TakeDamage(int damage)
    {
        controller.TakeDamage(damage);
    }

    public void CheckDeath()
    {
        controller.IsDead();
    }

    public IEnumerator Attack() //  controller 안에 들어가야한다.
    {
        return controller.Attack();
    }

    public void SetAttackable(bool active)
    {
        if (active)
            StartCoroutine("Attack");
        else
            StopCoroutine("Attack");
    }

    /*
     * followings are implemented methods of "IStateMachineControllable"
    */
    public void ChangeState(State<Agt_Type1> newState)
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
        if (field == ObserverTypes.Health)
        {
            UpdateUI();
        }
    }


    /*
     * followings are attributes
     */
    public override IHandler Successor
    {
        get { return stateMachine; }
    }
    public StateMachine<Agt_Type1> StateMachine
    {
        get { return stateMachine; }
    }
    public NGUISpriteAnimation Anim
    {
        get { return anim; }
        set { anim = value; }
    }

    public Tower Model
    {
        get { return _model; }
        set { _model = value; }
    }
    public Agt_Controller Controller
    {
        get { return controller; }
        set { controller = value; }
    }

    public new const string TAG = "[Agt_Type1]";
}
