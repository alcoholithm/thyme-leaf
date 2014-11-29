using UnityEngine;
using System.Collections;

public class Hero : GameEntity<Hero> {
	public new const string TAG = "[Hero]";
	public int hPoint = 100;

	private UISpriteAnimation anim;
	
	void Awake()
	{
		Initialize();
	}
	
	void Update()
	{
		stateMachine.Update();
	}
	

	void OnCollisionEnter2D(Collision2D coll) 
	{
		//나는 때리는 상태로
		stateMachine.ChangeState(HeroState_Attacking.Instance);

		//상대방은 쳐맞는 상태로
		coll.gameObject.GetComponent<Hero>().stateMachine.ChangeState(HeroState_Hitting.Instance);
	}

	void OnCollisionStay2D(Collision2D coll)
	{

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
