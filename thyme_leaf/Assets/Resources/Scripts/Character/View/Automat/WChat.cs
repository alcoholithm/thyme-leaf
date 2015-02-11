using UnityEngine;
using System.Collections;

public interface IWChat
{
    void TakeDamage(int damage);
}

public class WChat_Controller
{
    private WChat view;
    private CommandCenter model;

    public WChat_Controller(WChat view, CommandCenter model)
    {
        this.view = view;
        this.model = model;
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("HP : " + model.HP + " / " + model.MaxHP);
        model.TakeDamageWithDEF(damage);

        if (model.IsDead())
        {
            view.ChangeState(WChatState_Dying.Instance);
        }
    }
}


public class WChat : GameEntity, IWChat, IStateMachineControllable<WChat>, IObserver
{
    //-------------------- Children
    [SerializeField]
    private HealthBar healthbar;
    //--------------------

    private NGUISpriteAnimation anim;

    private StateMachine<WChat> stateMachine;

    //--------------------- MVC
    private CommandCenter model;
    private WChat_Controller controller;

    //---------------------
	private UnitObject my_unit;
	private Vector3 position_node;
    /*
    * followings are unity callback methods
    */
    void Awake()
    {
        Initialize();
    }

    void OnEnable()
    {
        Reset();
    }

    //void OnEnable()
    //{
    //    Initialize();   // 귀찮아서 이렇게 한다. 원래는 Reset을 만들고 다시 new 로 인스턴시에이션 할 필요없이 각 클래스의 초기화루틴을 호출한다.
    //                    // 그런 연후에 상위 클래스인 게임엔티티클래스에서 OnEnable을 구현하고 초기화 함수는 서브클래스에 위임하는 등의 공통 인터페이스를 완성해야 하는데 ㅅㅂ 너무 귀찮다....아ㅏㅏㅏㅏㅏㅏㅏㅏ
    //}

    void Update()
    {
        stateMachine.Update();
    }

//    void OnDisable()
//    {
//        this.model.RemoveObserver(this, ObserverTypes.Health);
//        // MVC
//        this.model = null;
//        this.controller = null;
//
//        // set children
//        this.healthbar.Model = null;
//        this.Remove(healthbar);
//
//        // set state machine
//        this.stateMachine = null;
//
//        this.anim = null;
//    }

    /*
     * followings are member functions
     */
    private void Initialize()
    {
        // MVC
        this.model = GetComponent<CommandCenter>();
        this.controller = new WChat_Controller(this, model);

        this.model.RegisterObserver(this, ObserverTypes.Health);

        // set children
        this.healthbar.Model = this.model;
        this.Add(healthbar);

        // set state machine
        this.stateMachine = new StateMachine<WChat>(this);
        this.stateMachine.CurrentState = WChatState_None.Instance;
        this.stateMachine.GlobalState = WChatState_Hitting.Instance;

        this.anim = GetComponent<NGUISpriteAnimation>();
    }

    private void Reset()
    {
        // MVC
        //this.model = new Tower(this);
        //this.model.RegisterObserver(this, ObserverTypes.Enemy);
        //this.controller = new AutomatTower_Controller(this, model);

        // set state machine
//        this.stateMachine = new StateMachine<WChat>(this);
//        this.stateMachine.CurrentState = WChatState_None.Instance;
//        this.stateMachine.GlobalState = WChatState_Hitting.Instance;

        this.anim = GetComponent<NGUISpriteAnimation>();
        this.anim.Pause();

        this.PrepareUI();
    }

    /*
     * Followings are implemented methods of "IWChat"
     */
    public void TakeDamage(int damage)
    {
        controller.TakeDamage(damage);
    }

    /*
     * Followings are implemented methods of "IStateMachineControllable"
     */
    public void ChangeState(State<WChat> newState)
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

	public void Emergency()
	{
		Debug.Log ("Automat Emergency Spawn!!");
		StartCoroutine ("EmergencySpawnUnit");
	}
	
	IEnumerator EmergencySpawnUnit()
	{
		for (int i = 0; i < 5; i++)
		{
			Hero obj = Spawner.Instance.GetHero(AutomatType.FRANSIS_TYPE1);
			obj.helper.setPos(position_node);
			for (float timer = 0; timer < 0.5f; timer += Time.deltaTime)
			{
				yield return null;
			}
		}
		Debug.Log ("Automat Emergency Spawn Exit");
	}

	void OnClick()
	{
		//my selected center position
		Define.selected_center = position_node;
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
        get { return model; }
        set { model = value; }
    }

    public WChat_Controller Controller
    {
        get { return controller; }
        set { controller = value; }
    }

    public Vector3 PositionNode
    {
        get { return position_node; }
        set { position_node = value; }
    }

	public UnitObject MyUnit
	{
		get { return my_unit; }
		set { my_unit = value; }
	}

    public new const string TAG = "[W_Chat]";
}
