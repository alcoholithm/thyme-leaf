﻿using UnityEngine;
using System.Collections;

public class HeroState_Dying : State<Hero> {
	
    //private string animationName = "Comma_Dying_";

	private HeroState_Dying() {
		Successor = null;
	}
	
	public override void Enter (Hero owner)
	{
		owner.controller.setStateName(Naming.DYING);
		owner.AnimationName = Naming.Instance.BuildAnimationName(owner.gameObject, owner.model.StateName);
		owner.Anim.PlayOneShot(owner.AnimationName+"_");
	}
	
	public override void Execute (Hero owner)
	{
		if(!owner.GetAnim().isPlaying)
			owner.StateMachine.ChangeState(HeroState_None.Instance);
	}
	
	public override void Exit (Hero owner)
	{
		if(owner == null) return;

		AudioManager.Instance.PlayClipWithState(owner.gameObject, StateType.DYING);  

		//current unit muster release...
		if(owner.helper.getMusterTrigger())
			UnitMusterController.GetInstance().removeUnit(owner.model.MusterID, owner.model.ID);
		//collision module exit...
		owner.Die ();
		//remove character in unit pool...
		UnitPoolController.GetInstance ().RemoveUnit (owner.MyUnit);

        Spawner.Instance.Free(owner.gameObject);

        UserAdministrator.Instance.CurrentUser.Gold += 100;

		//throw new System.NotImplementedException ();
	}

	public override bool HandleMessage (Message msg)
	{
		throw new System.NotImplementedException ();
	}
	
	private static HeroState_Dying instance = new HeroState_Dying();
	public static HeroState_Dying Instance {
		get { return HeroState_Dying.instance;}
		set { HeroState_Dying.instance = value;}
	}
	
	
}
