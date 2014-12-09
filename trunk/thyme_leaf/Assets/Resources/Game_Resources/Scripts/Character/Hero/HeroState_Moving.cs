using UnityEngine;
using System.Collections;

public class HeroState_Moving : State<Hero> {

	private string animationsName = "Comma_Moving_Normal_";
	public float speed;
	private HeroState_Moving()
	{
		speed = 0.3f;
		Successor = HeroState_Hitting.Instance;
	}
	public override void Enter (Hero owner)
	{
		owner.PlayAnimation(animationsName);
		Debug.Log("Moving Enter");
	}

	public override void Execute (Hero owner)
	{
		owner.transform.Translate(Vector2.right* speed * Time.deltaTime);
	}

	public override void Exit (Hero owner)
	{
		Debug.Log("Moving Exit");
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
