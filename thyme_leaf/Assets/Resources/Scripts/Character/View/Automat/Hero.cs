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
	private HealthBar health_bar_controller;
	
	public Hero target;
	public string my_name;  //test code...
	public string state_name; //test code...
	public string muster_name; //test code...

	//extra...
	private bool onlyfirst;
	private string hit_unit;

	[HideInInspector]
	public MHero model;
	[HideInInspector]
	public ControllerHero controller;
	[HideInInspector]
	public Helper helper;

	[HideInInspector]
	public Transform health_bar_body;
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
		//animation control...
		ChangingAnimationAngle ();
		//muster control...
		MusterControl ();
	}

	//unit detail initialize...
	public void SettingInitialize()
	{
        //Debug.Log ("SettingInitialize");
		//mvc setting...
		helper = new Helper (this.gameObject);
		model = new MHero (helper);
		controller = new ControllerHero (model, helper); 

		//other reference...
		target = null;

		onlyfirst = false;

		controller.setSpeed (speed / 10.0f);
		controller.setMaxHp (MaxHP);
		controller.setHp (MaxHP);
		float range_value = 100;
		controller.setMoveOffset (Random.Range (-range_value, range_value), Random.Range (-range_value, range_value));

		helper.setMoveTrigger (false);
		helper.collision_range = gameObject.GetComponent<CircleCollider2D>().radius;

        health_bar_controller = transform.GetChild(0).GetChild(0).gameObject.GetComponent<HealthBar>();
		health_bar_body = transform.GetChild (0);
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
//			if(IsAttackCase(coll.gameObject)) 
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
		Layer my_layer = (Layer)gameObject.layer;
		switch(my_layer)
		{
		case Layer.Automart:
			if(controller.isGesture())
			{
				if(Input.GetMouseButtonDown(0))
				{
					helper.gesture_startpoint = Input.mousePosition;
					Collider2D collider = helper.RaycastHittingObject(helper.gesture_startpoint);
					if(collider == null) return;
					Hero obj = collider.gameObject.GetComponent<Hero>();
					hit_unit = obj.model.Name;
				}
				else if(Input.GetMouseButtonUp(0))
				{
					if(hit_unit != model.Name) return;
					hit_unit = "null";  //initailize...

					helper.gesture_endpoint = Input.mousePosition;
					if(helper.SelectPathNode(helper.gesture_startpoint, helper.gesture_endpoint, my_layer))
					{
						controller.setMoveTrigger(true);
					}
					if(helper.getMusterTrigger())
					{
						Debug.Log("command move");
						UnitMusterController.GetInstance().CommandMove(model.MusterID, model.Name);
					}
				}
			}
			break;
		case Layer.Trovant:
			if(controller.isGesture())
			{
                //Debug.Log("gesture trovant");
				if(helper.SelectPathNode(helper.gesture_startpoint, helper.gesture_endpoint, my_layer))
				{
					controller.setMoveTrigger(true);
				}
			}
			break;
		}
	}
	
	public void ChangingAnimationAngle()
	{
		helper.angle_calculation_rate += Time.deltaTime;
		if(helper.angle_calculation_rate >= 0.2f)
		{
			helper.angle_calculation_rate = 0;
			
//			int dir = helper.Current_Right_orLeft ();
			float a = helper.CurrentAngle ();
			controller.setAngle (a);
			
//			if(dir == -1)
//			{
//				transform.localScale = new Vector3(-1, 1, 1); //left
//				health_bar_body.localScale = new Vector3(-1, 1, 1);
//			}
//			else if(dir == 1)
//			{
//				transform.localScale = new Vector3(1, 1, 1);  //right
//				health_bar_body.localScale = new Vector3(1, 1, 1);
//			}
			
			if(a < -45 && a > -135) //down
			{
				anim.Play("Python_Moving_Downwards_");
                //Debug.Log("down");
				//		transform.localRotation = Quaternion.Euler(0,0,0);
			}
			else if(a >= -45 && a <= 45)  //right
			{
				transform.localScale = new Vector3(1, 1, 1); //left
				health_bar_body.localScale = new Vector3(1, 1, 1);
				anim.Play("Python_Moving_Normal_");
//                Debug.Log("right");
				//		transform.localRotation = Quaternion.Euler(0,0,a);
			}
			else if(a <= -135 || a >= 135) //left
			{
				transform.localScale = new Vector3(-1, 1, 1); //left
				health_bar_body.localScale = new Vector3(-1, 1, 1);
				anim.Play("Python_Moving_Normal_");
//                Debug.Log("left");
				//		transform.localRotation = Quaternion.Euler(0,0,a + 180);
			}
//			else if(a > 45 && a < 135) //up
//			{
//				anim.Play("Python_Moving_Upwards_");
//				Debug.Log("up");
//				transform.localRotation = Quaternion.Euler(0,0,0);
//			}
		}
	}
	
	public void MusterControl()
	{
		if(getLayer() != Layer.Automart) return;
		
		if(!controller.isGesture()) onlyfirst = false;
		
		if(controller.isGesture() && !onlyfirst) //cross point...
		{
			Debug.Log("muster control! : "+helper.getCurrentNodeName());
			onlyfirst = true;
			//search...muster...
			bool isCharacter = false;
			bool isCount = false;
			for(int i=0;i<UnitPoolController.GetInstance().CountUnit();i++)
			{
				UnitObject other = UnitPoolController.GetInstance().ElementUnit(i);
				
				if(model.Name == other.nameID || other.obj.layer != (int)Layer.Automart) continue;
				
				//okay unit...
				isCount = true;
				
				//okay cross like root...
				if(helper.getCurrentNodeName() == "null" && other.infor_hero.helper.getCurrentNodeName() == "null")
					continue;
				
				if(other.infor_hero.controller.isGesture() &&
				   helper.getCurrentNodeName() == other.infor_hero.helper.getCurrentNodeName())
				{
					//i none muster & u muster okay...
					if(!helper.getMusterTrigger() && other.infor_hero.helper.getMusterTrigger())
					{
						string muster_id = other.infor_hero.model.MusterID;
						
						//add good...
						bool check = UnitMusterController.GetInstance().addUnit(muster_id, this);
						//add fail...
						if(!check)
						{
							//push other muster...
							Debug.Log("other current muster full : "+muster_id);
							//get leader...
							//Hero leader = UnitMusterController.GetInstance().LeaderObj(muster_id);
							//push action...
							
						}
						else
						{
							Debug.Log("i none muster & u muster okay..."+muster_id);
						}
						isCharacter = true;

						break;
					}
					//i && u muster okay...
					else if(helper.getMusterTrigger() && other.infor_hero.helper.getMusterTrigger())
					{
						string muster_id = other.infor_hero.model.MusterID;
						string i_muster_id = model.MusterID;
						if(i_muster_id == muster_id)
						{
							Debug.Log("muster id like...return...");
							continue;
						}
						bool check = UnitMusterController.GetInstance().addUnits(muster_id, model.MusterID);
						if(!check)
						{
							Debug.Log("i && u musters full");
							//push other muster...
						}
						else
						{
							Debug.Log("i && u muster okay...");
						}
						isCharacter = true;

						break;
					}
					//i muster okay & u none muster...
					else if(helper.getMusterTrigger() && !other.infor_hero.helper.getMusterTrigger())
					{
						string muster_id = model.MusterID;
						bool check = UnitMusterController.GetInstance().addUnit(muster_id, other.infor_hero);
						if(!check)
						{
							Debug.Log("i current muster full : "+muster_id);
							//push other muster...
						}
						else
						{
							Debug.Log("i muster okay & u none muster...");
						}
						isCharacter = true;

						break;
					}
				}
			}
			//nothing character & not if...
			if(!isCharacter && isCount)
			{
				List<Hero> list_arr = new List<Hero>();
				Debug.Log("nothing character & not if...");
				if(!helper.getMusterTrigger())
				{
					for(int i=0;i<UnitPoolController.GetInstance().CountUnit();i++)
					{
						UnitObject other = UnitPoolController.GetInstance().ElementUnit(i);
						
						if(model.Name == other.nameID || other.obj.layer != (int)Layer.Automart) continue;
						if(helper.getCurrentNodeName() == "null" && other.infor_hero.helper.getCurrentNodeName() == "null")
							continue;
						if(other.infor_hero.controller.isGesture() &&
						   helper.getCurrentNodeName() == other.infor_hero.helper.getCurrentNodeName())
						{
							list_arr.Add(other.infor_hero);
						}
					}
					if(list_arr.Count > 0)
					{
						string muster_name = UnitMusterController.GetInstance().addMuster(this);
						if(muster_name != "none")
						{
							Debug.Log("muster make : "+muster_name);
						}
						for(int i=0;i<list_arr.Count;i++)
						{
							UnitMusterController.GetInstance().addUnit(muster_name, list_arr[i]);
						}
					}
				}
				list_arr.Clear();
				list_arr = null;
			}
			//command exit...
		}
	}

    public void HealthUpdate()
    {
        float ratio = model.HP / (float)model.MaxHP;
        Color color = Color.Lerp(Color.red, Color.green, ratio);

        health_bar_controller.getSlider().value = ratio;
        health_bar_controller.getSlider().foregroundWidget.color = color;
    }

	//==============================================
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
