using UnityEngine;
using System.Collections;

public class HeroDamageCommand : ICommand {
	private Hero hero;
	
	public HeroDamageCommand(Hero hero) {
		this.hero = hero;
	}
	
	public void Execute ()
	{
		hero.controller.addHp (-20);
		hero.CurrentHP = hero.model.HP;  //test code...
		hero.HealthUpdate ();
		if(hero.model.HP <= 0)
		{
			Debug.Log(hero.model.Name + " die");
			hero.StateMachine.ChangeState(HeroState_Dying.Instance);
		}
	}
}
