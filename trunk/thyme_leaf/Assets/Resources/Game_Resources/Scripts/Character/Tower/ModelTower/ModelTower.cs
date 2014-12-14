using UnityEngine;
using System.Collections;

public class ModelTower : AutomartTower
{
	//attack...
	private int attackDelay;
	private int attackDamage;
	private int attackRange;
	
	public ModelTower()
	{
	}

	public void setAttackDelay(int v) { attackDelay = v; }
	public int getAttackDelay() { return attackDelay; }

	public void setAttackDamage(int v) { this.attackDamage = v; }
	public int getAttackDamage() { return this.attackDamage; }

	public void setAttackRange(int v) { this.attackRange = v; }
	public int getAttackRange() { return this.attackRange; }
}
