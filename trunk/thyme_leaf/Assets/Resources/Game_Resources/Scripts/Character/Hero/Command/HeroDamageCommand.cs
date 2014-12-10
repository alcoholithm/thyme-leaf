using UnityEngine;
using System.Collections;

public class HeroDamageCommand : ICommand {
	private Hero hero;
	
	public HeroDamageCommand(Hero hero) {
		this.hero = hero;
	}
	
	public void Execute ()
	{
		hero.hPoint -= 1;
		if(hero.hPoint <= 0)
		{
			hero.StateMachine.ChangeState(HeroState_Dying.Instance);
		}
	}
}
