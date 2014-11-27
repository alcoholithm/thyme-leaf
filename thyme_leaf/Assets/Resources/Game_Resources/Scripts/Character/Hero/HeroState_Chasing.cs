using UnityEngine;
using System.Collections;

public class HeroState_Chasing : State<Hero> {

	private HeroState_Chasing() {
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

	private static HeroState_Chasing instance = new HeroState_Chasing();
	public static HeroState_Chasing Instance {
		get { return HeroState_Chasing.instance;}
		set { HeroState_Chasing.instance = value;}
	}

}
