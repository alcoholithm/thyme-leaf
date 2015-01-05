using UnityEngine;
using System.Collections;

public interface ITHouse
{
    void TakeDamage(int damage);
}

public class THouse_Controller
{
    private THouse view;
    private CommandCenter model;

    public THouse_Controller(THouse view, CommandCenter model)
    {
        this.view = view;
        this.model = model;
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("HP : " + model.HP + " / " + model.MaxHP);
        model.HP -= damage;

        if (model.IsDead())
        {
            view.ChangeState(THouseState_Dying.Instance);
        }
    }
}


public class THouse : GameEntity, ITHouse, IStateMachineControllable<THouse>, IObserver
{
    //-------------------- Children
    [SerializeField]
    private HealthBar healthbar;
    //--------------------

    private NGUISpriteAnimation anim;

    private StateMachine<THouse> stateMachine;

    //--------------------- MVC
    [SerializeField]
    private CommandCenter _model;
    private THouse_Controller controller;

    //---------------------
	private GameObject position_node;

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
        healthbar.gameObject.SetActive(false);
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

        this.anim = null;
    }

    /*
     * followings are member functions
     */
    void Initialize()
    {
        // MVC
        this._model = new CommandCenter();
        this.controller = new THouse_Controller(this, _model);

        // set children
        this.healthbar.Model = this._model;
        this.Add(healthbar);

        // set state machine
        this.stateMachine = new StateMachine<THouse>(this);
        this.stateMachine.CurrentState = THouseState_None.Instance;
        this.stateMachine.GlobalState = THouseState_Hitting.Instance;

        this._model.RegisterObserver(this, ObserverTypes.Health);

        this.anim = GetComponent<NGUISpriteAnimation>();
        this.anim.Pause();

    }

    /*
     * Followings are implemented methods of "IW_Chat"
     */
    public void TakeDamage(int damage)
    {
        controller.TakeDamage(damage);
    }

    /*
     * Followings are implemented methods of "IStateMachineControllable"
     */
    public void ChangeState(State<THouse> newState)
    {
        stateMachine.ChangeState(newState);
    }

    public void RevertToPreviousState()
    {
        stateMachine.RevertToPreviousState();
    }

    /*
     * Followings are implemented methods of "IObserver"
     */
    public void Refresh(ObserverTypes field)
    {
        if (field == ObserverTypes.Health)
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
    public NGUISpriteAnimation Anim
    {
        get { return anim; }
        set { anim = value; }
    }
    public CommandCenter Model
    {
        get { return _model; }
        set { _model = value; }
    }

    public THouse_Controller Controller
    {
        get { return controller; }
        set { controller = value; }
    }

    public GameObject PositionNode
    {
        get { return position_node; }
        set { position_node = value; }
    }

    public new const string TAG = "[W_Chat]";
}
