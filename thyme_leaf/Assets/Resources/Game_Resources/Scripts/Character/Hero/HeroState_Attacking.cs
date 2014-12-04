using UnityEngine;
using System.Collections;

public class HeroState_Attacking : State<Hero> {

	private string animationsName = "Comma_Attacking_Normal_";

	private HeroState_Attacking() {
		Successor = HeroState_Hitting.Instance;

		//check = false;
	}

	public override void Enter (Hero owner)
	{
		owner.PlayAnimation(animationsName);
		Debug.Log("Attack Enter ************************");
	}

	public override void Execute (Hero owner)
	{
		Debug.Log("Attack Execute ************************");
		Debug.Log (owner.transform.name);

		//Animation 실행

		//StartCoroutine
	//	if(!check) owner.StartCoroutine("HeroAttack");

		//throw new System.NotImplementedException ();
	}

	public override void Exit (Hero owner)
	{

		Debug.Log("Attack  Exit ************************");
		//throw new System.NotImplementedException ();
	}

	public override bool IsHandleable (Message msg)
	{
		Debug.Log(msg);
		switch(msg.what)
		{
		case MessageTypes.MSG_MOVE_HERO:
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
