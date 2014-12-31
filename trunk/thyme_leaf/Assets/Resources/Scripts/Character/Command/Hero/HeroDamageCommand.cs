using UnityEngine;
using System.Collections;

public class HeroDamageCommand : ICommand {
	private Hero hero;
	
	public HeroDamageCommand(Hero hero) {
		this.hero = hero;
	}
	
	public void Execute ()
	{
		hero.TakeDamage (20);
	}
}
