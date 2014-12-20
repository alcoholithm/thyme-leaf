using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>

interface IAttackable
{
    IEnumerator Attack();
}

interface IMovalble
{
    void Move();
}


interface IAgt : IAttackable
{
    void TakeDamage(int damage);
    void CheckDeath();
}

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

    internal void EnemyLeave(GameEntity enemy)
    {
        model.RemoveEnemy(enemy);
    }
}

public class Agt_Type1 : GameEntity, IAgt, IStateMachineControllable<Agt_Type1>, IObserver, IView
{
    private NGUISpriteAnimation anim;

    [SerializeField]
    private HealthBarView healthbar;

    private StateMachine<Agt_Type1> stateMachine;

    //---------------------MVC
    [SerializeField]
    private Tower model;
    private Agt_Controller controller;

    //---------------------

    // 만약 스트링을 보내지 않고 이넘 값을 보내도 되는 걸로 테스트가 완료 되면 스트링 대신 이넘으로 고치기
    //public const string FUNCTION_ATTACK = "attack";
    //public const string FUNCTION_TAKEDAMAGE = "takeDamage";
    //public const string FUNCTION_TAKEDAMAGE = "isDead";

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
        this.model = new Tower(this);
        this.controller = new Agt_Controller(this, model);

        //State machine
        this.stateMachine = new StateMachine<Agt_Type1>(this);
        this.stateMachine.CurrentState = TowerState_None.Instance;
        this.stateMachine.GlobalState = TowerState_Hitting.Instance;

        this.anim = GetComponent<NGUISpriteAnimation>();
        //this.anim.Pause();

        this.healthbar.Model = this.model;

        model.RegisterObserver(this, ObserverTypes.Health);
    }


    public void SetAttackable(bool active)
    {
        if (active)
            StartCoroutine("Attack");
        else
            StopCoroutine("Attack");
    }

    /*
    * followings are implemented methods of "IATT_Type1"
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
     * followings are implemented methods of "IView"
    */
    public void ActionPerformed(string actionCommand)
    {
        throw new NotImplementedException();
    }

    public void UpdateUI()
    {
        healthbar.UpdateUI();
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
        get { return model; }
        set { model = value; }
    }

    public Agt_Controller Controller
    {
        get { return controller; }
        set { controller = value; }
    }

    public new const string TAG = "[Tower]";
}
