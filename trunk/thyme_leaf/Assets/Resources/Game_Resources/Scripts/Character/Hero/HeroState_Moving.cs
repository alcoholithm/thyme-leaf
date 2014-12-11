using UnityEngine;
using System.Collections;

public class HeroState_Moving : State<Hero> {

	private HeroState_Moving()
	{
		Successor = HeroState_Hitting.Instance;
	}
	public override void Enter (Hero owner)
	{
		//if(own
		owner.PlayAnimation("Comma_Moving_Normal_");
	}

	public override void Execute (Hero owner)
	{
		float dx = (owner.helper.nodeInfor.getPos (PosParamOption.CURRENT).x + owner.model.getMoveOffset ().x) - owner.helper.getPos ().x;
		float dy = (owner.helper.nodeInfor.getPos (PosParamOption.CURRENT).y + owner.model.getMoveOffset ().y) - owner.helper.getPos ().y;

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
						Debug.Log("next null");
						//turnoff root true
						if(owner.helper.nodeInfor.turnoffBridge == null)
						{
							Debug.Log("turnoff null");
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
						Debug.Log("prev null");
						//turnoff root true
						if(owner.helper.nodeInfor.turnoffBridge == null)
						{
							Debug.Log("turnoff null");
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
		else
		{
			//don't move

		}
	}

	public override void Exit (Hero owner)
	{
		//exit stage...
	}

	public override bool IsHandleable (Message msg)
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
