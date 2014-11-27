using UnityEngine;
using System.Collections;

public class Hero : GameEntity<Hero> {
	public new const string TAG = "[Tower]";
	
	private UISpriteAnimation anim;
	
	void Awake()
	{
		Initialize();
	}
	
	void Update()
	{
		stateMachine.Update();
	}
	
	/*
     * followings are member functions
     */
	public void PlayAnimation(string name)
	{
		anim.namePrefix = name;
		anim.Play();
	}
	
	public void Initialize()
	{
		this.stateMachine = new StateMachine<Hero>(this);
		this.stateMachine.CurrentState = HeroState_None.Instance;
		this.stateMachine.GlobalState = HeroState_Hitting.Instance;
		
		this.anim = GetComponent<UISpriteAnimation>();
		this.anim.Pause();
	}
}
