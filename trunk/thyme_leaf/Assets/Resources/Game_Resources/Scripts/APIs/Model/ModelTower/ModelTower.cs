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

	public override void setMaxHp(int v) { maxHp = v; }
	public override int getMaxHp() { return maxHp; }
	
	public override void setHp(int v) { curHp = v; }
	public override void addHp(int v) 
	{
		curHp += v;
		if(maxHp <= curHp) curHp = maxHp;
	}
	public override int getHp() { return curHp; }
	
	public override void setName(string v) { name = v; }
	public override string getName() {	return name; }
	
	public override void setDefencePoint(int v) { defence_point = v; }
	public override int getDefencePoint() { return defence_point; }
	
	public override void setType(UnitType option) { type = option; }
	public override UnitType getType() { return type; }

}
