using UnityEngine;
using System.Collections;

public class Hero : GameEntity {
	public new const string TAG = "[Hero]";

    private StateMachine<Hero> stateMachine;

    public StateMachine<Hero> StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }

	//=====================
	//unit identity value
	public int MaxHP = 100;
	public int CurrentHP = 100;
	public float speed = 10;
	//=====================

	private NGUISpriteAnimation anim;	
	private HealthBarView health_bar_controller;

	//setting value...
	private bool alive = false;
	public string name;  //test after...chagne private
	private float offsetX, offsetY;
	private UnitType type;
	private int layer_setting;

	public Hero target;
	public string target_name; //test debug...

	[HideInInspector]
	public ModelHero model;
	public ControllerHero controller;
	public Helper helper;

	//=====================

	void Awake()
	{
		Initialize();
	}

	void OnEnable()
	{
		SettingInitialize ();
	}
	
	void Update()
	{
		stateMachine.Update();
		//=============================
		Gesturing ();
	}

	//unit detail initialize...
	public void SettingInitialize()
	{
		Debug.Log ("SettingInitialize");
		//mvc setting...
		helper = new Helper (this.gameObject);
		model = new ModelHero (helper);
		controller = new ControllerHero (model, helper);

		//other reference...
		target = null;

		float range_value = 100;
		setOffset (Random.Range (-range_value, range_value), Random.Range (-range_value, range_value));
		
		controller.setSpeed (speed / 10.0f);
		controller.setMaxHp (MaxHP);
		controller.setHp (CurrentHP);
		controller.setMoveOffset (offsetX, offsetY);

		helper.setMoveTrigger (false);
		helper.collision_range = gameObject.GetComponent<CircleCollider2D>().radius;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		//coll return checking...
		if(coll == null) return;

		if(stateMachine.CurrentState == HeroState_Moving.Instance)
		{
			if(IsAttackCase(coll.gameObject))  //state compare okay...
		    {
				helper.attack_target = coll.gameObject.GetComponent<Hero>();
				//나는 때리는 상태로
				stateMachine.ChangeState(HeroState_Attacking.Instance);
				//상대방은 쳐맞는 상태로
				coll.gameObject.GetComponent<Hero>().stateMachine.ChangeState(HeroState_Attacking.Instance);
			}
		}
		else if(stateMachine.CurrentState == HeroState_Attacking.Instance)
		{
			if(IsAttackCase(coll.gameObject))  //state compare okay...
			{
				helper.attack_target = coll.gameObject.GetComponent<Hero>();
			}
		}
	}

	//attack mode checking...
	private bool IsAttackCase(GameObject gObj)
	{
		//add.....tag...
		Layer type = getLayer();

		switch (type) {
		case Layer.Automart:
			if(Layer.Trovant == (Layer) gObj.layer)
			{
				return true;
			}
			break;
		case Layer.Trovant:
			if(Layer.Automart == (Layer) gObj.layer)
			{
				return true;
			}
			break;
		}
		return false;
	}

	//gesturing function...
	private void Gesturing()
	{
		if(controller.isGesture())
		{
			if(Input.GetMouseButtonDown(0))
			{
				helper.gesture_startpoint = Input.mousePosition;
			}
			else if(Input.GetMouseButtonUp(0))
			{
				helper.gesture_endpoint = Input.mousePosition;
				if(helper.SelectPathNode(helper.gesture_startpoint, helper.gesture_endpoint))
				{
					controller.setMoveTrigger(true);
				}
			}
		}
	}

	public void HealthUpdate()
	{
		//health_bar_controller.UpdateHealthBar_Test (this.gameObject);
	}

	//==============================================
	//this functions are execute -> extern area...
//	public void EnableAlive() { health_bar_controller = transform.GetChild (0).GetChild(0).gameObject.GetComponent<HealthBarView> (); }
	public void setName(string str) { name = str; }
	public void setOffset(float offx, float offy) { offsetX = offx; offsetY = offy; }

	public void setLayer(Layer v) { gameObject.layer = (int) v; }
	public Layer getLayer() { return (Layer) gameObject.layer; }

	public void CollisionVisiable() { gameObject.GetComponent<CircleCollider2D>().enabled = true; }

	//Get anim Function...
	public UISpriteAnimation GetAnim() { return anim; }
	//==========================

	//===============================================
	/*
     * followings are member functions
     */

	public NGUISpriteAnimation Anim
	{
		get { return anim; }
		set { anim = value; }
	}


	public void Initialize()
	{
		this.stateMachine = new StateMachine<Hero>(this);
		this.stateMachine.CurrentState = HeroState_None.Instance;
		this.stateMachine.GlobalState = HeroState_Hitting.Instance;
		
		this.anim = GetComponent<NGUISpriteAnimation>();
		this.anim.Pause();
		transform.localPosition = new Vector3(1000,1000);
	}

    public override IHandler Successor
    {
        get { return stateMachine; }
    }
}
