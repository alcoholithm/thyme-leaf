using UnityEngine;
using System.Collections;

public class HeroState_Idling : State<Hero> {

	private HeroState_Idling() {
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

	private static HeroState_Idling instance = new HeroState_Idling();
	public static HeroState_Idling Instance {
		get { return HeroState_Idling.instance;}
		set { HeroState_Idling.instance = value;}
	}

}
