using UnityEngine;
using System.Collections;

public class HeroDamageCommand : ICommand {
	private Hero hero;
    private int damage;
    	
	public HeroDamageCommand(Hero hero, int damage) {
		this.hero = hero;
        this.damage = damage;
	}
	
	public void Execute ()
	{
		hero.TakeDamage (damage);
	}
}
