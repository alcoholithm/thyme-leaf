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
		hero.hPoint = hero.model.getHp ();  //test code...
		if(hero.model.getHp() <= 0)
		{
			hero.StateMachine.ChangeState(HeroState_Dying.Instance);
		}
	}
}
