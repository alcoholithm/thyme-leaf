using UnityEngine;
using System.Collections;

public class HeroState_Moving : State<Hero> {

	private HeroState_Moving()
	{
	//	Successor = HeroState_Hitting.Instance;
	}
	public override void Enter (Hero owner)
	{
		owner.PlayAnimation("Hero_Moving_");
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

	public new const string TAG = "[HeroState_Moving]";
	private static HeroState_Moving _instance = new HeroState_Moving();
	public static HeroState_Moving Instance {
		get {
			return _instance;
		}
	}
}
