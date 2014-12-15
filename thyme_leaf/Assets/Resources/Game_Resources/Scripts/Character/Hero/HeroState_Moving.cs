using UnityEngine;
using System.Collections;

public class HeroState_Moving : State<Hero> {

	private HeroState_Moving()
	{
		Successor = HeroState_Hitting.Instance;
	}
	public override void Enter (Hero owner)
	{
		owner.PlayAnimation("Comma_Moving_Normal_");
	}

	public override void Execute (Hero owner)
	{
		float dx = (owner.helper.nodeInfor.getPos (PosParamOption.CURRENT).x + owner.model.getMoveOffset ().x) - owner.helper.getPos ().x;
		float dy = (owner.helper.nodeInfor.getPos (PosParamOption.CURRENT).y + owner.model.getMoveOffset ().y) - owner.helper.getPos ().y;

		/*
		//checking...
		Vector3 me = owner.helper.getPos(); // this character position...
		if(owner.helper.lockOn_target == null)
		{
			for(int i=0;i<UnitPoolController.GetInstance().CountUnit();i++)
			{
				GameObject other = UnitPoolController.GetInstance().ElementUnit(i);

				bool check = false;
				switch(owner.gameObject.layer)
				{
				case 9:  //automart
					if(other.layer == Layer.Automart()) check = true;
					break;
				case 10:  //trovant
					if(other.layer == Layer.Trovant()) check = true;
					break;
				default:
					check = true;
					break;
				}

				if(!check)
				{
					Vector3 other_pos = other.transform.localPosition;

					float cdx = other_pos.x - me.x;
					float cdy = other_pos.y - me.y;
					if(cdx * cdx + cdy * cdy < 100 * 100)
					{
						owner.helper.setMoveTrigger(false);
						owner.helper.lockOn_target = other;
						break;
					}
				}
			}
		}
		else if(owner.helper.lockOn_target != null)
		{
			if(!owner.helper.getMoveTrigger())
			{
				Vector3 tempPos = owner.helper.lockOn_target.transform.localPosition;
				float cdx = tempPos.x - me.x;
				float cdy = tempPos.y - me.y;
				float angle_rt = Mathf.Atan2(cdy, cdx);
				float speed_v = owner.model.getSpeed();
				owner.controller.addPos(speed_v * Mathf.Cos(angle_rt), speed_v * Mathf.Sin(angle_rt));
			}
		}
		*/

		//moving...
		if(owner.helper.getMoveTrigger())
		{
			if(dx * dx + dy * dy < 10) //checking range    
			{
				if(owner.helper.GetMoveMode() == MoveModeState.FORWARD)
				{
					if(owner.helper.nodeInfor.Next != null)
					{
						owner.helper.nodeStock = owner.helper.nodeInfor.Next;
					}
					else
					{
						//Debug.Log("next null");
						//turnoff root true
						if(owner.helper.nodeInfor.turnoffBridge == null)
						{
							//Debug.Log("turnoff null");
							if(owner.helper.nodeInfor.startPoint || owner.helper.nodeInfor.endPoint) owner.controller.MoveReverse();
							else owner.helper.nodeStock = owner.helper.nodeInfor.turnoffList[0].GetComponent<scriptPathNode>().turnoffBridge;
						}
						else
						{
							owner.helper.nodeStock = owner.helper.nodeInfor.turnoffBridge;
						}
					}
				}
				else if(owner.helper.GetMoveMode() == MoveModeState.BACKWARD)
				{
					if(owner.helper.nodeInfor.Prev != null) 
					{
						owner.helper.nodeStock = owner.helper.nodeInfor.Prev;
					}
					else
					{
						//Debug.Log("prev null");
						//turnoff root true
						if(owner.helper.nodeInfor.turnoffBridge == null)
						{
							//Debug.Log("turnoff null");
							if(owner.helper.nodeInfor.startPoint || owner.helper.nodeInfor.endPoint) owner.controller.MoveReverse();
							else owner.helper.nodeStock = owner.helper.nodeInfor.turnoffList[0].GetComponent<scriptPathNode>().turnoffBridge;
						}
						else
						{
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
			float sp = owner.model.getSpeed() * Define.FrameControl();
			float rt = Mathf.Atan2(dy, dx);

			owner.controller.addPos(Mathf.Cos(rt) * sp, Mathf.Sin(rt) * sp);
		}
	}

	public override void Exit (Hero owner)
	{
		//exit stage...
		owner.helper.lockOn_target = null;
	}

	public override bool HandleMessage (Message msg)
	{
		return false;
	}

	public new const string TAG = "[HeroState_Moving]";
	private static HeroState_Moving _instance = new HeroState_Moving();
	public static HeroState_Moving Instance {
		get {
			return _instance;
		}
	}
}
