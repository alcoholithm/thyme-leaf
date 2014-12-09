﻿using UnityEngine;
using System.Collections;

public class HeroState_Attacking : State<Hero> {

//	private bool check;

	private string animationsName = "Comma_Attacking_Normal_";

	private HeroState_Attacking() 
	{
		Successor = HeroState_Hitting.Instance;
//		check = false;
	}

	public override void Enter (Hero owner)
	{
		owner.PlayAnimation(animationsName);
		Debug.Log("Attack Enter ************************");
	}

	public override void Execute (Hero owner)
	{
		Debug.Log("Attack Execute ************************");

		//Animation 실행

		//StartCoroutine
		//if(!check) owner.StartCoroutine("HeroAttack");

	}

	public override void Exit (Hero owner)
	{
		Debug.Log("Attack  Exit ************************");
	}

	public override bool IsHandleable (Message msg)
	{
		Debug.Log("Attack IsHandleable ************************");
		throw new System.NotImplementedException ();
	}

	/*	
	IEnumerator HeroAttack()
	{
		check = true;
		yield return new WaitForSeconds(1);
		//HP를 깍음
		Debug.Log("상대 HP를 깍음");
		check  = false;
	}
*/

	private static HeroState_Attacking instance = new HeroState_Attacking();
	public static HeroState_Attacking Instance
	{
		get { return HeroState_Attacking.instance;}
		set { HeroState_Attacking.instance = value;}
	}


}
