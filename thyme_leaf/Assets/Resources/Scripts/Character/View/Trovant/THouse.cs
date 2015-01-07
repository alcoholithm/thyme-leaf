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
        Debug.Log("HP : " + model.HP + " / " + model.MaxHP);

        model.TakeDamageWithDEF(damage);

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
    private CommandCenter model;
    private THouse_Controller controller;

    //---------------------
	private UnitObject my_unit;
	private Vector3 position_node;
	private CenterWaveStruct wave_table;
	private int wave_count = -1;

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

    void Update()
    {
        stateMachine.Update();
    }

//    void OnDisable()
//    {
//        this.model.RemoveObserver(this, ObserverTypes.Health);
//
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
        this.controller = new THouse_Controller(this, model);

        this.model.RegisterObserver(this, ObserverTypes.Health);

        // set children
        this.healthbar.Model = this.model;
        this.Add(healthbar);

        // set state machine
        this.stateMachine = new StateMachine<THouse>(this);
        this.stateMachine.CurrentState = THouseState_None.Instance;
        this.stateMachine.GlobalState = THouseState_Hitting.Instance;

        this.anim = GetComponent<NGUISpriteAnimation>();
    }

    private void Reset()
    {
        // MVC
        //this.model = new Tower(this);
        //this.model.RegisterObserver(this, ObserverTypes.Enemy);
        //this.controller = new AutomatTower_Controller(this, model);

        // set state machine
        this.stateMachine = new StateMachine<THouse>(this);
        this.stateMachine.CurrentState = THouseState_None.Instance;
        this.stateMachine.GlobalState = THouseState_Hitting.Instance;

        this.anim = GetComponent<NGUISpriteAnimation>();
        this.anim.Pause();
        this.PrepareUI();
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

	//wave system setting...
	public void WaveSystemStart(CenterWaveStruct v)
	{
		wave_table = v;
		wave_count = 0;
		Debug.Log ("wave okay call!!");
		StartCoroutine ("WaveRoutine", wave_count);
	}
	
	IEnumerator WaveRoutine(int idx)
	{
		if(idx >= wave_table.wave_setting_value_set.Length)
		{
			Debug.Log("wave exit");
			yield return null;
		}
		else
		{
			Debug.Log ("wave start!!");
			yield return new WaitForSeconds(wave_table.wave_setting_value_set[idx].first_delay_time);
			
			stateMachine.ChangeState(THouseState_Waving.Instance);  //change wave state~~!!!...
			
			StartCoroutine ("WaveSpawnUnit", idx);
		}
	}
	
	IEnumerator WaveSpawnUnit(int idx)
	{
		for (int i = 0; i < wave_table.wave_setting_value_set[idx].unit_num; i++)
		{
			Hero obj = Spawner.Instance.GetTrovant (wave_table.wave_setting_value_set[idx].unit_type);
			obj.controller.StartPointSetting(position_node);
			for (float timer = 0; timer < wave_table.wave_setting_value_set[idx].unit_delay_time; timer += Time.deltaTime)
			{
				yield return null;
			}
		}
		Debug.Log ((wave_count+1) + " wave exit");
		wave_count++;
		
		stateMachine.ChangeState (THouseState_Idling.Instance);  //change idling state~~!!!...

		StartCoroutine ("WaveRoutine", wave_count);
	}
	
	public void Emergency()
	{
		Debug.Log ("Trovant Emergency Spawn!!");
		StartCoroutine ("EmergencySpawnUnit");
	}
	
	IEnumerator EmergencySpawnUnit()
	{
		for (int i = 0; i < 5; i++)
		{
			Hero obj = Spawner.Instance.GetTrovant (TrovantType.COMMA);
			obj.controller.StartPointSetting(position_node);  //my center position okay...
			for (float timer = 0; timer < 0.5f; timer += Time.deltaTime)
			{
				yield return null;
			}
		}
		Debug.Log ("Trovant Emergency Spawn Exit");
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

    public THouse_Controller Controller
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
