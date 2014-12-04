using UnityEngine;
using System.Collections;

public class Hero : GameEntity<Hero> {
	public new const string TAG = "[Hero]";

	//=====================
	//unit identity value
	public int hPoint = 100;
	public float speed = 10;
	//=====================

	private UISpriteAnimation anim;

	public ModelUnit model;
	public ControllerUnit controller;
	public HelperUnit helper;
	
	private bool alive;
	private string name;

	public Hero target;

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
		controller.setHp(hPoint);
		//Debug.Log (hPoint);
		Gesturing ();
	}
	

	void OnCollisionEnter2D(Collision2D coll) 
	{
			target = coll.gameObject.GetComponent<Hero>();
			if(gameObject.CompareTag(Tag.TagWarriorTrovant()))
		   {
				//나는 때리는 상태로
				stateMachine.ChangeState(HeroState_Attacking.Instance);
				//상대방은 쳐맞는 상태로
				coll.gameObject.GetComponent<Hero>().stateMachine.ChangeState(HeroState_Hitting.Instance);
			}

	}


	private void SettingInitialize()
	{
		helper = new HelperUnit (this.gameObject);
		model = new ModelUnit (helper);
		controller = new ControllerUnit (model, helper);
		
		stateMachine.ChangeState(HeroState_Moving.Instance);
		controller.setSpeed (speed / 10.0f);

		controller.setHp (hPoint);
		
		if(alive) controller.setMoveTrigger(true);
		else controller.setMoveTrigger(false);

		controller.setID (name);            
	}

	//gesturing function
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
	//this functions are execute -> extern area
	//before start this Start() function
	public void EnableAlive() { alive = true; }
	public void DisableAlive() { alive = false; }

	public void setName(string str) { name = str; }

	public void Visiable() { gameObject.GetComponent<CircleCollider2D>().enabled = true; }
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

	//Get anim Function
	public UISpriteAnimation GetAnim() {
		return anim;
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
}
