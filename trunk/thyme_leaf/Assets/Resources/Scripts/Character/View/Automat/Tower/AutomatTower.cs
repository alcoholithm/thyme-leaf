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

    public IEnumerator Attack()
    {
        return model.Attack();
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("HP : " + model.HP + " / " + model.MaxHP);
        model.HP -= damage;

        if (model.IsDead())
        {
            view.ChangeState(TowerState_Dying.Instance);
        }
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

public class AutomatTower : GameEntity, IStateMachineControllable<AutomatTower>, IObserver
{
    //-------------------- Children
    [SerializeField]
    private HealthBar healthbar;
    //--------------------

    private NGUISpriteAnimation anim;

    private StateMachine<AutomatTower> stateMachine;

    //--------------------- MVC
    [SerializeField]
    private Tower _model;
    private AutomatTower_Controller controller;
    //---------------------


    /*
    * followings are unity callback methods
    */
    void Awake()
    {
        Initialize();
    }

    void OnEnable()
    {
        Initialize(); // 귀찮아서 이렇게 한다. 원래는 Reset을 만들고 다시 new 로 인스턴시에이션 할 필요없이 각 클래스의 초기화루틴을 호출한다.

        this.PrepareUI();
    }

    void Update()
    {
        stateMachine.Update();
    }

    void OnDisable()
    {
        this._model.RemoveObserver(this, ObserverTypes.Health);

        // MVC
        this._model = null;
        this.controller = null;

        // set children
        this.healthbar.Model = null;
        this.Remove(healthbar);

        // set state machine
        this.stateMachine = null;

        this.anim.Pause();
        this.anim = null;
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
        this.controller = new AutomatTower_Controller(this, _model);

        // set children
        this.healthbar.Model = this._model;
        this.Add(healthbar);

        // set state machine
        this.stateMachine = new StateMachine<AutomatTower>(this);
        this.stateMachine.CurrentState = TowerState_None.Instance;
        this.stateMachine.GlobalState = TowerState_Hitting.Instance;

        this.anim = GetComponent<NGUISpriteAnimation>();
        this.anim.Pause();

        this._model.RegisterObserver(this, ObserverTypes.Health);
    }

    /*
    * followings are implemented methods of "IAgt"
    */
    public void TakeDamage(int damage)
    {
        controller.TakeDamage(damage);
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
    public StateMachine<AutomatTower> StateMachine
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
    public AutomatTower_Controller Controller
    {
        get { return controller; }
        set { controller = value; }
    }

    public new const string TAG = "[Agt_Type1]";
}
