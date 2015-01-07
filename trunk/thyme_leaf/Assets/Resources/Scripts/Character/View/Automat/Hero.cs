using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hero : GameEntity, IStateMachineControllable<Hero>, IObserver
{
	public new const string TAG = "[Hero]";

    private StateMachine<Hero> stateMachine;

    public StateMachine<Hero> StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }

	//=====================
	//unit identity value
	//=====================

	private NGUISpriteAnimation anim;	

    // set children
    [SerializeField]
    private View health_bar_controller;
    [SerializeField]
    private View _FxWhap;
    [SerializeField]
    private View _FxBurn;

    public View FxBurn
    {
        get { return _FxBurn; }
        set { _FxBurn = value; }
    }
    [SerializeField]
    private View _FxPoisoning;

    public View FxPoisoning
    {
        get { return _FxPoisoning; }
        set { _FxPoisoning = value; }
    }

	private string animation_name;
	private UnitObject my_unit;
	public UnitObject target;

	public string my_name;  //test code...
	public string state_name; //test code...
	public string muster_name; //test code...
	public string p_name;  //test code...

	//extra...
	private bool onlyfirst;
	private int hit_unit_id;

	[HideInInspector]
	public MHero model;
	[HideInInspector]
	public ControllerHero controller;
	[HideInInspector]
	public Helper helper;

	[HideInInspector]
	private UISprite ui_sprite;
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
		//other reference...
		target.DataInit();

		onlyfirst = false;

		controller.setSpeed (model.MovingSpeed);
		controller.setMaxHp (model.MaxHP);
		controller.setHp (model.MaxHP);
		controller.setMoveTrigger (false);
        controller.setAttackDamage(model.AttackDamage);
		controller.setAttackDelay (model.AttackDelay);

		helper.collision_object = gameObject.GetComponent<CircleCollider2D> ();
		helper.collision_3d = gameObject.transform.FindChild ("CollisionBag").gameObject.GetComponent<SphereCollider> ();
		helper.collision_range_normal = helper.collision_3d.radius;
		helper.collision_range_muster = 180;

        health_bar_controller = transform.GetChild(0).gameObject.GetComponent<HealthBar>();
		ui_sprite = gameObject.GetComponent<UISprite> ();
		ui_sprite.depth = 1;

        this.PrepareUI();
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		//coll return checking...
		if(coll == null || helper.collision_object.radius == helper.collision_range_muster) return;
	
		if(stateMachine.CurrentState == HeroState_Moving.Instance)
		{
			if(IsAttackCase(coll.gameObject))  //state compare okay...
		    {
				if(!coll.CompareTag(Tag.TagCommandCenter))
				{
					helper.attack_target = coll.gameObject.GetComponent<Hero>().MyUnit;
				}
				else 
				{
					Debug.Log("collision center");
					Layer other_center_layer = (Layer)coll.gameObject.layer;
					switch(other_center_layer)
					{
					case Layer.Automart:
						helper.attack_target = coll.gameObject.transform.parent.GetComponent<WChat>().MyUnit;
						break;
					case Layer.Trovant:
						Debug.Log("trovant center");
						helper.attack_target = coll.gameObject.transform.parent.GetComponent<THouse>().MyUnit;
						Debug.Log(helper.attack_target.obj.name);
						break;
					}
				}
//				my state is attaking...
				stateMachine.ChangeState(HeroState_Attacking.Instance);
			}
			else
			{
//				if(coll.CompareTag(Tag.TagProjectile) || coll.CompareTag(Tag.TagTower) || coll.CompareTag(Tag.TagTowerSpot)) return;
//				
//				Hero other_hero = coll.gameObject.GetComponent<Hero>();
//				if(!other_hero.helper.isGesture() || other_hero.model.MusterID == model.MusterID) return;
//				if(UnitMusterController.GetInstance().isUnitCountCheck(other_hero.model.MusterID, model.MusterID)) return;
//				//push action...
//				if(other_hero.helper.SelectPathNode(helper.gesture_startpoint, helper.gesture_endpoint, other_hero.getLayer(), FindingNodeDefaultOption.RANDOM_NODE))
//				{
//					other_hero.controller.setMoveTrigger(true);
//					if(other_hero.helper.getMusterTrigger())
//						UnitMusterController.GetInstance().CommandMove(other_hero.model.MusterID, other_hero.model.Name);
//				}
//				Debug.Log("push muster or a unit");
			}
		}
		else if(stateMachine.CurrentState == HeroState_Attacking.Instance)
		{

		}
	}

	//attack mode checking...
	private bool IsAttackCase(GameObject gObj)
	{
		Layer type = getLayer();
		switch (type) {
		case Layer.Automart:
			if((Layer.Trovant == (Layer) gObj.layer) && 
			   !gObj.CompareTag(Tag.TagProjectile) && !gObj.CompareTag(Tag.TagTower) && !gObj.CompareTag(Tag.TagTowerSpot))
			{
				return true;
			}
			break;
		case Layer.Trovant:
			if(Layer.Automart == (Layer) gObj.layer &&
			   !gObj.CompareTag(Tag.TagProjectile) && !gObj.CompareTag(Tag.TagTower) && !gObj.CompareTag(Tag.TagTowerSpot))
			{
				return true;
			}
			break;
		}
		return false;
	}


    /// <summary>
    /// Networking methods made by Deokil
    /// </summary>
    private void SubGesture(Vector3 startPt, Vector3 endPt, Layer my_layer)
    {
        int mLayer = (int)my_layer;
        if (Network.peerType == NetworkPeerType.Disconnected){
            Debug.Log("DISCONNECTED");
            NetworkGesture(startPt, endPt, mLayer);
        }
        else
        {
            Debug.Log("CONNECTED");
            networkView.RPC("NetworkGesture", RPCMode.All, startPt, endPt, mLayer);
        }
    }

    [RPC]
    void NetworkGesture(Vector3 startPt, Vector3 endPt, int my_layer)
    {
        if (helper.SelectPathNode(startPt, endPt, (Layer)my_layer))
        {
            controller.setMoveTrigger(true);
        }

        if ((Layer)my_layer == Layer.Trovant) return;

        if (helper.getMusterTrigger())
        {
            Debug.Log("command move");
            UnitMusterController.GetInstance().CommandMove(model.MusterID, model.ID);
        }
    }

	//gesturing function...
	private void Gesturing()
	{
        // when connected to network and this object is mine
        if (Network.peerType != NetworkPeerType.Disconnected && !networkView.isMine) return;

		Layer my_layer = (Layer)gameObject.layer;
		switch(my_layer)
		{
		case Layer.Automart:
			if(controller.isGesture())
			{
				if(Input.GetMouseButtonDown(0))
				{
					helper.gesture_startpoint = Input.mousePosition;
					if(helper.getMusterTrigger())
					{
						Debug.Log("muster range edit");
						UnitMusterController.GetInstance().CommandSearchRangeValue(model.MusterID, model.ID, helper.collision_range_muster);
					}

					GameObject coll_obj = helper.RaycastHittingObject3D(helper.gesture_startpoint);
					if(coll_obj == null) return;

					//					Collider2D collider = helper.RaycastHittingObject(helper.gesture_startpoint);
					//					if(collider == null) return;
					//					if(collider.CompareTag(Tag.TagTower) || collider.CompareTag(Tag.TagTowerSpot)) return;

					Hero obj = coll_obj.GetComponent<Hero>();
					if(obj == null) return;  //if...null never case...

					hit_unit_id = obj.model.ID;
				}
				else if(Input.GetMouseButtonUp(0))
				{
					if(helper.getMusterTrigger())
						UnitMusterController.GetInstance().CommandSearchRangeValue(model.MusterID, model.ID, helper.collision_range_normal);

					if(hit_unit_id != model.ID) return;
					hit_unit_id = -1;  //initailize...
					
					helper.gesture_endpoint = Input.mousePosition;

                    // Don't touch this method that is for network
                    SubGesture(helper.gesture_startpoint, helper.gesture_endpoint, my_layer);
				}
			}
			break;
		case Layer.Trovant:
			if(controller.isGesture())
			{
                SubGesture(helper.gesture_startpoint, helper.gesture_endpoint, my_layer);
				//automatic direction finding...
                //if(helper.SelectPathNode(helper.gesture_startpoint, helper.gesture_endpoint, my_layer))
                //{
                //    controller.setMoveTrigger(true);
                //}
			}
			break;
		}
	}
	
	public void ChangingAnimationAngle()
	{
		helper.angle_calculation_rate += Time.deltaTime;
		if(helper.angle_calculation_rate >= 0.1f)
		{
			if(stateMachine.CurrentState == HeroState_Dying.Instance) return;

			helper.angle_calculation_rate = 0;
			
			int dir = helper.Current_Right_orLeft ();
			dir = helper.isGesture() == true ? 0 : dir;

			float a = helper.CurrentAngle ();
			controller.setAngle (a);

			//string anim_name = Naming.Instance.BuildAnimationName(gameObject, model.StateName);
			if(dir == -1)
			{
				ui_sprite.flip = UIBasicSprite.Flip.Horizontally;
			}
			else if(dir == 1)
			{
				ui_sprite.flip = UIBasicSprite.Flip.Nothing;
			}
			
			if(a < -45 && a > -135) //down
			{
				anim.Play(animation_name+"_Downwards_");
			}
			else if(a >= -45 && a <= 45)  //right
			{
				anim.Play(animation_name+"_Normal_");
			}
			else if(a <= -135 || a >= 135) //left
			{
				anim.Play(animation_name+"_Normal_");
			}
//			else if(a > 45 && a < 135) //up
//			{
//				anim.Play("Python_Moving_Upwards_");
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

                if (other.obj == null)
                    continue;
				
				if(model.ID == other.nameID || other.obj.layer != (int)Layer.Automart ||
				   other.infor_hero == null) continue;
				
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
							//Debug.Log("other current muster full : "+muster_id);
							//push action...
							if(other.infor_hero.helper.SelectPathNode(helper.gesture_startpoint, helper.gesture_endpoint, other.infor_hero.getLayer(), FindingNodeDefaultOption.RANDOM_NODE))
							{
								other.infor_hero.controller.setMoveTrigger(true);
								UnitMusterController.GetInstance().CommandMove(other.infor_hero.model.MusterID, other.nameID);
							}
						}
						else
						{
							//Debug.Log("i none muster & u muster okay..."+muster_id);
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
							//Debug.Log("muster id like...return...");
							continue;
						}
						bool check = UnitMusterController.GetInstance().addUnits(muster_id, model.MusterID);
						if(!check)
						{
							//Debug.Log("i && u musters full");
							//push action...
							if(other.infor_hero.helper.SelectPathNode(helper.gesture_startpoint, helper.gesture_endpoint, other.infor_hero.getLayer(), FindingNodeDefaultOption.RANDOM_NODE))
							{
								other.infor_hero.controller.setMoveTrigger(true);
								UnitMusterController.GetInstance().CommandMove(other.infor_hero.model.MusterID, other.nameID);
							}
						}
						else
						{
							//Debug.Log("i && u muster okay...");
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
							//Debug.Log("i current muster full : "+muster_id);
							//push action...
							if(helper.SelectPathNode(helper.gesture_startpoint, helper.gesture_endpoint, getLayer(), FindingNodeDefaultOption.RANDOM_NODE))
							{
								controller.setMoveTrigger(true);
								UnitMusterController.GetInstance().CommandMove(model.MusterID, model.ID);
							}
						}
						else
						{
							//Debug.Log("i muster okay & u none muster...");
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
				//Debug.Log("nothing character & not if...");
				if(!helper.getMusterTrigger())
				{
					for(int i=0;i<UnitPoolController.GetInstance().CountUnit();i++)
					{
						UnitObject other = UnitPoolController.GetInstance().ElementUnit(i);
						
						if(model.ID == other.nameID || other.obj.layer != (int)Layer.Automart ||
						   other.infor_hero == null) continue;
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
							//Debug.Log("muster make : "+muster_name);
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

	public void TakeDamage(int damage_range)
	{
        model.TakeDamageWithDEF(damage_range);

		if(model.HP <= 0)
		{
			Debug.Log(model.Name + " die");
			StateMachine.ChangeState(HeroState_Dying.Instance);
		}
	}

	//==============================================
	public void setLayer(Layer v) { gameObject.layer = (int) v; }
	public Layer getLayer() { return (Layer) gameObject.layer; }

	public void CollisionSetting(bool trigger) { gameObject.GetComponent<CircleCollider2D>().enabled = trigger; }

	public void Die()
	{
		helper.setPos (1000, 1000, 0);
		CollisionSetting (false); 
		health_bar_controller.gameObject.SetActive(false);
		UnitPoolController.GetInstance ().RemoveUnit (MyUnit);
	}
	
	public UISpriteAnimation GetAnim() { return anim; }

	//==============================================
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
        //mvc setting...
        helper = new Helper(this.gameObject);
        model = gameObject.GetComponent<MHero>();
        model.Helper = helper;
        controller = new ControllerHero(model, helper);

        // observer
        model.RegisterObserver(this, ObserverTypes.Health);

        //hp bar setting...
        (this.health_bar_controller as HealthBar).Model = this.model;
        this.Add(health_bar_controller);
        this.Add(_FxWhap);

        // state machine
		this.stateMachine = new StateMachine<Hero>(this);
		this.stateMachine.CurrentState = HeroState_None.Instance;
		this.stateMachine.GlobalState = HeroState_Hitting.Instance;

		this.anim = GetComponent<NGUISpriteAnimation>();
		this.anim.Pause();
		transform.localPosition = new Vector3(1000,1000);
	}

    /*
    * followings are implemented methods of "IStateMachineControllable"
    */
    public void ChangeState(State<Hero> newState)
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
     * Followings are attributes.
     */ 
    public override IHandler Successor
    {
        get { return stateMachine; }
    }

	public UnitObject MyUnit
	{
		get { return my_unit; }
		set { my_unit = value; }
	}

	public string AnimationName
	{
		get { return animation_name; }
		set { animation_name = value; }
	}
}
