using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	//setting value...59.47
	private float offsetX, offsetY;
	private UnitType type;

	public Hero target;
	public string my_name;

	[HideInInspector]
	public MHero model;
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

		//angle checking...

	}

	//unit detail initialize...
	public void SettingInitialize()
	{
		Debug.Log ("SettingInitialize");
		//mvc setting...
		helper = new Helper (this.gameObject);
		model = new MHero (helper);
		controller = new ControllerHero (model, helper); 

		//other reference...
		target = null;

		float range_value = 100;
		setOffset (Random.Range (-range_value, range_value), Random.Range (-range_value, range_value));

		controller.setSpeed (speed / 10.0f);
		controller.setMaxHp (MaxHP);
		controller.setHp (MaxHP);
		controller.setMoveOffset (offsetX, offsetY);

		helper.setMoveTrigger (false);
		helper.collision_range = gameObject.GetComponent<CircleCollider2D>().radius;

		health_bar_controller = transform.GetChild (0).GetChild(0).gameObject.GetComponent<HealthBarView> ();
		Debug.Log (health_bar_controller);
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
				//my state is attaking...
				stateMachine.ChangeState(HeroState_Attacking.Instance);
			}
		}
		else if(stateMachine.CurrentState == HeroState_Attacking.Instance)
		{
//			if(IsAttackCase(coll.gameObject))  //state compare okay...
//			{
//				helper.attack_target = coll.gameObject.GetComponent<Hero>();
//			}
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

	public void ChangingAnimationAngle()
	{

	}

	public void HealthUpdate()
	{
//		health_bar_controller.UpdateUI();
	}

	//==============================================
	public void setName(string str) { name = str; }
	public void setOffset(float offx, float offy) { offsetX = offx; offsetY = offy; }

	public void setLayer(Layer v) { gameObject.layer = (int) v; }
	public Layer getLayer() { return (Layer) gameObject.layer; }

	public void CollisionSetting(bool trigger) { gameObject.GetComponent<CircleCollider2D>().enabled = trigger; }

	public void Die(){ helper.setPos (1000, 1000, 0); CollisionSetting (false); }

	//get anim Function...
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
