using UnityEngine;
using System.Collections;

public class HeroState_Dying : State<Hero> {

	private string animationName = "Comma_Dying_";

	private HeroState_Dying() {
		Successor = null;
	}

	public override void Enter (Hero owner)
	{
		Debug.Log("Died Enter");
		owner.PlayAnimationOneTime(animationName);
	}

	public override void Execute (Hero owner)
	{
		if( !owner.GetAnim().isPlaying)
		{
			owner.StateMachine.ChangeState(HeroState_None.Instance);
		}
	}

	public override void Exit (Hero owner)
	{
		Message msg = owner.target.ObtainMessage(MessageTypes.MSG_MOVE_HERO, new HeroMovingCommand(owner.target));
		owner.target.DispatchMessage(msg);

		HeroSpawner.Instance.Free(owner.gameObject);
		//throw new System.NotImplementedException ();
	}

	public override bool IsHandleable (Message msg)
	{
		throw new System.NotImplementedException ();
	}

	private static HeroState_Dying instance = new HeroState_Dying();
	public static HeroState_Dying Instance {
		get { return HeroState_Dying.instance;}
		set { HeroState_Dying.instance = value;}
	}


}
