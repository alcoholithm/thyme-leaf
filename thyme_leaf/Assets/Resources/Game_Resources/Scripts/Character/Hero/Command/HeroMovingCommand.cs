using UnityEngine;
using System.Collections;

public class HeroMovingCommand : ICommand {

	private Hero hero;
	public HeroMovingCommand(Hero hero)
	{
		this.hero = hero;
	}
	public void Execute ()
	{
		hero.StateMachine.ChangeState(HeroState_Moving.Instance);
		hero.target = null;  //what?...
	}
}
