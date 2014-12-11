using UnityEngine;
using System.Collections;

public class AttackCommand : ICommand {
    private GameEntity target;
    private int attackPower;

    public AttackCommand(GameEntity target, int attackPower)
    {
        this.target = target;
        this.attackPower = attackPower;
	}
	
	public void Execute ()
	{
        //(target as Hero).hPoint -= attackPower;
        //if(hero.hPoint <= 0)
        //{
        //    hero.StateMachine.ChangeState(HeroState_Dying.Instance);
        //}
	}
}
