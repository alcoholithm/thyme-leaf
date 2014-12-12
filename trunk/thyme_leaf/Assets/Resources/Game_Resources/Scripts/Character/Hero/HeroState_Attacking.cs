﻿using UnityEngine;
using System.Collections;

public class HeroState_Attacking : State<Hero> {
	
	private string animationsName = "Comma_Attacking_Normal_";
	
	private HeroState_Attacking() {
		Successor = HeroState_Hitting.Instance;
	}
	
	public override void Enter (Hero owner)
	{
		owner.PlayAnimation(animationsName);
		Debug.Log("Attack Enter ************************");

		owner.helper.attack_delay = 0;  //attack delay setting...
	}
	
	public override void Execute (Hero owner)
	{
		owner.helper.attack_delay += Time.deltaTime;
		if(owner.helper.attack_delay >= 1)
		{
			Message msg = owner.ObtainMessage(MessageTypes.MSG_DAMAGE,new HeroDamageCommand(owner.target));
			owner.DispatchMessage(msg);
			owner.helper.attack_delay = 0;
		}

		//if.. target is null -> mode change moving state...
		owner.TargetingInitialize ();

		/*
		if(owner.target == null)
		{
	//		Message msg = owner.ObtainMessage<Hero>(MessageTypes.MSG_MISSING, hero => hero.StateMachine.ChangeState(HeroState_Moving.Instance));
	//		owner.DispatchMessage(msg);
		}
		else  //test code...
		{
			//other name...
			owner.target_name = owner.target.model.getID();
		}
		*/
	}
	
	public override void Exit (Hero owner)
	{
		//Debug.Log("Attack  Exit ************************");
		//throw new System.NotImplementedException ();
	}
	
	public override bool IsHandleable (Message msg)
	{
		Debug.Log(msg);
		switch(msg.what)
		{
		case MessageTypes.MSG_MOVE_HERO:
			return true;
		case MessageTypes.MSG_DAMAGE:
			return true;
		case MessageTypes.MSG_MISSING:
			return true;
		}
		return false;
	}
	
	private static HeroState_Attacking instance = new HeroState_Attacking();
	public static HeroState_Attacking Instance
	{
		get { return HeroState_Attacking.instance;}
		set { HeroState_Attacking.instance = value;}
	}
	
	
}
