using UnityEngine;
using System.Collections;

public class HeroState_Attacking : State<Hero> {

	private HeroState_Attacking() {
		Successor = HeroState_Hitting.Instance;
	}

	public override void Enter (Hero owner)
	{
		throw new System.NotImplementedException ();
	}

	public override void Execute (Hero owner)
	{
		throw new System.NotImplementedException ();
	}

	public override void Exit (Hero owner)
	{
		throw new System.NotImplementedException ();
	}

	public override bool IsHandleable (Message msg)
	{
		throw new System.NotImplementedException ();
	}

	private static HeroState_Attacking instance = new HeroState_Attacking();
	public static HeroState_Attacking Instance
	{
		get { return HeroState_Attacking.instance;}
		set { HeroState_Attacking.instance = value;}
	}


}
