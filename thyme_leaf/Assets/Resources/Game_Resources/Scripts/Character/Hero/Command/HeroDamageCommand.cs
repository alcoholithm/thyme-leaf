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
		hero.CurrentHP = hero.model.getHp ();  //test code...
		hero.HealthUpdate ();
		if(hero.model.getHp() <= 0)
		{
			hero.StateMachine.ChangeState(HeroState_Dying.Instance);
		}
	}
}
