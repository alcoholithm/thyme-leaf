using UnityEngine;
using System.Collections;

public class HeroState_Dying : State<Hero> {

	private string animationsName = "Comma_Dying_";

	private HeroState_Dying() {
		Successor = null;
	}

	public override void Enter (Hero owner)
	{
		Debug.Log ("Died Enter");
		owner.PlayAnimationOnTime(animationsName);
	}

	public override void Execute (Hero owner)
	{
		if (!owner.Anim().isPlaying)
		{
			owner.StateMachine.ChangeState(HeroState_None.Instance);
		}
	}

	public override void Exit (Hero owner)
	{
		//HeroSpawner.Instance.Free (owner);
	}

	public override bool IsHandleable (Message msg)
	{
		throw new System.NotImplementedException ();
	}

	private static HeroState_Dying instance = new HeroState_Dying();
	public static HeroState_Dying Instance {
		get { return HeroState_Dying.instance; }
		set { HeroState_Dying.instance = value;}
	}


}
