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
	public int hPoint = 100;
	public float speed = 10;
	//=====================

	private UISpriteAnimation anim;

	public ModelHero model;
	public ControllerHero controller;
	public Helper helper;

	//setting value...
	private bool alive = false;
	public string name;  //test after...chagne private
	private float offsetX, offsetY;
	private UnitType type;
	private int layer_setting;
	private MoveModeState move_state;

	public Hero target;
	public string target_name; //test debug...
	//=====================

	void Awake()
	{
		Initialize();
	}

	void Start()
	{
		SettingInitialize ();
		//3 call....???
	}
	
	void Update()
	{
		stateMachine.Update();
		//=============================
		Gesturing ();
	}

	//unit detail initialize...
	private void SettingInitialize()
	{
		//mvc setting...
		helper = new Helper (this.gameObject);
		model = new ModelHero (helper);
		controller = new ControllerHero (model, helper);


		//other reference...
		target = null;
		
		controller.setSpeed (speed / 10.0f);
		controller.setMaxHp (MaxHP);
		controller.setHp (hPoint);
		controller.setMoveOffset (offsetX, offsetY);
		controller.setID (name);
		controller.setType (UnitType.AUTOMART_CHARACTER);
		
		if(layer_setting == Layer.Automart())
		{
			gameObject.layer = Layer.Automart();
			controller.setLayer(Layer.Automart());
			Define.SetUnitType(UnitType.AUTOMART_CHARACTER);
			controller.StartPointSetting(StartPoint.AUTOMART_POINT);
		}
		else if(layer_setting == Layer.Trovant())
		{
			gameObject.layer = Layer.Trovant();
			controller.setLayer(Layer.Trovant());
			Define.SetUnitType(UnitType.TROVANT_CHARACTER);
			controller.StartPointSetting(StartPoint.TROVANT_POINT);
		}

		//move trigger & unit pool manager setting <add>...
		if(alive)
		{
			//moving state...
			stateMachine.ChangeState(HeroState_Moving.Instance);
			//moveing enable...
			controller.setMoveTrigger(true);
			
			//unit pool insert...
			UnitPoolController.GetInstance ().AddUnit (gameObject, model.getType());
		}
		else
		{
			controller.setMoveTrigger(false);
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Debug.Log ("=========================================");

		if(IsAttackCase(coll.gameObject))  //state compare okay...
	    {
			//나는 때리는 상태로
			if(target == null)
			{
				target = coll.gameObject.GetComponent<Hero>();
			}else return;

			stateMachine.ChangeState(HeroState_Attacking.Instance);
			//상대방은 쳐맞는 상태로
			coll.gameObject.GetComponent<Hero>().stateMachine.ChangeState(HeroState_Attacking.Instance);
		}
	}

	//attack mode checking...
	private bool IsAttackCase(GameObject gObj)
	{
		//add.....tag...
		if(gameObject.layer == Layer.Automart())
		{
			if(gObj.CompareTag(Tag.TagWarriorTrovant()))
			{
				return true;
			}
		}
		else if(gameObject.layer == Layer.Trovant())
		{
			if(gObj.CompareTag(Tag.TagWarriorAutomart()))
			{
				return true;
			}
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

	//==============================================
	//this functions are execute -> extern area...
	//before start this Start() function...
	public void EnableAlive() { alive = true; }
	public void DisableAlive() { alive = false; }

	public void setName(string str) { name = str; }
	public void setOffset(float offx, float offy) { offsetX = offx; offsetY = offy; }

	public void setLayer(int v) { layer_setting = v; }

	public void setMoveMode(MoveModeState option) { move_state = option; }

	public void Visiable() { gameObject.GetComponent<CircleCollider2D>().enabled = true; }

	//Get anim Function...
	public UISpriteAnimation GetAnim() { return anim; }
	//==========================

	//==========================
	public void TargetingInitialize()
	{
		if(target == null)
		{
			stateMachine.ChangeState(HeroState_Moving.Instance);
		}else target_name = target.model.getName();
	}
	//===============================================
	/*
     * followings are member functions
     */
	public void PlayAnimation(string name)
	{
		anim.namePrefix = name;
		anim.Play();
	}

	// HeroState_Dyning Animation 
	public void PlayAnimationOneTime(string name)
	{
		anim.namePrefix = name;
		anim.Play();
		anim.loop = false;
	}

	public void Initialize()
	{
		this.stateMachine = new StateMachine<Hero>(this);
		this.stateMachine.CurrentState = HeroState_None.Instance;
		this.stateMachine.GlobalState = HeroState_Hitting.Instance;
		
		this.anim = GetComponent<UISpriteAnimation>();
		this.anim.Pause();
		transform.localPosition = new Vector3(1000,1000);
	}

    public override IHandler Successor
    {
        get { return stateMachine; }
    }
}
