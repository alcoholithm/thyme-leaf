using UnityEngine;
using System.Collections;

public class Hero_Warrior : Hero 
{
	void Awake()
	{
		Initialize();
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
					switch((Layer)coll.gameObject.layer)
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
		}
	}

	public void OnEnable()
	{
		SettingInitialize ();
		DetailSettingInitialize ();
	}
	
	public void DetailSettingInitialize()
	{
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

		AttackType = MessageTypes.MSG_NORMAL_DAMAGE;
	}

	protected override bool IsAttackCase (GameObject gObj)
	{
		return base.IsAttackCase (gObj);
	}
}
