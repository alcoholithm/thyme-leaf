﻿using UnityEngine;
using System.Collections;

public class HeroState_Moving : State<Hero> {

	private HeroState_Moving()
	{
		Successor = HeroState_Hitting.Instance;
	}
	public override void Enter (Hero owner)
	{
		if(!owner.helper.collision_object.enabled)
			owner.helper.collision_object.enabled = true;

		owner.controller.setStateName (Naming.MOVING);
		owner.AnimationName = Naming.Instance.BuildAnimationName(owner.gameObject, owner.model.StateName);
	}

	public override void Execute (Hero owner)
	{
        if (Network.peerType != NetworkPeerType.Disconnected && !owner.gameObject.networkView.isMine) 
            return;

		Vector3 d = (owner.helper.nodeInfor.getPos (PosParamOption.CURRENT) + owner.model.NodeOffsetStruct.offset) - owner.helper.getPos ();

		//checking...
		Vector3 me = owner.helper.getPos(); // this character position...
		if(owner.target.obj == null)
		{
			for(int i=0;i<UnitPoolController.GetInstance().CountUnit();i++)
			{
				UnitObject other = UnitPoolController.GetInstance().ElementUnit(i);

				if(owner.model.ID == other.nameID) continue;

                if (other.obj == null)
                    continue;

				bool check = false;
				switch(owner.getLayer())
				{ 
				case Layer.Automart:  //automart
					if(other.obj.layer == (int) Layer.Automart) check = true;
					break;
				case Layer.Trovant:  //trovant
					if(other.obj.layer == (int) Layer.Trovant) check = true;
					break;
				default:
					check = true;
					break;
				}

				if(!check)
				{
					float range = 150;//owner.helper.collision_range + 200;
					float dist = Vector3.SqrMagnitude(other.obj.transform.localPosition - me);
					if(dist < range * range)
					{
						owner.helper.setMoveTrigger(false);
						owner.target = other;
						break;
					}
				}
			}
		}
		else if(owner.target.obj != null)
		{
			if(GetHp(ref owner.target) <= 0)
			{
				owner.target.DataInit();
				owner.controller.setMoveTrigger(true);
			}
			else if(!owner.helper.getMoveTrigger())
			{
				Vector3 dd = owner.target.obj.transform.localPosition - me;
				float r = Mathf.Atan2(dd.y, dd.x);
				float speed_v = owner.model.MovingSpeed * Define.FrameControl();
				owner.controller.addPos(speed_v * Mathf.Cos(r), speed_v * Mathf.Sin(r));
			}
		}

		//moving...
		if(owner.helper.getMoveTrigger())
		{
			if(Vector3.SqrMagnitude(d) < 50) //checking range    
			{
				if(owner.helper.GetMoveMode() == MoveModeState.FORWARD)
				{
					if(owner.helper.nodeInfor.Next != null)
					{
						owner.helper.nodeOld = owner.helper.nodeStock;
						owner.helper.nodeStock = owner.helper.nodeInfor.Next;
					}
					else
					{
						//Debug.Log("next null");
						//turnoff root true
						if(owner.helper.nodeInfor.turnoffBridge == null)
						{
							//Debug.Log("turnoff null");
							if(owner.helper.nodeInfor.automatPoint || owner.helper.nodeInfor.trovantPoint) owner.controller.MoveReverse();
							else owner.helper.nodeStock = owner.helper.nodeInfor.turnoffList[0].GetComponent<scriptPathNode>().turnoffBridge;
						}
						else
						{
							owner.helper.nodeOld = owner.helper.nodeStock;
							owner.helper.nodeStock = owner.helper.nodeInfor.turnoffBridge;
						}
					}
				}
				else if(owner.helper.GetMoveMode() == MoveModeState.BACKWARD)
				{
					if(owner.helper.nodeInfor.Prev != null) 
					{
						owner.helper.nodeOld = owner.helper.nodeStock;
						owner.helper.nodeStock = owner.helper.nodeInfor.Prev;
					}
					else
					{
						//Debug.Log("prev null");
						//turnoff root true
						if(owner.helper.nodeInfor.turnoffBridge == null)
						{
							//Debug.Log("turnoff null");
							if(owner.helper.nodeInfor.automatPoint || owner.helper.nodeInfor.trovantPoint) owner.controller.MoveReverse();
							else owner.helper.nodeStock = owner.helper.nodeInfor.turnoffList[0].GetComponent<scriptPathNode>().turnoffBridge;
						}
						else
						{
							owner.helper.nodeOld = owner.helper.nodeStock;
							owner.helper.nodeStock = owner.helper.nodeInfor.turnoffBridge;
						}
					}
				}
				
				if(owner.helper.nodeInfor.TurnoffRoot && !owner.helper.selectTurnoffRoot)
				{
					owner.helper.selectTurnoffRoot = true;
					owner.controller.setMoveTrigger(false);
					return;
				}
				else if(!owner.helper.nodeInfor.TurnoffRoot)
				{
					owner.helper.selectTurnoffRoot = false;
				}
				
				owner.helper.nodeInfor = owner.helper.nodeStock.GetComponent<scriptPathNode>();
			}
			//move module
			float sp = owner.model.MovingSpeed * Define.FrameControl();
			float rt = Mathf.Atan2(d.y, d.x);
			if(owner.helper.isGesture())
			{
				sp = 1;
			}
			owner.controller.addPos(Mathf.Cos(rt) * sp, Mathf.Sin(rt) * sp);
		}
	}

	public override void Exit (Hero owner)
	{
		//exit stage...
		owner.target.DataInit();
		owner.helper.setMoveTrigger(true);
	}

	public override bool HandleMessage (Message msg)
	{
		return false;
	}

	private int GetHp(ref UnitObject obj)
	{
		switch(obj.type)
		{
		case UnitType.AUTOMAT_CHARACTER:
		case UnitType.TROVANT_CHARACTER:
			return obj.infor_hero == null ? -1 : obj.infor_hero.model.HP;
		case UnitType.AUTOMAT_WCHAT:
			return obj.infor_automat_center.Model == null ? -1 : obj.infor_automat_center.Model.HP;
		case UnitType.TROVANT_THOUSE:
			return obj.infor_trovant_center.Model == null ? -1 : obj.infor_trovant_center.Model.HP;
		}
		return -1;
	}

	public new const string TAG = "[HeroState_Moving]";
	private static HeroState_Moving _instance = new HeroState_Moving();
	public static HeroState_Moving Instance {
		get {
			return _instance;
		}
	}
}
