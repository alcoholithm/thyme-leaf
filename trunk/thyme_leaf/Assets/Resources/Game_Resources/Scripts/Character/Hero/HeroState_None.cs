using UnityEngine;
using System.Collections;

public class HeroState_None : State<Hero> {

	private HeroState_None()
	{
		Successor = HeroState_Hitting.Instance;
	}
	public override void Enter (Hero owner)
	{
	}

	public override void Execute (Hero owner)
	{
	}

	public override void Exit (Hero owner)
	{
	}

	public override bool IsHandleable (Message msg)
	{
		return false;
	}

	public new const string TAG = "[HeroState_None]";
	private static HeroState_None _instance = new HeroState_None();
	public static HeroState_None Instance {
		get {
			return _instance;
		}
	}
}
